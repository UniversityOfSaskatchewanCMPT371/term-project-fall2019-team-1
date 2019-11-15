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
    /// Current node we are at, starts at head.
    /// </summary>
    public Dialogue currentNode;


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
}
