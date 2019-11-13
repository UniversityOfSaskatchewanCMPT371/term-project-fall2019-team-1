using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; 


/// <summary>
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
/// Mason Demerais, Matt Radke
/// </authors>
/// 


public class LanguageEngine : MonoBehaviour
{

    // The tree UI, setup in the inspector, or mocked.

    public TreeUI treeUI;


    // The system logger.

    public Log log;


    // The system debug logger.

    public DebugLog debugLog;

    private string testString = "hello"; 



    /// <summary>
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
        // break down user input into seperate words.
        string[] wordBrokenDown = Regex.Split(input, " ");
        
        // Have a previous high response percentage. If anything beats this precentage, we will change it to,
        // new response.
        double prevIndexPercentage = -1;
        double currentWordPercent = -1; 


        int prevIndex = -1;
        int numbOfSameWords = 0; 

        

        // count through each possible option.
        for(int curUserResp = 0; curUserResp < options.Length; curUserResp++)
        {
            numbOfSameWords = 0;
            currentWordPercent = -1;

           
            for (int wordsInUserResp = 0; wordsInUserResp < options[curUserResp].Length; wordsInUserResp++)
            {
      
                for (int userInputWords = 0; userInputWords < wordBrokenDown.Length; userInputWords++)
                {
                    //Debug.Log("input: " + wordBrokenDown[userInputWords] + " options: " + options[curUserResp][wordsInUserResp]);

                    // if a word matches to another word, then we know it could possible be a match to a response.
                    if (wordBrokenDown[userInputWords].Equals(options[curUserResp][wordsInUserResp]))
                    {
                        numbOfSameWords += 1;
                       // Debug.Log("we making it within this cond? " + numbOfSameWords);

                    }


                }
             
            }


            // Debug.Log("what is inside of numbword" + numbOfSameWords);

            // figure out how many words where discovered to be in a certian response.
            currentWordPercent = ((numbOfSameWords / wordBrokenDown.Length) * 100);

            //Debug.Log("currentPercent: " + currentWordPercent); 

       
            if (prevIndex == -1)
            {

                prevIndex = curUserResp;

                prevIndexPercentage = currentWordPercent;

            }
            else if (prevIndexPercentage < currentWordPercent)
            {
                // if the current word response is bigger then then the last we will change it.
                prevIndex = curUserResp;

            }

        }


        //throw new NoBestDecision();
        Debug.Assert(prevIndex != -1, "prevIndex did not get changed within the calculation above"); 

        return prevIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("attempting to build string array of different words!");

            RecieveInput(testString);
        }

       // print("test");     
    }
}
