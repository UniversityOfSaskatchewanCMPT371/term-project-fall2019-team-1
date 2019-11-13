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
    int GUIwidth;
    int GUIheight;
   
    //variables that affect the lines thickness, colour, etc...
    int shadowLine = 1;
    int shadowEdge = 4;
    Color shadowCol = Color.blue;
    Color lineCol = Color.black;

    // A list of prompt windows.
    public List<Rect> dialwindows = new List<Rect>();

    // A list of the response windows.
    public List<Rect> responsewindows = new List<Rect>();
    
    // A list of all Dialogue objects.
    public UnityEngine.Object[] Dialogues;

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

        // Obtain all of the Dialogue Objects.
        Dialogues = UnityEngine.Resources.LoadAll("DialogueTree");
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
        max_nodes_x = 0;

        Debug.Assert(max_nodes_x > 0);


        findTrees();
        Debug.Assert(trees.Count >= 0, "Error in OnGUI, failure to obtain number of trees");
        Debug.Assert(trees != null, "Error in OnGUI, trees is null");



        // If a tree was found.
        if (trees.Count > 0 && currentTree < 0)
        {
            // The first tree will be the default tree.
            currentTree = trees[0];
            Debug.Assert(currentTree > 0, "failure to set tree to first tree");
        }

        // Load the dialogue objects for the given tree.
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + currentTree);
        Debug.Assert(Dialogues != null, "Error in OnGUI, failure to obtain Dialogues");

        //Refresh and atLayer
        atLayer.Clear();
        Debug.Assert(atLayer != null, "Error in OnGUI, failure to refresh atLayer");
        Debug.Assert(atLayer.Count == 0, "Error in OnGUI, failure to refresh atLayer");

        EditorGUILayout.BeginHorizontal();
        
        // The scrollbar for the list of trees.
        scrollBar = GUILayout.BeginScrollView(scrollBar, false, true, GUILayout.Width(120));

        // For every tree.
        for(int i = 0; i < trees.Count; i++)
        {
            GUILayout.BeginHorizontal();

            if (!treesToDelete.Contains(trees[i]))
            {
                // Make a button for that tree.
                GUI.backgroundColor = Color.white;
                // Make the button colour for the current tree cyan.
                if(currentTree == trees[i])
                {
                    GUI.backgroundColor = Color.cyan;
                }
                if (GUILayout.Button("Tree " + trees[i]))
                {
                    currentTree = trees[i];
                    Debug.Log(currentTree);
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
                if (!trees.Contains(i))
                {
                    // Make the folder.
                    AssetDatabase.CreateFolder("Assets/Resources/DialogueTree", "Tree" + i);
                    
                    // Make the head dialogue.
                    Dialogue newDial = new Dialogue();
                    Debug.Assert(newDial != null, "failure in OnGUI, newDial creation failed");
                    newDial.tree = i;
                    newDial.start = true;

                    AssetDatabase.CreateAsset(newDial, "Assets/Resources/DialogueTree/Tree" + i + "/" + "Dialogue.asset");

                    // Add the new tree to found.
                    trees.Add(i);
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
            int i = 1;
            try
            {
                string path = ImportDialogGui();
                int newTree = 0;
                bool treeAdded = false;
               

                while (!treeAdded)
                {
                    Debug.Log(i);

                    // Make sure that tree doesnt already exist.
                    if (!trees.Contains(i))
                    {
                        // Make the folder.
                        AssetDatabase.CreateFolder("Assets/Resources/DialogueTree", "Tree" + i);

                        // Add the new tree to found.
                        trees.Add(i);
                        treeAdded = true;
                    }
                    i++;
                }
                newTree = i;

                //grab the file
                StreamReader inportFile = new StreamReader(path);

                //read the file
                List<Dialogue> dialogues = new List<Dialogue>();
                List<tempObject> tempobj = new List<tempObject>();
                while (!inportFile.EndOfStream)
                {
                    tempobj.Add(JsonUtility.FromJson<tempObject>(inportFile.ReadLine()));
                }

                //for every tempObj
                for (int j = 0; j < tempobj.Count; j++)
                {


                    //convert it into a Dialogue
                    dialogues.Add(new Dialogue());
                    dialogues[j].prompt = tempobj[j].prompt;
                    dialogues[j].response = tempobj[j].response;
                    dialogues[j].start = tempobj[j].head;
                    dialogues[j].tree = tempobj[j].tree;

                }

                //now that all of the dialogues are made, put them into the proper folder.
                for (int j = 0; j < dialogues.Count; j++)
                {
                    AssetDatabase.CreateAsset(dialogues[j], "Assets/Resources/DialogueTree/Tree" + (newTree - 1) + "/Dialogue" + (j + 1) + ".asset");

                }

                //change each Dialogues.next so that it matches the tempobj.next index   
                Dialogues = Resources.LoadAll("DialogueTree/Tree" + (newTree - 1));
                Debug.Log(newTree - 1);

                //for each dialogue
                for (int j = 0; j < Dialogues.Length; j++)
                {
                    ((Dialogue)Dialogues[j]).next = new List<Dialogue>();

                    //for each next[] in the tempobj
                    for (int k = 0; k < tempobj[j].next.Count; k++)
                    {
                        ((Dialogue)Dialogues[j]).next.Add((Dialogue)(Dialogues[tempobj[j].next[k]]));
                    }
                }
            }
            catch
            {
                AssetDatabase.DeleteAsset("Assets/Resources/DialogueTree" + "/Tree" + (i-1));
                AssetDatabase.Refresh();
                trees.Remove(i - 1);
             }
        }

        if(GUILayout.Button("export"))
        {
            string json = "";
            string path = ExportDialogGui();

            Debug.Log(path);

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
        EditorGUILayout.EndHorizontal();

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
        NodeLayer.Clear();


        for (int i = 0; i < Dialogues.Length; i++)
        {
            // If this node belongs to the current tree, and is the head of that tree.
            if ((((Dialogue)Dialogues[i]).tree == treeNum) && (((Dialogue)Dialogues[i]).start == true))
            {
                head = (Dialogue)Dialogues[i];
                goto Found; 
            }
            else
            {
                // If it cant be found, set it ot the first one that appears.
                head = (Dialogue)Dialogues[0];
            }
        }

        Found:
        atLayer.Add(0);
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
        // TODO: have node start below its parent.

       

        // Create the position of the node.
        Rect nodeRect = new Rect(dial.response.Count *100 + 20 + biggestX, layer * 100 + 10, 175, 65);

        // Adjusts the x-coordinate to adjust scrolling
        if (max_nodes_x < nodeRect.x)
        {
            max_nodes_x = nodeRect.x;
        }
        Rect textRect = new Rect(nodeRect.x, nodeRect.y + 20, nodeRect.width, 20);
        Rect buttonRect = new Rect(nodeRect.x, textRect.y + 25, nodeRect.width, 20);
        Rect exitRect = new Rect(nodeRect.x + nodeRect.width - 15, nodeRect.y, 15, 15);

        dialwindows.Add(nodeRect);

        NodeLayer.Add(layer);

        // If this node is not going to be deleted.


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
    /// <param name="layer">the layer of the node</param>
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
        Rect nodeRect = new Rect((atLayer[layer] * 200) + 20, layer * 100+10, 175, 70);
        if (max_nodes_x < nodeRect.x)
        {
            max_nodes_x = nodeRect.x;
        }
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


    //TODO: find a better curve function.

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

        return EditorUtility.SaveFilePanel("Export Tree To File", path, currentTree + ".json", "json");
    }


    class tempObject
    {
        public string prompt;
        public List<string> response;
        public List<int> next;
        public int tree;
        public bool head;
    }

    tempObject package(Dialogue dialogue)
    {
        //copy the prompt and responses
        tempObject temp = new tempObject
        {
            prompt = dialogue.prompt,
            response = dialogue.response,
            next = new List<int>(),
            tree = dialogue.tree,
            head = dialogue.start

        };


        //for every response in the dialogue
        for (int i = 0; i < dialogue.next.Count; i++)
        {
            //find the index of dialogue.next, and set the temp.next to that index
            if (dialogue.next[i] != null)
                temp.next.Add(getNodeIndex(dialogue.next[i]));
        }

        return temp;
    }
}
#endif