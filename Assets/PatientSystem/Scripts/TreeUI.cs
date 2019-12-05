using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 
/// <c>Tree UI</c>
/// 
/// Description: This communicates with the tree UI. Gets and sets data to it.
/// 
/// Pre-condition: Needs to make use of CustomGUI.CS sub system.
/// 
/// Post-condition: creates a dialouge Tree UI that is able to be used by the user before compile time.
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// </authors>
public class TreeUI : MonoBehaviour
{

    /// <summary>
    /// Current tree we are at.
    /// </summary>
    public string currentTree;

    /// <summary>
    /// Current node we are at, starts at head.
    /// </summary>
    [HideInInspector]
    public Dialogue currentNode;

    /// <summary>
    /// A list of the Dialogue objects of the current tree (Object[] format).
    /// </summary>
    [HideInInspector]
    public UnityEngine.Object[] Dialogues;

    /// <summary>
    /// A list of tree names.
    /// </summary>
    [HideInInspector]
    public List<string> treeNames;

    /// <summary>
    /// The Animator object that hendles animations.
    /// </summary>
    public Animator animator;


    // Inbuilt Unity function. Called on creation.
    public void Awake()
    {
        Dialogues = Resources.LoadAll("DialogueTree");
        findTrees();

        //if the given tree is one of the trees that are in the Resources Folder
        if(treeNames.Contains(currentTree))
        {
            // Set the head node to the head of that tree.
            Dialogues = Resources.LoadAll("DialogueTree/Tree" + findID());
            List<Dialogue> dialList = convertToList(Dialogues);
            currentNode = findHead(dialList);
        }
        else
        {
            Debug.Log("Given tree name was not found");
        }

    }

    /// <summary>
    /// 
    /// <c>GetCurrentOptions</c>
    /// 
    /// Description:Returns the options we can current take.
    /// 
    /// pre-conditions:We must be on a valid node in the tree ui.
    /// 
    /// Post-condition: None
    /// 
    /// </summary>
    /// 
    /// <returns>The options we can take at the current node.</returns>
    public List<string> GetCurrentOptions()
    {
        return currentNode.response;
    }


    /// <summary>
    /// 
    /// <c>TakeOption</c>
    /// 
    /// Description: Traverses the tree down the coresponding index.
    /// 
    /// pre-conditions: We must be on a valid node in the tree ui. There must be an option to take.
    /// 
    /// post-conditions: The current node will be updated to be the node who is at the option.
    /// 
    /// </summary>
    /// 
    /// <param name="option">The branch to take.</param>
    /// <returns> NULL </returns>
    public void TakeOption(int option)
    {
        currentNode = currentNode.next[option];
    }

    /// <summary>
    /// Description: Returns the prompt at the current node we are at.
    /// 
    /// pre-conditions: We must be on a valid node in the tree ui.
    /// 
    /// Post-condition: None
    /// </summary>
    /// 
    /// <returns>The prompt at the current node.</returns>
    public string GetCurrentPrompt()
    {
        return currentNode.prompt;
    }


    /// <summary>
    /// Description: Runs the animation of the current node if there is one.
    /// 
    /// pre-conditions: We must be on a valid node in the tree ui.
    /// 
    /// Post-condition: None
    /// </summary>
    /// 
    /// <return> NULL </returns>
    public void RunAnim()
    {
        // A try catch to stop it from printing an error if there is no animation.
        try
        {
            animator.Play(currentNode.anim.name);
        }
        catch 
        {
            Debug.Log("there is no animation");
        }
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
        for (int i = 0; i < dialogues.Count; i++)
        {
            if (dialogues[i].start == true)
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
            if (!treeNames.Contains(((Dialogue)Dialogues[i]).treeName))
            {
                treeNames.Add(((Dialogue)Dialogues[i]).treeName);
            }
        }
        treeNames.Sort();
        return treeNames.Count;
    }

    /// <summary>
    /// <c>findID</c>
    /// 
    /// Description: Finds the treeID of he given treeName
    /// 
    /// Pre-condition: The Resources/DialogueTree folder exists
    /// 
    /// Post-condition: nothing
    /// 
    /// </summary>
    /// <returns>the Id of the treeName</returns>
    public int findID()
    {
        Dialogues = Resources.LoadAll("DialogueTree");

        // For every node
        for (int i = 0; i < Dialogues.Length; i++)
        {
            // If that node belongs to a tree that has not been found yet. 
            if (currentTree == ((Dialogue)Dialogues[0]).treeName)
            {
                return ((Dialogue)Dialogues[i]).tree;
            }
        }

        return -1;
    }
}
