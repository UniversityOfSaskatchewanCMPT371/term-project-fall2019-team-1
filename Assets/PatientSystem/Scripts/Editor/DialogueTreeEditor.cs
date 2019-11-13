using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;

#if UNITY_EDITOR

public class DialogueTreeEditor : EditorWindow
{
    public string currentResourcePathOfTree = "DialogueTrees/Tree0";

    [MenuItem("Window/DialogueTreeEditor")]
    static void ShowEditor()
    {
        var editor = EditorWindow.GetWindow<DialogueTreeEditor>();
        Debug.Assert(editor != null, "there is no editor");
        editor.Show();
    }

    public void Awake()
    {
    }

    public void OnGUI()
    {
        var dialogue = Resources.LoadAll<DialogueNode>(currentResourcePathOfTree)[0];

        DrawNode(dialogue, null, 0, 0);
    }

    int nodeWidth = 200;
    int nodeHeight = 100;
    int nodeWidthPadding = 10;
    int nodeHeightPadding = 25;

    void DrawNode(DialogueNode node, DialogueNode parent, int x, int y)
    {
        Rect GetBackgroundRect(int tx, int ty)
        {
            return new Rect(
                (tx * nodeWidth) + (nodeWidthPadding / 2),
                (ty * nodeHeight) + (nodeHeightPadding / 2),
                nodeWidth - nodeWidthPadding,
                nodeHeight - nodeHeightPadding);
        }

        Rect GetFocusRect()
        {
            var bgBox = GetBackgroundRect(x, y);
            bgBox.x += 40;
            bgBox.width = 60;
            bgBox.height = 25;
            return bgBox;
        }

        Rect GetAddRect()
        {
            var bgBox = GetBackgroundRect(x, y);
            bgBox.width = 25;
            bgBox.height = 25;
            return bgBox;
        }

        Rect GetDeleteRect()
        {
            var bgBox = GetBackgroundRect(x, y);
            bgBox.y += 40;
            bgBox.width = 25;
            bgBox.height = 25;
            return bgBox;
        }

        var backgroundBox = GetBackgroundRect(x, y);

        //draw the background
        EditorGUI.DrawRect(backgroundBox, Color.grey);

        // draw the focus button
        if (GUI.Button(GetFocusRect(), "Focus"))
        {
            Selection.activeObject = node;
            SceneView.FrameLastActiveSceneView();
        }

        // draw the add button
        if (GUI.Button(GetAddRect(), "+"))
        {
            AddToNode(node);
        }

        // draw the delete button
        if (GUI.Button(GetDeleteRect(), "X"))
        {
            DeleteNode(node, parent);
        }

        for (var i = 0; i < node.options.Count; i++)
        {
            var childNode = node.options[i].node;
            var newX = x + i;
            var newY = y + 1;

            // draw the link to the child
            DrawNodeCurve(backgroundBox, GetBackgroundRect(newX, newY));

            //draw the child
            DrawNode(childNode, node, newX, newY);
        }
    }

    public void DeleteNode(DialogueNode node, DialogueNode parent)
    {
        // unlink the parent.

        // if root node, no need to unlink
        if (parent == null)
        {
            node.prompt = "";
        }
        else
        {
            // find the parent's option who next node is us.
            var parentOption = parent.options.Find((opt) => opt.node == node);

            // remove us.
            parent.options.Remove(parentOption);

            //delete our asset file.
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(node));
        }

        // delete the children now.

        // recursive deleter helper
        void DeleteChild(DialogueNode cNode)
        {
            // delete current node
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(cNode));

            // recursively delete children
            cNode.options.ForEach((opt) => DeleteChild(opt.node));
        }
        // delete all children
        node.options.ForEach((opt) => DeleteChild(opt.node));

        // clear our children
        node.options.Clear();
    }

    public void AddToNode(DialogueNode node)
    {
        // create the instance of the object
        var newNode = ScriptableObject.CreateInstance<DialogueNode>();
        newNode.prompt = "";
        newNode.options = new List<DialogueOption>();

        // create the new option
        var newOption = new DialogueOption(newNode);

        // add the option to the node
        node.options.Add(newOption);

        var newAssetFileName = string.Format("{0}/DialogueNode_{1}.asset",
            Path.GetDirectoryName(AssetDatabase.GetAssetPath(node)),
            Path.GetRandomFileName());

        // write the object to a file.
        AssetDatabase.CreateAsset(newNode, newAssetFileName);
    }

    int shadowLine = 1;
    int shadowEdge = 4;
    Color shadowCol = Color.blue;
    Color lineCol = Color.black;

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        for (int i = 0; i < shadowLine; i++)
        {
            // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * shadowEdge);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, lineCol, null, 1);
    }
}
#endif
