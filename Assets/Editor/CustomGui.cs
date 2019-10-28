using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR

public class CustomGUI : EditorWindow
{
    public Object[] Trees;
    public List<Rect> dialoguewindows = new List<Rect>();
    public List<Rect> responsewindows = new List<Rect>();
    public List<int> attachedWindows = new List<int>();
    public int x = 10;
    public int y = 10;
    public Object[] Dialogues;
    public Dialogue Dialogue;
    public Dialogue currentNode;
    public int currentTree;

    // Called after all gameObjects are initialized, Used to initialized variables 
    public void Awake()
    {
        Trees = Resources.LoadAll("DialogueTree");
    }

    // Adds the button on the window tab
    [MenuItem("Window/customGUI")]
    static void ShowEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
        editor.Show();
        Debug.Assert(editor != null, "there is no editor");
    }
     
    // Called several times per frame, used to redraw the GUI
    public void OnGUI()
    {

    }


    /* Draws the Dialogue Tree in the customGUI window
     * 
     * PARAM    index - The index of the tree that is being drawn
     * PRE -  The index of the tree being drawn
     * POST - Tree is displayed in the cusotmGUI window
     * RETURN - NULL
     */
    public void drawTree(int index)
    {

    }

    /* Draws a node
    * 
    * PARAM:    The index of a node
    * PRE -  The id of the node being drawn is not null
    * POST - Node is displayed in the cusotmGUI window
    * RETURN - NULL
    */
    public void drawNode(int id)
    {

    }

    /* Draws a node
    * 
    * PARAM:    id - the index of a node
    * PRE -  The id of the node being drawn is not null
    * POST - All of the responses in the node are displayed in the cusotmGUI window
    * RETURN - NULL
    */
    public void drawReponse(int id)
    {

    }

    /*a helper function to get the index of a given node
    * 
    * PARAM:    node -  dialogue node that exists in the Dialogue[] array
    * PRE - The given node is not null
    * POST - NONE
    * RETURN - the index of the node in the Dialogue[] array
    */
    public int getNodeIndex(Dialogue node)
    {
        return 0;
    }

    /* A helper function that draws a line between nodes.
    * 
    * PARAM:    start - The node that the line is starting from
    *           end - The node that the line is ending from
    * PRE -  start and end are not null
    * POST - Draws a line between two nodes
    * RETURN - NONE
    */
    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < 3; i++)
        {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
    
    /* A helper function that finds the current node. if there is more than one current node
     * throw and error. if there is no selected current, set current to the first node.
    * 
    * PRE -  The tree is not null.
    * POST - NONE.
    * RETURN - A dialogue that is asigned as the current node.
    */
    public Dialogue findCurrent()
    {
        return null;
    }


}


#endif