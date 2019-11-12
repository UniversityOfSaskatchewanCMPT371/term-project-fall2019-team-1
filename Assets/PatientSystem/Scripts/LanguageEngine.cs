using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// <c>Language Engine</c>
/// 
/// Descriptions: This is takes in a String from SpeechToText, and uses it to traverse the TreeUI.
/// 
/// pre-condition: Needs to be able to take input from Speech-To-Text system 
/// 
/// Post-Condition: Calculates a best branch option to progress the dialouge tree.
/// 
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// </authors>
public class LanguageEngine : MonoBehaviour
{

    // The tree UI, setup in the inspector, or mocked.

    public TreeUI treeUI;

    /// <summary>
    /// 
    /// <c>Recieve Input</c>
    /// 
    /// Descriptions: Recieves input from the SpeechToText output.
    /// 
    /// preconditions: We must be on a valid node in the tree ui.
    /// 
    /// postconditions: The tree ui current node will be updated.
    /// 
    /// </summary>
    /// 
    /// <param name="input">The input from the stt.</param>
    /// <returns> NULL </returns>
    public void RecieveInput(string input)
    {
        // log our input
        Debug.Log(string.Format("LanguageEngine::RecieveInput: input: '{0}'", input));

        // get options we have at current node.
        string[][] options = treeUI.GetCurrentOptions();

        // log our options
        Debug.Log(string.Format("LanguageEngine::RecieveInput: options: {0}", options));

        // now get the decision to make
        int decisionIndex;
        try
        {
            decisionIndex = BestDecision(input, options);
        }
        catch (NoBestDecision e)
        {
            Debug.Log(string.Format("LanguageEngine::RecieveInput: NoBestDecision: {0}", e));
            return;
        }
        Debug.Assert(decisionIndex >= 0 && decisionIndex < options.Length, "decisionIndex is out of bounds of options");

        // log our options
        Debug.Log(string.Format("LanguageEngine::RecieveInput: decision: {0}", decisionIndex));

        // with the decision, traverse the tree.
        treeUI.TakeOption(decisionIndex);
    }

    /// <summary>
    /// 
    /// <c>Best Decision</c>
    /// 
    /// 
    /// Description: Will return the best decision given the input string and the options.
    /// 
    /// Pre-conditions: None
    /// 
    /// Post-conditions: None
    /// 
    /// </summary>
    /// 
    /// <param name="input">A string to compare to the options.</param>
    /// <param name="options">The options, an array of array of strings.</param>
    /// <returns>The index of the option to be taken.</returns>
    public int BestDecision(string input, string[][] options)
    {
        throw new NoBestDecision();
    }
}
