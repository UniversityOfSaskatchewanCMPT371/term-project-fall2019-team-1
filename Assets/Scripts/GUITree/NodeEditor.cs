using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
 
 
public class NodeEditor : EditorWindow {
 
    List<Rect> dialoguewindows = new List<Rect>();
    List<Rect> repsonsewindows = new List<Rect>();
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();
    int x = 10;
    int y= 10;
    Object[] Dialogues;
    Dialogue Dialogue;


    public void Awake(){
        Dialogues = Resources.LoadAll("DialogueTree/Tree1");        
    }
 
    [MenuItem("Window/Node editor")]
    static void ShowEditor() {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
        editor.Show();
    }

    void OnGUI() {
        if (windowsToAttach.Count == 2) {
            attachedWindows.Add(windowsToAttach[0]);
            attachedWindows.Add(windowsToAttach[1]);
            windowsToAttach = new List<int>();
        }
 
        if (attachedWindows.Count >= 2) {
            for (int i = 0; i < attachedWindows.Count; i += 2) {
                DrawNodeCurve(dialoguewindows[attachedWindows[i]], dialoguewindows[attachedWindows[i + 1]]);
            }

        }
        BeginWindows();
        DrawDialogueTree();
        EndWindows();

    }

    void DrawDialogueTree()
    {
        for (int i = 0; i < Dialogues.Length; i++)
        {
            dialoguewindows.Add(new Rect(x, y, 150, 100));
            x += 175;
            y += 125;
            dialoguewindows[i] = GUI.Window(i, dialoguewindows[i],
            DrawDialogueNode,
           Dialogues[i].name);
            
        }

        //for every dialogue node
        for (int i = 0; i < Dialogues.Length; i++)
        {
            //for every response in that node
            for (int j = 0; j < ((Dialogue)Dialogues[i]).next.Count; j++)
            {
                //if the response points to a node
                if (((Dialogue)Dialogues[i]).next[j] != null)
                {
                    //draw a line from the current node, to the response node
                    DrawNodeCurve(dialoguewindows[i], 
                        dialoguewindows[getNodeIndex(((Dialogue)Dialogues[i]).next[j])]);
                }
            }
        }
    }

      void DrawDialogueNode(int id){
          if (GUILayout.Button("Attach")) {
            windowsToAttach.Add(id-1);
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("prompt");
        ((Dialogue)Dialogues[id]).prompt = GUILayout.TextField(((Dialogue)Dialogues[id]).prompt);
        GUILayout.EndHorizontal();
        GUI.DragWindow();
    }
    void DrawNodeWindow(int id)
    {
    }
 
 
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height /2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height /2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
 
        for (int i = 0; i < 3; i++) {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
 
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    //a helper function to get the index of a given node
    int getNodeIndex(Dialogue node)
    {
        //for every node in the list of node windows
        for(int i = 0; i < dialoguewindows.Count; i++)
        {
            if(node == Dialogues[i])
            {
                return i;
            }
        }

        return -1;
    }
}

