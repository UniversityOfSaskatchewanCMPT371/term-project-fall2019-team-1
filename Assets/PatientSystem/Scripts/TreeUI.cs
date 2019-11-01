using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This communicates with the tree UI. Gets and sets data to it.
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class TreeUI : MonoBehaviour
{
    /// <summary>
    /// Returns the options we can current take.
    /// </summary>
    /// <preconditions>
    /// We must be on a valid node in the tree ui.
    /// </preconditions>
    /// <returns>The options we can take at the current node.</returns>
    public string[][] GetCurrentOptions()
    {
        return null;
    }

    /// <summary>
    /// Traverses the tree down the coresponding index.
    /// </summary>
    /// <preconditions>
    /// We must be on a valid node in the tree ui.
    /// There must be an option to take.
    /// </preconditions>
    /// <postconditions>
    /// The current node will be updated to be the node who is at the option.
    /// </postconditions>
    /// <param name="option">The branch to take.</param>
    public void TakeOption(int option)
    {

    }

    /// <summary>
    /// Returns the prompt at the current node we are at.
    /// </summary>
    /// <preconditions>
    /// We must be on a valid node in the tree ui.
    /// </preconditions>
    /// <returns>The prompt at the current node.</returns>
    public string GetCurrentPrompt()
    {
        return null;
    }
}
