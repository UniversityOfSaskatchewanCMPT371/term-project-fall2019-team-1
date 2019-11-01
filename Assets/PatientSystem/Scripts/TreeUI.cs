using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This communicates with the tree UI. Gets and sets data to it.
/// </summary>
public class TreeUI : MonoBehaviour
{
    /// <summary>
    /// Returns the options we can current take.
    /// </summary>
    /// <returns>The options we can take at the current node.</returns>
    public string[][] GetCurrentOptions()
    {
        return null;
    }

    /// <summary>
    /// Traverses the tree down the coresponding index.
    /// </summary>
    /// <param name="option">The branch to take.</param>
    public void TakeOption(int option)
    {

    }

    /// <summary>
    /// Returns the prompt at the current node we are at.
    /// </summary>
    /// <returns>The prompt at the current node.</returns>
    public string GetCurrentPrompt()
    {
        return null;
    }
}
