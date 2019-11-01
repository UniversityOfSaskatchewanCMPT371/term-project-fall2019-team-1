using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is takes in a String from SpeechToText, and uses it to traverse the TreeUI.
/// </summary>
public class LanguageEngine : MonoBehaviour
{
    /// <summary>
    /// The tree UI, setup in the inspector, or mocked.
    /// </summary>
    public TreeUI treeUI;

    /// <summary>
    /// The system logger.
    /// </summary>
    public Log log;

    /// <summary>
    /// The system debug logger.
    /// </summary>
    public DebugLog debugLog;

    /// <summary>
    /// Recieves input from the SpeechToText output.
    /// </summary>
    /// <param name="input">The input from the stt.</param>
    public void RecieveInput(string input)
    {
        // log our input
        log.WriteToLog(string.Format("LanguageEngine::RecieveInput: input: '{0}'", input));

        // get options we have at current node.
        string[][] options = treeUI.GetCurrentOptions();

        // log our options
        log.WriteToLog(string.Format("LanguageEngine::RecieveInput: options: {0}", options));

        // now get the decision to make
        int decisionIndex;
        try
        {
            decisionIndex = BestDecision(input, options);
        }
        catch (NoBestDecision e)
        {
            log.WriteToLog(string.Format("LanguageEngine::RecieveInput: NoBestDecision: {0}", e));
            return;
        }
        Debug.Assert(decisionIndex >= 0 && decisionIndex < options.Length, "decisionIndex is out of bounds of options");

        // log our options
        log.WriteToLog(string.Format("LanguageEngine::RecieveInput: decision: {0}", decisionIndex));

        // with the decision, traverse the tree.
        treeUI.TakeOption(decisionIndex);
    }

    /// <summary>
    /// Will return the best decision given the input string and the options.
    /// </summary>
    /// <param name="input">A string to compare to the options.</param>
    /// <param name="options">The options, an array of array of strings.</param>
    /// <returns>The index of the option to be taken.
    /// </returns>
    public int BestDecision(string input, string[][] options)
    {
        throw new NoBestDecision();
    }
}
