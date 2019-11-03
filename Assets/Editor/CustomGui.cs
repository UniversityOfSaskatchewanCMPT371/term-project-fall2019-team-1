using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR


/// <summary>
/// 
/// Author: Clayton VanderStelt
/// 
/// <c>CustomGUI</c>
/// Description:Panel which allows the users to successfully build a dialouge tree, when interacting with NPC
/// entity. To find follow this path: Window/CustomGUI
/// 
/// Pre-condtion: System needs access to Dialouge GUI and Node editor scripts when running.
/// 
/// Post-condition: Builds a interactable tree editor window in unity.
///
/// </summary>
/// 
/// <authors>
/// Clayton VanderStelt
/// </authors>
public class CustomGUI : EditorWindow
{
    // A list of prompt windows.
    public List<Rect> dialwindows = new List<Rect>();

    // A list of the response windows.
    public List<Rect> responsewindows = new List<Rect>();
    
    // A list of all Dialogue objects.
    public Object[] Dialogues;

    // A list of the Dialogues of the current tree.
    public List<Dialogue> treeDialogues;

    // The Layer that the corrosponding node in dialogueWindows is in.
    public List<int> NodeLayer;

    // The current Tree being drawn.
    public int currentTree;

    // A Scrollbar for the list of trees.
    public Vector2 scrollBar;

    // The Dialogue Tree ScrollBar.
    public Vector2 scrollBar2;
    // A list of nodes at a given layer.
    public List<int> atLayer;

    // A list of found trees.
    public List<int> found;

    // A list of trees to delete.
    public List<int> treesToDelete;

    // The amount of layers in the Tree.
    public int layers;


    /// <summary>
    /// 
    /// <c>Awake</c>
    /// 
    /// Description: Initalizes data for this class at game start time.
    /// 
    /// 
    /// pre-condtions: Game running.
    /// 
    /// post-conditions: initalizes data for Custom GUI.
    /// 
    /// 
    /// </summary>
    /// <returns> NULL </returns>
    public void Awake()
    {
        layers = 0;
        Debug.Assert(layers == 0, "failure to create found");

        found = new List<int>();
        Debug.Assert(found != null, "failure to create found");

        treeDialogues = new List<Dialogue>();
        Debug.Assert(treeDialogues != null, "failure to create treesDialogues");

        treesToDelete = new List<int>();
        Debug.Assert(treesToDelete != null, "failure to create treesToDelete");

        atLayer = new List<int>();
        Debug.Assert(atLayer != null, "failure to create atlayer");

        // Obtain all of the Dialogue Objects.
        Dialogues = Resources.LoadAll("DialogueTree");
        Debug.Assert(Dialogues != null, "failure loading dialgoues");

        // -1 indicates that there is no current tree selected.
        currentTree = -1;
        Debug.Assert(currentTree < 0, "failure to set tree to first tree");
        

        NodeLayer = new List<int>();
        Debug.Assert(NodeLayer != null, "failure to create nodeLayer");

        dialwindows = new List<Rect>();
        Debug.Assert(dialwindows != null, "failure to create dialwindows");

        responsewindows = new List<Rect>();
        Debug.Assert(responsewindows != null, "failure to create dialwindows");
    }

    

    /// <summary>
    /// 
    /// Description: Adds the button on the window tab.
    /// 
    /// Pre-condition: Node must not be NULL.
    /// 
    /// Post-condtion: Returns a new button
    /// 
    /// </summary>
    /// <returns>NULL</returns>
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
        findTrees();
        Debug.Assert(found.Count >= 0, "Error in OnGUI, failure to obtain number of trees");


        // If a tree was found.
        if (found.Count > 0 && currentTree < 0)
        {
            // The first tree will be the default tree.
            currentTree = found[0];
            Debug.Assert(currentTree > 0, "failure to set tree to first tree");
        }

        // Load the dialogue objects for the given tree.
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + currentTree);
        Debug.Assert(Dialogues != null, "Error in OnGUI, failure to obtain Dialogues");

        //Refresh and atLayer
        atLayer.Clear();
        Debug.Assert(atLayer != null, "Error in OnGUI, failure to refresh atLayer");

        EditorGUILayout.BeginHorizontal();
        
        // The scrollbar for the list of trees.
        scrollBar = GUILayout.BeginScrollView(scrollBar, false, true, GUILayout.Width(120));

        // For every tree.
        for(int i = 0; i < found.Count; i++)
        {
            GUILayout.BeginHorizontal();

            if (!treesToDelete.Contains(i))
            {
                // Make a button for that tree.
                GUI.backgroundColor = Color.white;
                // Make the button colour for the current tree cyan.
                if(currentTree == i+1)
                {
                    GUI.backgroundColor = Color.cyan;
                }
                if (GUILayout.Button("Tree " + found[i]))
                {
                    currentTree = i + 1;
                    Debug.Log(currentTree);
                }

                // Make a button to delete that tree.
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("x"))
                {
                    GUI.backgroundColor = Color.red;
                    AssetDatabase.DeleteAsset("Assets/Resources/DialogueTree/Tree" + found[i]);
                    treesToDelete.Add(i);

                }
            }

            GUILayout.EndHorizontal();
        }

        // A button that makes a new tree.
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("Add"))
        {
            bool treeAdded = false;
            // Make a new folder for the tree.
            int i = 1;
            // While the Tree hasn't been made yet.
            while (!treeAdded)
            {
                Debug.Log(i);

                // Make sure that tree doesnt already exist.
                if (!found.Contains(i))
                {
                    // Make the folder.
                    AssetDatabase.CreateFolder("Assets/Resources/DialogueTree", "Tree" + i);
                    
                    // Make the head dialogue.
                    Dialogue newDial = new Dialogue();
                    newDial.tree = i;
                    newDial.start = true;

                    AssetDatabase.CreateAsset(newDial, "Assets/Resources/DialogueTree/Tree" + i + "/" + "Dialogue.asset");

                    // Add the new tree to found.
                    found.Add(i);
                    treeAdded = true;
                }
                i++;
            }
            
        }
        EditorGUILayout.EndScrollView();

        // Create an arrea for the import/export buttons.
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("import"))
        {
            //TODO: do import
        }

        if(GUILayout.Button("export"))
        {
            //TODO: do export
        }
        EditorGUILayout.EndHorizontal();
           //TODO: display what tree is active.


        // Create an area for the nodes to be in.
        scrollBar2 = GUILayout.BeginScrollView(scrollBar2, true, true, new
            GUILayoutOption[]{
                GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true),

        }) ;

        // If there is atleast one tree.
        if (found.Count != 0)
        {
            // Draw that tree.
            drawTree(currentTree);
        }

        GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Repaint();
    }


    /// <summary>
    /// 
    /// <c>drawTree</c>
    /// 
    /// Description: Draws out the graphical tree in the unity window for Custom GUI window.
    /// 
    /// Pre-condition:The index of the tree being drawn
    /// 
    /// Post-condition: Tree is displayed in the cusotmGUI window.
    /// 
    /// </summary>
    /// 
    /// <param name="treeNum">The number of the tree that is being drawn</param>
    /// <returns>NULL</returns>
    public void drawTree(int treeNum)
    {
        //find the head node.
        Dialogue head = ScriptableObject.CreateInstance<Dialogue>();

        dialwindows.Clear();
        NodeLayer.Clear();


        for (int i = 0; i < Dialogues.Length; i++)
        {
            //if this node belongs to the current tree, and is the head of that tree.
            if ((((Dialogue)Dialogues[i]).tree == treeNum) && (((Dialogue)Dialogues[i]).start == true))
            {
                head = (Dialogue)Dialogues[i];
                goto Found; 
            }
            else
            {
                //if it cant be found, set it ot the first one that appears.
                head = (Dialogue)Dialogues[0];
            }
        }

        Found:
        atLayer.Add(0);
        drawPrompt(head, 0);
    }

    /// <summary>
    /// 
    /// <c>DrawPrompt</c>
    /// 
    /// Description: Draws out a node in the CUSTOM GUI window. Draws "prompt" refers to the prompt in the node
    /// being drawn.
    /// 
    /// Pre-condition: The node being drawn is not NULL.
    /// 
    /// post-condition: Node is displayed in the customGUI window.
    ///
    /// </summary>
    /// <param name="dial">the dialogue being draw.</param>
    /// <param name="layer">the layer of the node</param>
    /// <returns>The Rect of the node.</returns>
    public Rect drawPrompt(Dialogue dial, int layer)
    {
        float biggestX = 0;

        if (layer > layers)
        {
            layers = layer;
        }

        Debug.Assert(dial != null, "Error in drawPrompt, the dialogue input was null.");
        Debug.Assert(layer >= 0, "Error in drawPrompt, the layer input was less than 0.");

        // Find other nodes at the current layer.
        for(int i = 0; i < dialwindows.Count; i++)
        {
            // Find the hightest x position of the nodes on the same layer.
            if((NodeLayer[i] == layer) && dialwindows[i].x > biggestX)
            {
                biggestX = dialwindows[i].x + dialwindows[i].width;
            }
        }

        //if the response list doesnt exist.
        if (dial.response == null)
        {
            //Make one
            dial.response = new List<string>();
        }

        //if the next list doesnt exist.
        if (dial.next == null)
        {
            //Make one
            dial.next = new List<Dialogue>();
        }
        //TODO: have node start below its parent.

        // Create the position of the node.
        Rect nodeRect = new Rect(dial.response.Count *100 + 20 + biggestX, layer * 100, 175, 65);
        Rect textRect = new Rect(nodeRect.x, nodeRect.y + 20, nodeRect.width, 20);
        Rect buttonRect = new Rect(nodeRect.x, textRect.y + 25, nodeRect.width, 20);
        Rect exitRect = new Rect(nodeRect.x + nodeRect.width - 15, nodeRect.y, 15, 15);

        dialwindows.Add(nodeRect);
        NodeLayer.Add(layer);

        //if this node is not going to be deleted.


            // Draw the node.
            EditorGUI.DrawRect(nodeRect, Color.grey);

            // Display the dialogues parameters.
            EditorGUI.LabelField(nodeRect, "NPC Prompt:");
            dial.prompt = EditorGUI.TextField(textRect, dial.prompt);

            // Make a button for creating another node.
            if (GUI.Button(buttonRect, "new child"))
            {
                GUI.FocusControl(null);
                dial.response.Add("");
                dial.next.Add(null);
            }
        // If the current node is not the head.

        //Create a delete button if this is a leaf node.
        if (dial.response.Count == 0)
        {
            if (layer != 0)
            {
                // Make a button for deleting the node.
                GUI.backgroundColor = Color.red;
                if (GUI.Button(exitRect, "X"))
                {
                    AssetDatabase.DeleteAsset("Assets/Resources/DialogueTree/Tree" + dial.tree + "/" + dial.name + ".asset");
                }
                GUI.backgroundColor = Color.white;
            }
        }

        //if this node has a list of responses.
        if (dial.response != null)
        {
            // For each of its responses, create a response node.
            for (int i = 0; i < dial.response.Count; i++)
            {
                // if adding the response would make an new layer... 
                if (atLayer.Count == layer + 1)
                {
                    // Make a new layer
                    atLayer.Add(0);
                }

                Rect child = drawReponse(dial, layer + 1, i);
                Debug.Assert(child != null, "error in drawPrompt, the child was null");
                DrawNodeCurve(nodeRect, child);
            }
        }

        // Notify the other nodes that this node is at the current layer.
        atLayer[layer] ++;

        

        return nodeRect;
    }

    /// <summary>
    /// 
    /// <c>drawReponse</c>
    /// 
    /// Description: Draws a dialouge section within the node.
    /// 
    /// Pre-condtion:The id of the node being drawn is not null.
    /// 
    /// Post-condition: All of the responses in the node are displayed in the cusotmGUI window.
    /// 
    /// </summary>
    /// 
    /// <param name="dial">the dialogue being draw.</param>
    /// <param name="layer">the layer of the node</param>
    /// <param name="index">the index of the response being drawn</param>
    /// 
    /// <returns>The rect of the response node</returns>
    public Rect drawReponse(Dialogue dial, int layer, int index)
    {
        if(layer > layers)
        {
            layers = layer;
        }

        // Create the position of the node.
        Rect nodeRect = new Rect((atLayer[layer] * 200) + 10, layer * 100, 175, 70);
        Rect textRect = new Rect(nodeRect.x, nodeRect.y + 20, nodeRect.width, 20);
        Rect buttonRect = new Rect(nodeRect.x, textRect.y + 25, nodeRect.width, 20);
        Rect exitRect = new Rect(nodeRect.x + nodeRect.width - 15, nodeRect.y, 15, 15);

        responsewindows.Add(nodeRect);
        

        // Draw the node.
        EditorGUI.DrawRect(nodeRect, Color.white);

        // Display the dialogues parameters.
        EditorGUI.LabelField(nodeRect, "User Response:");
        dial.response[index] = EditorGUI.TextField(textRect, dial.response[index]);

        if (dial.next.Count >= index)
        {
            if (dial.next[index] == null)
            {
                // Make a button for creating another node.
                if (GUI.Button(buttonRect, "new child"))
                {
                    GUI.FocusControl(null);

                    // Make a new Dialogue, and set some of its parameters
                    Dialogue newDial = ScriptableObject.CreateInstance<Dialogue>();
                    newDial.start = false;
                    newDial.tree = dial.tree;
                    newDial.response = new List<string>();
                    newDial.prompt = "";
                    AssetDatabase.CreateAsset(newDial, "Assets/Resources/DialogueTree/Tree" + dial.tree + "/Dialogue" + Dialogues.Length + ".asset");

                    //if the current Dialogue doesnt have a next, make one
                    if (dial.next == null)
                    {
                        dial.next = new List<Dialogue>();
                    }

                    Debug.Assert(dial.next != null, "Error in drawResponse, dial.next is null");

                    // If the next[] already has a spot open set it to the new node
                    if (dial.next.Count >= index)
                    {
                        dial.next[index] = newDial;
                    }
                    // Otherwise, add it to the list
                    else
                    {
                        dial.next.Add(newDial);
                    }
                }
            }
        }

        // If the current response has no next, make a delete button.
        if (dial.next[index] == null)
        {
            // Make a button for deleting the response.
            GUI.backgroundColor = Color.red;
            if (GUI.Button(exitRect, "X"))
            {
                dial.response.Remove(dial.response[index]);
                dial.next.Remove(dial.next[index]);
            }
            GUI.backgroundColor = Color.white;
        }
        // Else, if it does have a next, make a new node for it.
        else
        {
            // if adding the next node would make an new layer... 
            if (atLayer.Count == layer + 1)
            {
                // Make a new layer
                atLayer.Add(0);
            }

            Rect child = drawPrompt(dial.next[index], layer + 1);
            Debug.Assert(child != null, "Error in drawResponse, child was null");
            DrawNodeCurve(nodeRect, child);
        }
        
        // Notify the other nodes that this node is at the current layer.
        atLayer[layer]++;

        return nodeRect;
    }

    /// <summary>
    /// 
    /// <c>getNodeIndex</c>
    /// 
    /// Description: a helper function to get the index of a given node.
    /// 
    /// Pre-condition: The given node is not null.
    /// 
    /// Post-condition: NONE.
    /// 
    /// </summary>
    /// <param name="node">dialogue node that exists in the Dialogue[] array</param>
    /// <returns>the index of the node in the Dialogue[] array</returns>
    public int getNodeIndex(Dialogue node)
    {
        return 0;
    }
    //TODO: might not need.


    //TODO: find a better curve function.

    /// <summary>
    /// 
    /// <c>DrawNodeCurve</c>
    /// 
    /// Description: A helper function that draws a line between nodes.
    /// 
    /// Pre-condition:start and end are not null.
    /// 
    ///Post-condtion:Draws a line between two nodes.
    ///
    /// </summary>
    /// <param name="start">The node that the line is starting from</param>
    /// <param name="end">The node that the line is ending from</param>
    /// <returns>NULL</returns>
    void DrawNodeCurve(Rect start, Rect end)
    {
        int shadowLine = 1;
        int shadowEdge = 4;
        Vector3 startPos = new Vector3(start.x + start.width/2, start.y + start.height, 0);
        Vector3 endPos = new Vector3(end.x + end.width/2, end.y, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < shadowLine; i++)
        {// Draw a shadow.
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.blue, null, (i + 1) * shadowEdge);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    /// <summary>
    /// 
    /// <c>Find Current</c>
    /// 
    /// Description:A helper function that finds the current node. if there is more than one current node
    /// throw and error. if there is no selected current, set current to the first node.
    /// 
    /// Pre-condition:The tree is not null.
    /// 
    /// Post-condtion: NONE.
    /// 
    /// </summary>
    /// <returns> A dialogue that is asigned as the current node.</returns>
    public Dialogue findCurrent()
    {
        return null;
    }
    //TODO: might not need.

    /// <summary>
    /// 
    /// <c>findTrees</c>
    /// 
    /// Description: Returns the amount of trees that are currently in the resources folder.
    /// 
    /// Pre-condition:None
    /// 
    /// Post-condition: None
    /// 
    /// </summary>
    /// <returns>the number of trees in the resources folder</returns>
    public int findTrees()
    {
        Dialogues = Resources.LoadAll("DialogueTree");

        // For every node
        for (int i = 0; i < Dialogues.Length; i++)
        {
            // If that node belongs to a tree that has not been found yet. 
            if(!found.Contains(((Dialogue)Dialogues[i]).tree))
            {
                found.Add(((Dialogue)Dialogues[i]).tree);
            }
        }
        found.Sort();
        return found.Count;
    }
}
#endif