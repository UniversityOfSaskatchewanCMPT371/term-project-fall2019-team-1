using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR

/// <summary>
///
/// 
/// <c>NodeEditor</c>
/// 
/// Description:builds the NODE graphic
/// 
/// Pre-condition: System needs access to Dialouge GUI and Custom GUI scripts when running.
/// 
/// Post-condition: Builds a node within the custom GUI editor.
///
/// </summary>
/// 
/// <authors>
/// Clayton VanderStelt
/// </authors>

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


    /// <summary>
    /// 
    /// <c>init_texture</c>
    /// 
    /// Description: Creates textures for the node.
    /// 
    /// pre-condition: must be a tree section must not be NULL.
    /// 
    /// post-condition: coats the desired tree object in a texture.
    /// 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void init_texture()
    {
        Tree_Section_texture = new Texture2D(1, 1);
        Tree_Section_texture.SetPixel(0, 0, Tree_Section_Colour);
        Tree_Section_texture.Apply();

    }

    /// <summary>
    /// 
    /// <c>init_texture</c>
    /// 
    /// Description: provides special layout for tree GUI
    /// 
    /// pre-condition: must be a tree section must not be NULL.
    /// 
    /// post-condition: Moves around special object in tree.
    /// 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
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

    // enables a texture.
    private void OnEnable()
    {
        init_texture();
    }

    /// <summary>
    /// 
    /// <c>Awake</c>
    /// 
    /// Description: initalizes data in dialouge tree. Awake is called when game is first ran.
    /// 
    /// pre-condition: Resources in resource folder has a dialouge tree called tree1.
    /// 
    /// post-condition: returns all dialogues.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void Awake(){
    

        Dialogues = Resources.LoadAll("DialogueTree/Tree1");
        
    }

    /// <summary>
    /// 
    /// <c>ShowEditor</c>
    /// 
    /// Description: displays the node within the editor window on the unity editor.
    /// 
    /// pre-condition: a Tree is avabile and built to be shown in the editor.
    /// 
    /// post-conditions: updates the gui to display a node on the editor. 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    [MenuItem("Window/Node editor")]
    static void ShowEditor() {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
       
        editor.Show();
    }

    /// <summary>
    /// <c>OnGUI</c>
    /// 
    /// Description: Enables the use of GUI, a reserved unity method call. OnGUI controls
    /// graphical objects within this script.
    /// 
    /// pre-condition: data must not be null, as nothing can run.
    /// 
    /// post-condition: ability to control and use GUI for CustomGUI panel.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
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

    /// <summary>
    /// 
    /// <c>DrawDialougeTree</c>
    /// 
    /// Description: Draws out dialouge Tree on the window unity editor.
    /// 
    /// pre-condition: there must be a tree so this function can correctly draw out a tree
    /// 
    /// post-Condition: draws out a dialouge tree.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
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


        //for every dialogue node.
        for (int i = 0; i < Dialogues.Length; i++)
        {
            //for every response in that node.
            for (int j = 0; j < ((Dialogue)Dialogues[i]).next.Count; j++)
            {
                //if the response points to a node.
                if (((Dialogue)Dialogues[i]).next[j] != null)
                {
                    //draw a line from the current node, to the response node.
                    DrawNodeCurve(dialoguewindows[i],
                        dialoguewindows[getNodeIndex(((Dialogue)Dialogues[i]).next[j])]);
                }
            }
        }

    }

    /// <summary>
    /// 
    /// <c>DrawDialougeNode</c>
    /// 
    /// description: draws a node onto the custom GUI panel.
    /// 
    /// pre-condition: nodes cannot be NULL.
    /// 
    /// post-condition: draws a node by its certian ID.
    /// 
    /// </summary>
    /// <param name="id">ID of node to draw on graphical panel.</param>
    /// <returns>NULL</returns>
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

    /// <summary>
    /// 
    /// <c>DrawNodeCurve</c>
    /// 
    /// Description: draws the node box, give the node box a cruve look.
    /// 
    /// pre-condition: Node needs to exist before we can draw it.
    /// 
    /// post-condition: draws a new rounded node box!
    /// 
    /// 
    /// </summary>
    /// <param name="start">place to start drawing rounded box</param>
    /// <param name="end">place to stop drawing rounded box.</param>
    ///  <returns>NULL</returns>
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height /2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height /2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
 
        for (int i = 0; i < 3; i++) {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
 
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    
    /// <summary>
    /// 
    /// <c>getNodeIndex</c>
    /// 
    /// Description: a helper function to get the index of a given node.
    /// 
    /// pre-condition: dialougewindows is built and has nodes within it.
    /// 
    /// post-condtion: returns a certian nodes index in the tree.
    /// 
    /// </summary>
    /// <param name="node">node we want its index.</param>
    /// 
    /// <returns>returns the index of the node, or -1</returns>
    int getNodeIndex(Dialogue node)
    {
        //for every node in the list of node windows.
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

#endif