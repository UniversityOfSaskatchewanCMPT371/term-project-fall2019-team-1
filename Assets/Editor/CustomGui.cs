using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR

public class CustomGUI : EditorWindow
{
    public int Trees;
    public List<Rect> dialoguewindows = new List<Rect>();
    public List<Rect> responsewindows = new List<Rect>();
    public List<int> attachedWindows = new List<int>();
    public int x = 10;
    public int y = 10;
    public Object[] Dialogues;
    public Dialogue Dialogue;
    public List<Dialogue> toDestroy;
    public Dialogue currentNode;
    public int currentTree;
    public Vector2 scrollBar;
    private FileInfo info;
    List<int> found;
    List<int> treesToDelete;


    // Called after all gameObjects are initialized, Used to initialized variables 
    public void Awake()
    {
        found = new List<int>();
        treesToDelete = new List<int>();

        // Obtain all of the Dialogue Objects.
        Dialogues = Resources.LoadAll("DialogueTree");
        Debug.Assert(Dialogues != null, "failure loading dialgoues");

        // Find the amount of trees.
        Trees = findTrees();

        // The first tree will be the default tree.
        currentTree = 1;
    }

    // Adds the button on the window tab
    [MenuItem("Window/customGUI")]
    static void ShowEditor()
    {
        CustomGUI editor = EditorWindow.GetWindow<CustomGUI>();
        editor.Show();
        Debug.Assert(editor != null, "there is no editor");
    }
     
    // Called several times per frame, used to redraw the GUI
    public void OnGUI()
    {
        Trees = findTrees();
        drawTree(currentTree);

        // Load the dialogue objects for the given tree.
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + currentTree);

        EditorGUILayout.BeginHorizontal();
        
        scrollBar = GUILayout.BeginScrollView(scrollBar, false, true, GUILayout.Width(120));

        // For every tree.
        for(int i = 0; i < Trees; i++)
        {
            GUILayout.BeginHorizontal();

            if (!treesToDelete.Contains(i))
            {
                // Make a button for that tree.
                GUI.backgroundColor = Color.white;
                if (GUILayout.Button("Tree " + (i + 1)))
                {
                    currentTree = i + 1;
                    Debug.Log(currentTree);
                }
                // Make a button to delete that tree.
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("x"))
                {
                    GUI.backgroundColor = Color.red;
                    treesToDelete.Add(i);
                }

            }

            GUILayout.EndHorizontal();
        }

        //TODO: Add a button that creates a new tree

        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndHorizontal();


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

    /* Returns the amount of trees that are currently in the resources folder.
    * 
    * PRE -  NONE
    * POST - NONE.
    * RETURN - the number of trees in the resources folder
    */
    public int findTrees()
    {
        Dialogues = Resources.LoadAll("DialogueTree");

        // For every node
        for (int i = 0; i < Dialogues.Length; i++)
        {
            // If that node belongs to a tree that has not been found yet 
            if(!found.Contains(((Dialogue)Dialogues[i]).tree))
            {
                found.Add(((Dialogue)Dialogues[i]).tree);
            }
        }
        return found.Count;
    }



}


#endif