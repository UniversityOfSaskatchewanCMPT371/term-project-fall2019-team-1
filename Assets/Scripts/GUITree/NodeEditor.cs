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


    Rect Tree_Section;
    Rect Node_Section;
    Color Tree_Section_Colour = new Color(13f/255f, 32f/255f, 46f/255f, 1f);
    Texture2D Tree_Section_texture;


    public void init_texture()
    {
        Tree_Section_texture = new Texture2D(1, 1);
        Tree_Section_texture.SetPixel(0, 0, Tree_Section_Colour);
        Tree_Section_texture.Apply();

    }

    public void init_Layout()
    {

        Tree_Section.x = 0;
        Tree_Section.y = 0;
        Tree_Section.width = Screen.width / 15f;
        Tree_Section.height = Screen.height;
        GUI.DrawTexture(Tree_Section, Tree_Section_texture);

        Node_Section.x = Screen.width / 10f;
        Node_Section.y = 10;
        Node_Section.width = Screen.width;
        Node_Section.height = Screen.height;


    }
    private void OnEnable()
    {
        init_texture();
    }


    public void Awake(){
    

        Dialogues = Resources.LoadAll("DialogueTree/Tree1");
        
    }
 
    [MenuItem("Window/Node editor")]
    static void ShowEditor() {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
       
        editor.Show();
    }

    void OnGUI() {

        init_Layout();

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
        GUILayout.BeginArea(Node_Section);
        BeginWindows();
        DrawDialogueTree();
        EndWindows();
        GUILayout.EndArea();
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

