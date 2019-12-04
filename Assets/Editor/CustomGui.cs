using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;

#if UNITY_EDITOR


/// <summary>
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
/// Clayton VanderStelt, Oluwaseyi Kareem, Sam Horovatin
/// </authors>
public class CustomGUI : EditorWindow
{
    // Variables that affect the size/positions of the nodes
    int NodeHeight = 65;
    int NodeWidth = 175;
    int promptXChild = 100; //The amount that the prompt node moves over per child
    int NodeXBuffer = 20; //The amount of empty space between the edge and the nodes, and between each node
    int NodeYLayer = 100; //The amount that the prompt node moves down per vertical layer
    int NodeYBuffer = 10; //The amount of empty space between the edge and the nodes, and between each node
    int NodeFieldHeight = 20; //The height of the buttons/text field in the prompt.
    int exitButtonSize = 15; //The size of the exit button


    //variables that affect the lines thickness, colour, etc...
    int shadowLine = 1;
    int shadowEdge = 4;
    Color shadowCol = Color.blue;
    Color lineCol = Color.black;

    // A list of prompt windows.
    public List<Rect> dialwindows = new List<Rect>();

    // A list of the response windows.
    public List<Rect> responsewindows = new List<Rect>();

    // A list of all Dialogue objects. Used when having to load Dialogues from other trees.
    public UnityEngine.Object[] nonCurrentDialogues;

    // A list of the Dialogue objects of the current tree (Object[] format).
    public UnityEngine.Object[] Dialogues;

    // A list of the Dialogues of the current tree (Dialogue list format).
    public List<Dialogue> treeDialogues;

    // The vertical Layer that the corresponding node in dialogueWindows is in.
    public List<int> NodeLayer;

    // The current Tree being drawn.
    public int currentTree;

    // A Scrollbar for the list of trees.
    public Vector2 scrollBar;

    // The Dialogue Tree ScrollBar.
    public Vector2 scrollBar2;
    
    // A list of nodes at a given layer.
    public List<int> atLayer;

    // A list of trees.
    public List<int> trees;

    // A list of trees to delete.
    public List<int> treesToDelete;

    // The amount of layers in the Tree.
    public int layers;

    // Keeps track of the max x-value of node to adjust scrollbars.
    public float max_nodes_x;


    /// <summary>
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
        // Initializing variables with empty values.
        layers = 0;
        Debug.Assert(layers == 0, "failure to create found");

        trees = new List<int>();
        Debug.Assert(trees != null, "failure to create found");

        treeDialogues = new List<Dialogue>();
        Debug.Assert(treeDialogues != null, "failure to create treesDialogues");
        
        treesToDelete = new List<int>();
        Debug.Assert(treesToDelete != null, "failure to create treesToDelete");
        
        atLayer = new List<int>();
        Debug.Assert(atLayer != null, "failure to create atlayer");
        
        NodeLayer = new List<int>();
        Debug.Assert(NodeLayer != null, "failure to create nodeLayer");
        
        dialwindows = new List<Rect>();
        Debug.Assert(dialwindows != null, "failure to create dialwindows");
        
        responsewindows = new List<Rect>();
        Debug.Assert(responsewindows != null, "failure to create dialwindows");


        // Obtain all of the Dialogue Objects.
        Dialogues = UnityEngine.Resources.LoadAll("DialogueTree");
        Debug.Assert(Dialogues != null, "failure loading dialgoues");

        // -1 indicates that there is no current tree selected.
        currentTree = -1;
        Debug.Assert(currentTree < 0, "failure to set tree to first tree");  
    }

    

    /// <summary>
    /// Description: Adds the button on the window tab.
    /// 
    /// Pre-condition: Node must not be NULL.
    /// 
    /// Post-condtion: A new button is in the "Window" menu
    /// 
    /// </summary>
    /// <returns> nothing </returns>
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
        // Resetting because node positions may have changed, and it needs to get recalculated.
        max_nodes_x = 0;
        Debug.Assert(max_nodes_x == 0, "failure in OnGui, failed to set max nodes to 0");

        //Refresh and atLayer
        atLayer.Clear();
        Debug.Assert(atLayer != null, "Error in OnGUI, failure to refresh atLayer");
        Debug.Assert(atLayer.Count == 0, "Error in OnGUI, failure to refresh atLayer");

        // Find the amount of trees, and their IDs.
        findTrees();
        Debug.Assert(trees.Count >= 0, "Error in OnGUI, failure to obtain number of trees");
        Debug.Assert(trees != null, "Error in OnGUI, trees is null");

        // If there is no current tree, set it to the first tree found
        if (trees.Count > 0 && currentTree < 0)
        {
            // The first tree will be the default tree.
            currentTree = trees[0];
            Debug.Assert(currentTree > 0, "failure to set tree to first tree");
        }

        // Load the dialogue objects for the given tree.
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + currentTree);
        Debug.Assert(Dialogues != null, "Error in OnGUI, failure to obtain Dialogues");
        Debug.Assert(Dialogues.Length > 0, "Error in OnGUI, failure to obtain Dialogues");

        EditorGUILayout.BeginHorizontal();
      
        // The scrollbar for the list of trees.
        scrollBar = GUILayout.BeginScrollView(scrollBar, false, true, GUILayout.Width(150));
            
        // Draw the selection buttons in the left scrollview
        drawTreeSelection();

        //draw the "add new tree" button
        drawNewTreeButton();

        // End the left scrollbar.
        EditorGUILayout.EndScrollView();

        // Create an arrea for the import/export buttons.
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();

        //draw the input button
        drawImportButton();

        //draw the export button
        drawExportButton();

        //nothing else needs to be beside the import and export button
        EditorGUILayout.EndHorizontal();
        //anything below here will be below the import/export button

        // Create an area for the nodes to be in.
        scrollBar2 = GUILayout.BeginScrollView(scrollBar2, true, true);

        // If there is atleast one tree.
        if (trees.Count != 0)
        {
            // Draw that tree.
            drawTree(currentTree);
        }

        float biggestX = 0;
        // Find the highest x position.
        for (int i = 0; i < atLayer.Count; i++)
        {
            if (atLayer[i] > biggestX)
                biggestX = atLayer[i];
        }


        // Create invisible boxes that extend the scrollview
        //(done becasue the nodes themselves do not do this)
        GUI.backgroundColor = Color.clear;
        GUILayout.Box("", GUILayout.Height(-15), GUILayout.Width(max_nodes_x + 175));
        GUILayout.Box("", GUILayout.Height(atLayer.Count *100 + 100), GUILayout.Width(0));

        GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();


    }


    /// <summary>
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
        // Find the head node.
        Dialogue head = ScriptableObject.CreateInstance<Dialogue>();

        dialwindows.Clear();
        Debug.Assert(dialwindows.Count == 0, "failure in drawTree, dialwindows is not cleared properly");
       
        NodeLayer.Clear();
        Debug.Assert(NodeLayer.Count == 0, "failure in drawTree, NodeLayer is not cleared properly");

        head = findHead(convertToList(Dialogues));
        Debug.Assert(head != null, "failure in drawTree, head is not set properly");

        atLayer.Add(0);

        //giving the paramter 0 becasue the first node is at the zeroith layer.
        drawPrompt(head, 0);
    }

    /// <summary>
    /// <c>DrawPrompt</c>
    /// 
    /// Description: Draws out a node in the CUSTOM GUI window. Draws "prompt" refers to the prompt in the node
    /// being drawn.
    /// 
    /// Pre-condition: The node being drawn is not NULL.
    /// 
    /// Post-condition: Node is displayed in the customGUI window.
    ///
    /// </summary>
    /// <param name="dial">the dialogue being draw.</param>
    /// <param name="layer">the layer of the node</param>
    /// <returns>The Rect of the node.</returns>
    public Rect drawPrompt(Dialogue dial, int layer)
    {
        float biggestX = 0;

        //if a new layer was added update layers
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

        // If the response list doesnt exist.
        if (dial.response == null)
        {
            // Make one
            dial.response = new List<string>();
        }

        // If the next list doesnt exist.
        if (dial.next == null)
        {
            // Make one
            dial.next = new List<Dialogue>();
        }

        // Create the position of the node.
        Rect nodeRect = new Rect(dial.response.Count *promptXChild + NodeXBuffer + biggestX, layer * NodeYLayer + NodeYBuffer, NodeWidth, NodeHeight);

        // Adjusts the x-coordinate to adjust scrolling
        if (max_nodes_x < nodeRect.x)
        {
            max_nodes_x = nodeRect.x;
        }

        Rect textRect = new Rect(nodeRect.x, nodeRect.y + NodeFieldHeight, nodeRect.width, NodeFieldHeight);
        Rect animRect = new Rect(nodeRect.x, textRect.y + NodeFieldHeight, nodeRect.width, NodeFieldHeight);
        Rect buttonRect = new Rect(nodeRect.x, animRect.y + NodeFieldHeight, nodeRect.width, NodeFieldHeight);
        Rect exitRect = new Rect(nodeRect.x + nodeRect.width - exitButtonSize, nodeRect.y, exitButtonSize, exitButtonSize);

        Debug.Assert(textRect != null, "failure in drawPrompt. textRect was not created properly");
        Debug.Assert(animRect != null, "failure in drawPrompt. animRect was not created properly");
        Debug.Assert(buttonRect != null, "failure in drawPrompt. buttonRect was not created properly");
        Debug.Assert(exitRect != null, "failure in drawPrompt. exitRect was not created properly");

        dialwindows.Add(nodeRect);
        NodeLayer.Add(layer);

        Debug.Assert(dialwindows.Count > 0, "Failure in drawPrompt, dialwindows is an impossible value");
        Debug.Assert(NodeLayer.Count > 0, "Failure in drawPrompt, NodeLayer is an imposible value");


        // Draw the node.
        EditorGUI.DrawRect(nodeRect, Color.grey);

        // Display the dialogues parameters.
        EditorGUI.LabelField(nodeRect, "NPC Prompt:");
        dial.prompt = EditorGUI.TextField(textRect, dial.prompt);


        // Make a field for inputting an animation.
        dial.anim = (AnimationClip)EditorGUI.ObjectField(animRect, dial.anim, typeof(AnimationClip), true);

        // Make a button for creating another node.
        if (GUI.Button(buttonRect, "new child"))
        {
            GUI.FocusControl(null);
            dial.response.Add("");
            dial.next.Add(null);
        }

        Debug.Assert(dial.response != null, "Failure in drawPrompt, dial.response should not be equal to null");
        Debug.Assert(dial.next != null, "Failure in drawPrompt, dial.next should not be equal to null");

        // Create a delete button if this is a leaf node.
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

        // If this node has a list of responses.
        if (dial.response != null)
        {
            // For each of its responses, create a response node.
            for (int i = 0; i < dial.response.Count; i++)
            {
                // If adding the response would make an new layer.
                if (atLayer.Count == layer + 1)
                {
                    // Make a new layer
                    atLayer.Add(0);
                    
                }

                Rect child = drawReponse(dial, layer + 1, i, nodeRect);
                Debug.Assert(child != null, "error in drawPrompt, the child was null");
                DrawNodeCurve(nodeRect, child);
            }
        }

        // Notify the other nodes that this node is at the current layer.
        atLayer[layer] ++;

        //if the object still exists
        if (dial != null)
        {
            // Let unity know that the dialogue object needs to be saved.
            EditorUtility.SetDirty(dial);
        }
        return nodeRect;
    }

    /// <summary>
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
    /// <param name="layer">the vetical layer of the node</param>
    /// <param name="index">the index of the response being drawn</param>
    /// 
    /// <returns>The rect of the response node</returns>
    public Rect drawReponse(Dialogue dial, int layer, int index, Rect parent)
    {
        if(layer > layers)
        {
            layers = layer;
        }

        // Create the position of the node.
        Rect nodeRect = new Rect((atLayer[layer] * 200) + NodeXBuffer, layer * NodeYLayer + NodeYBuffer, NodeWidth, NodeHeight);
        
        if (max_nodes_x < nodeRect.x)
        {
            max_nodes_x = nodeRect.x;
        }
        
        Rect textRect = new Rect(nodeRect.x, nodeRect.y + NodeFieldHeight, nodeRect.width, NodeFieldHeight);
        Rect buttonRect = new Rect(nodeRect.x, textRect.y + NodeFieldHeight, nodeRect.width, NodeFieldHeight);
        Rect exitRect = new Rect(nodeRect.x + nodeRect.width - exitButtonSize, nodeRect.y, exitButtonSize, exitButtonSize);

        Debug.Assert(textRect != null, "failure in drawResponse. textRect was not created properly");
        Debug.Assert(buttonRect != null, "failure in drawResponse. buttonRect was not created properly");
        Debug.Assert(exitRect != null, "failure in drawResponse. exitRect was not created properly");

        responsewindows.Add(nodeRect);
        Debug.Assert(responsewindows != null, "failure in drawResponse. responsewindows was null");
        Debug.Assert(responsewindows.Count > 0, "failure in drawResponse. responsewindows.count was 0");

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

                    Debug.Assert(newDial != null, "failure in drawResponse. newDial was created inproperly");
                    Debug.Assert(newDial.start == false, "failure in drawResponse. newDial.start was set inproperly");
                    Debug.Assert(newDial.tree == dial.tree, "failure in drawResponse. newDial.tree was set inproperly");
                    Debug.Assert(newDial.response != null, "failure in drawResponse. newDial.response was set inproperly");
                    Debug.Assert(newDial.prompt == "", "failure in drawResponse. newDial.prompt was set inproperly");

                    // If the current Dialogue doesnt have a next, make one
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
            // If adding the next node would make an new layer... 
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

        if (dial != null)
        {
            // Let unity know that the dialogue object needs to be saved.
            EditorUtility.SetDirty(dial);
        }


        return nodeRect;
    }

    /// <summary>
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
    int getNodeIndex(Dialogue node)
    {
        //for every node in the list of node windows
        for (int i = 0; i < Dialogues.Length; i++)
        {
            if (node == (Dialogue)Dialogues[i])
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// <c>DrawNodeCurve</c>
    /// 
    /// Description: A helper function that draws a line between nodes.
    /// 
    /// Pre-condition:start and end are not null.
    /// 
    /// Post-condtion:Draws a line between two nodes.
    ///
    /// </summary>
    /// <param name="start">The node that the line is starting from</param>
    /// <param name="end">The node that the line is ending from</param>
    /// <returns>NULL</returns>
    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width/2, start.y + start.height, 0);
        Vector3 endPos = new Vector3(end.x + end.width/2, end.y, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        for (int i = 0; i < shadowLine; i++)
        {
            // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * shadowEdge);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, lineCol, null, 1);
    }
   

    /// <summary>
    /// <c>findTrees</c>
    /// 
    /// Description: Returns the amount of trees that are currently in the resources folder.
    /// 
    /// Pre-condition: The Resources/DialogueTree folder exists
    /// 
    /// Post-condition: trees has been update to hold all of the treeID's
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
            if(!trees.Contains(((Dialogue)Dialogues[i]).tree))
            {
                trees.Add(((Dialogue)Dialogues[i]).tree);
            }
        }
        trees.Sort();
        return trees.Count;
    }

    /// <summary>
    /// 
    /// <c>ImportDialogGui</c>
    /// 
    /// Description: a helper function that opens a standard open file dialog
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: None
    /// 
    /// </summary>
    /// <returns>Path to import file</returns>
    string ImportDialogGui()
    { 
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return EditorUtility.OpenFilePanel("Import Json File", path, "json");
    }

    /// <summary>
    /// 
    /// <c>ExportDialogGui</c>
    /// 
    /// Description: a helper function opens a standard save file dialog
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: None
    /// 
    /// </summary>
    /// <returns>Path to save location</returns>
    string ExportDialogGui()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return EditorUtility.SaveFilePanel("Export Tree To File", path,  "Tree"+ currentTree+ ".json", "json");
    }


    /// <summary>
    /// A Class that represents a Dialogue Node, used during the export process.
    /// Needed becasue the next[] field can not be converted to json.
    /// </summary>
    class packagedNode
    {
        public string treeName;
        public string prompt;
        public List<string> response;
        public List<string> next;
        public int tree;
        public bool head;
        public string dialName;
    }

    /// <summary>
    /// <c>package</c>
    /// 
    /// Description: a helper function that readies a Dialogue object for export by packaging it into a packagedNode
    /// 
    /// Pre-condition: dialogue is not null
    /// 
    /// Post-condition: None
    /// </summary>
    /// <param name="dialogue">a Dialogue that needs to be packaged for export</param>
    /// <returns> A packagedObject </returns>
    packagedNode package(Dialogue dialogue)
    {
        //copy the prompt and responses
        packagedNode temp = new packagedNode
        {
            treeName = dialogue.treeName,
            prompt = dialogue.prompt,
            response = dialogue.response,
            next = new List<string>(),
            tree = dialogue.tree,
            head = dialogue.start,
            dialName = dialogue.name
        };

        // For every next[] in the dialogue
        for (int i = 0; i < dialogue.next.Count; i++)
        {
            if (dialogue.next[i] != null)
            {
                temp.next.Add(dialogue.next[i].name);
            }
        }

        //check that the packaging is correct
        Debug.Assert(temp.treeName == dialogue.treeName, "Failure in package");
        Debug.Assert(temp.dialName == dialogue.name, "Failure in package");
        Debug.Assert(temp.prompt == dialogue.prompt, "Failure in package");
        Debug.Assert(temp.response == dialogue.response, "Failure in package");
        Debug.Assert(temp.response.Count == dialogue.response.Count, "Failure in package");
        Debug.Assert(temp.tree == dialogue.tree, "Failure in package");
        Debug.Assert(temp.head == dialogue.start, "Failure in package");

        return temp;
    }

    /// <summary>
    /// <c>findHead</c>
    /// 
    /// Description: a helper function that finds the head node out of a list of Dialogues
    /// 
    /// Pre-condition: dialogues is not null
    /// 
    /// Post-condition: None
    /// </summary>
    /// <param name="dialogues">A list of Dialogues</param>
    /// <returns> A single Dialogue that is the head node of the list of Dialogues </returns>
    Dialogue findHead(List<Dialogue> dialogues)
    {
        //find the head node
        for(int i = 0; i < dialogues.Count; i++)
        {
            if(dialogues[i].start == true)
            {
                return dialogues[i];
            }
        }

        //else return the first node
        return dialogues[0];
    }

    /// <summary>
    /// <c>convertToList</c>
    /// 
    /// Description: a helper function that converts a 
    /// 
    /// Pre-condition: objects is not null, and is a array of Dialogue Objects
    /// 
    /// Post-condition: None
    /// </summary>
    /// <param name="objects"></param>
    /// <returns> A list of Dialogues </returns>
    List<Dialogue> convertToList(UnityEngine.Object[] objects)
    {
        // Convert them from object[] to a list of Dialogues.
        List<Dialogue> newList = new List<Dialogue>();
        for (int j = 0; j < objects.Length; j++)
        {
            newList.Add((Dialogue)objects[j]);
        }

        return newList;
    }

    /// <summary>
    /// <c>drawTreeSelection</c>
    /// 
    /// Description: Draws the buttons in the left sidebar, used for selecting and deleting trees.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: The buttons have been drawn on the customGUI window
    /// </summary>
    /// <returns> nothing </returns>
    void drawTreeSelection()
    {
        // For every tree.
        for (int i = 0; i < trees.Count; i++)
        {
            // Put the following three buttons beside eachother.
            GUILayout.BeginHorizontal();
            
            // If the three is going to be deleted, dont draw it
            if (!treesToDelete.Contains(trees[i]))
            {
                // Grab the dialogues objects of each tree.
                nonCurrentDialogues = Resources.LoadAll("DialogueTree/Tree" + trees[i]);

                // Convert them from object[] to a list of Dialogues.
                List<Dialogue> newList = convertToList(nonCurrentDialogues);

                // Draw a text field that displays the tree's name.
                GUI.backgroundColor = Color.white;
                findHead(newList).treeName = EditorGUILayout.TextField(findHead(newList).treeName);

                // If its drawing the current tree...
                if (currentTree == trees[i])
                {
                    // Make the button colour cyan.
                    GUI.backgroundColor = Color.cyan;
                }
                else
                {
                    // Else, make it white.
                    GUI.backgroundColor = Color.white;
                }
                //Make the select button
                if (GUILayout.Button("Select"))
                {
                    currentTree = trees[i];
                }

                // Make a button to delete that tree.
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("x"))
                {
                    GUI.backgroundColor = Color.red;
                    AssetDatabase.DeleteAsset("Assets/Resources/DialogueTree/Tree" + trees[i]);
                    treesToDelete.Add(trees[i]);
                }
            }

            // Nothing else needs to be beside these three buttons
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// <c>drawNewTreeButton</c>
    /// 
    /// Description: Draws the buttons in the left sidebar, used for creating a new tree
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: The button have been drawn on the customGUI window
    /// </summary>
    /// <returns> nothing </returns>
    void drawNewTreeButton()
    {
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("Add"))
        {
            bool treeAdded = false;
            // Make a new folder for the tree.
            int i = 1;

            // While the Tree hasn't been made yet.
            while (!treeAdded)
            {
                // Make sure that tree doesnt already exist.
                if (!trees.Contains(i))
                {
                    // Make the folder.
                    AssetDatabase.CreateFolder("Assets/Resources/DialogueTree", "Tree" + i);

                    // Make the head dialogue.
                    Dialogue newDial = ScriptableObject.CreateInstance<Dialogue>();
                    Debug.Assert(newDial != null, "failure in OnGUI, newDial creation failed");
                    newDial.tree = i;
                    newDial.start = true;
                    newDial.treeName = "Tree" + i;

                    AssetDatabase.CreateAsset(newDial, "Assets/Resources/DialogueTree/Tree" + i + "/" + "Dialogue.asset");

                    // Add the new tree to found.
                    trees.Add(i);
                    treeAdded = true;
                }
                i++;
            }
        }
    }

    /// <summary>
    /// <c>drawImportButton</c>
    /// 
    /// Description: Draws the buttons above the Node View, used for importing a tree.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: The button have been drawn on the customGUI window
    /// </summary>
    /// <returns> nothing </returns>
    void drawImportButton()
    {
        // button creation
        if (GUILayout.Button("import"))
        {
            //iterable
            int i = 1;

            try
            {
                // Obtaining the path of the file that is being imported
                string path = ImportDialogGui();
                
                // The ID of the new tree that will be created by the import
                int newTree = 0;

                // Keeps track of whether a new tree has been made.
                bool treeAdded = false;

                //while the tree has not been created
                while (!treeAdded)
                {
                    // Find the lowest ID that is not being used
                    if (!trees.Contains(i))
                    {
                        // Make the folder.
                        AssetDatabase.CreateFolder("Assets/Resources/DialogueTree", "Tree" + i);

                        // Add the new tree to the lsit of trees.
                        trees.Add(i);
                        treeAdded = true;
                    }
                    //if a tree has not been created, iterate by 1
                        i++;
                }
                //-1 to account for the last i++;
                newTree = i -1;

                // Grab the file
                StreamReader inportFile = new StreamReader(path);

                // Read the file, and convert it into a packagedNode
                List<Dialogue> dialogues = new List<Dialogue>();
                List<packagedNode> tempobj = new List<packagedNode>();
                while (!inportFile.EndOfStream)
                {
                    tempobj.Add(JsonUtility.FromJson<packagedNode>(inportFile.ReadLine()));
                }

                //for every packagedNode
                for (int j = 0; j < tempobj.Count; j++)
                {
                    //convert it into a Dialogue
                    dialogues.Add(ScriptableObject.CreateInstance<Dialogue>());
                    dialogues[j].prompt = tempobj[j].prompt;
                    dialogues[j].response = tempobj[j].response;
                    dialogues[j].next = new List<Dialogue>();
                    dialogues[j].start = tempobj[j].head;
                    dialogues[j].tree = newTree;
                    dialogues[j].name = tempobj[j].dialName;
                    dialogues[j].treeName = tempobj[j].treeName;
                }

                //now that all of the dialogues are made, put them into the proper folder.
                for (int j = 0; j < dialogues.Count; j++)
                {
                    AssetDatabase.CreateAsset(dialogues[j], "Assets/Resources/DialogueTree/Tree" + newTree + "/" + dialogues[j].name + ".asset");
                }

                // A dialogues's next[] field can be directly imported, so we have to assign it now.
                
                // Load all of the Objects from the new folder (reloading them just to be safe)
                Dialogues = Resources.LoadAll("DialogueTree/Tree" + newTree);

                // For each Dialogue
                for (int dial = 0; dial < Dialogues.Length; dial++)
                {
                    // Give it a new next[]
                    ((Dialogue)Dialogues[dial]).next = new List<Dialogue>();
                    Debug.Assert(((Dialogue)Dialogues[dial]).next != null, "failure in drawImport. failure to create next[]");

                    //for each next[] in the tempobj
                    for (int next = 0; next < tempobj[dial].next.Count; next++)
                    {
                        //for each Dialogue.(I have to iterate again because these are the Dialogues that will occupy the next[] field of dial)
                        for (int dial2 = 0; dial2 < Dialogues.Length; dial2++)
                        {
                            // If there is a next field with the same name as a Dialogue(dial2) that exists...
                            if (((Dialogue)Dialogues[dial2]).name == tempobj[dial].next[next])
                            {
                                // Add that Dialogue(dial2) to the next[] of the current Dialogue(dial)
                                ((Dialogue)Dialogues[dial]).next.Add((Dialogue)(Dialogues[dial2]));
                            }
                        }
                    }

                    // If the Dialogue object has a reponse[], but no corresponding next[], give it one
                    while (((Dialogue)Dialogues[dial]).next.Count != ((Dialogue)Dialogues[dial]).response.Count)
                    {
                        ((Dialogue)Dialogues[dial]).next.Add(null);
                    }
                   
                    Debug.Assert(((Dialogue)Dialogues[dial]).next.Count == ((Dialogue)Dialogues[dial]).response.Count,"Failure in drawImport," +
                        "response[].count doesnt equal next[].count");
                    Debug.Assert(((Dialogue)Dialogues[dial]).response.Count == tempobj[dial].response.Count, "Failure in drawImport," +
                        "response.count is not import properly");
                    Debug.Assert(((Dialogue)Dialogues[dial]).response == tempobj[dial].response,"Failure in drawImport" +
                        "response is not import properly");
                    Debug.Assert(((Dialogue)Dialogues[dial]).name == tempobj[dial].dialName,"Failure in drawImport" + 
                        "name is not import properly");
                    Debug.Assert(((Dialogue)Dialogues[dial]).prompt == tempobj[dial].prompt,"Failure in drawImport" + 
                        "prompt is not import properly");
                    Debug.Assert(((Dialogue)Dialogues[dial]).start == tempobj[dial].head , "Failure in drawImport" + 
                        "start is not import properly");
                    Debug.Assert(((Dialogue)Dialogues[dial]).treeName == tempobj[dial].treeName,"Failure in drawImport" + 
                        "treeName is not import properly");
                }
            }
            catch
            {
                Debug.Log("Failure in drawImport. Try threw an exception");
            }
        }
    }

    /// <summary>
    /// <c>drawExportButton</c>
    /// 
    /// Description: Draws the buttons above the Node View, used for exporting a tree.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: The button have been drawn on the customGUI window
    /// </summary>
    /// <returns> nothing </returns>
    void drawExportButton()
    {
        // Make the "export" button.
        if (GUILayout.Button("export"))
        {

            //create a new json string
            string json = "";
            
            //obtain the path to create a new file
            string path = ExportDialogGui();

            //if there is atleast 1 dialogue
            if (Dialogues.Length != 0)
            {
                //add it to the json
                json = JsonUtility.ToJson(package((Dialogue)Dialogues[0]));
            }

            //for every dialogue after the first
            for (int i = 1; i < Dialogues.Length; i++)
            {
                //make a new line, then add it
                json += "\n" + JsonUtility.ToJson(package((Dialogue)Dialogues[i]));
            }

            //put the json in a file
            File.WriteAllText(path, json);
        }
    }
}
#endif