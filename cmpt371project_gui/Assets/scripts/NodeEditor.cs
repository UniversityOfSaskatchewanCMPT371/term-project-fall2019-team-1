using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
 
 
public class NodeEditor : EditorWindow {
 
    List<Rect> dialoguewindows = new List<Rect>();
    List<Rect> repsonsewindows = new List<Rect>();
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();
    
    Object[] Dialogues;
    Dialogue Dialogue;


    public void Awake(){
        Dialogues = Resources.LoadAll("Dialogues");        
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
    
        // if (GUILayout.Button("Create Node")) {
        //     windows.Add(new Rect(10, 10, 100, 100));
        // }
 
        // for (int i = 0; i < windows.Count; i++) {
        //     windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, "Window " + i);
        // }
        EndWindows();

    }

    void DrawDialogueTree()
    {
        for (int i = 0; i < Dialogues.Length; i++)
        {
            dialoguewindows.Add(new Rect(10, 10, 100, 100));
            dialoguewindows[i] = GUI.Window(((Dialogue)Dialogues[i]).id, dialoguewindows[i],
            DrawDialogueNode,
           "Dialgoue " + ((Dialogue)Dialogues[i]).id);
        }
        for (int i = 0; i < Dialogues.Length; i++)
        {
            for (int j = 0; j < ((Dialogue)Dialogues[i]).next.Length; j++)
            {
                if (((Dialogue)Dialogues[i]).next[j] != 0)
                {
                    DrawNodeCurve(dialoguewindows[i], dialoguewindows[((Dialogue)Dialogues[i]).next[j] -1]);
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
        ((Dialogue)Dialogues[id-1]).prompt = GUILayout.TextField(((Dialogue)Dialogues[id-1]).prompt);
        GUILayout.EndHorizontal();



        GUI.DragWindow();
        // for (int i = 0; i < Dialogues.Length; i++){
        //     Debug.Log(((Dialogue)Dialogues[i]).id);
        //     Dialogue = (Dialogue)Dialogues[i];

        //     for(int j = 0; j < Dialogue.response.Length; j++){
        //         repsonsewindows.Add(new Rect(10, 10, 100, 100));
                
        //     }
            
        // }
    }
    void DrawNodeWindow(int id) {
    }
 
 
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
 
        for (int i = 0; i < 3; i++) {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
 
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
