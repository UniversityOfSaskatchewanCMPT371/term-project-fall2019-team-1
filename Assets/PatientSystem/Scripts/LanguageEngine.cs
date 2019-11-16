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
    public TreeUI tree;

    // The text to speech system.
    public TextToSpeech TTS;

    // the patient system.
    public PatientSystem patientSystem;

    public bool wordComparison;

    public bool KMPComparison; 

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
        List<string> options = tree.GetCurrentOptions();


        // now get the decision to make
        int decisionIndex;
        try
        {
            decisionIndex = BestDecision(input, options);

            Debug.Assert(decisionIndex != -1, "Language Engine not setup correctly within unity!"); 
        }
        catch (NoBestDecision e)
        {
            Debug.Log(string.Format("LanguageEngine::RecieveInput: NoBestDecision: {0}", e));
            return;
        }
        Debug.Assert(decisionIndex >= 0 && decisionIndex < options.Count, "decisionIndex is out of bounds of options");

        // log our options
        Debug.Log(string.Format("LanguageEngine::RecieveInput: decision: {0}", decisionIndex));

        // with the decision, traverse the tree.
        tree.TakeOption(decisionIndex);

        // now say the next prompt
        TTS.RunSpeech(tree.GetCurrentPrompt());
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
    /// <param name="treeData">All responses at the current node!</param>
    /// <returns>The index of the option to be taken. returns -1 if no string search algorithm checked.</returns>
    public int BestDecision(string input, List<string> treeData)
    {
       
        if (this.wordComparison)
        {
          
            // finds the custom gui dialogue prompts on this current tree.
       

            // builds a Jaggered Array(different from 2DArray in c#).
            string[][] options = new string[treeData.Count][];


            int counter = 0;

            // we can state how many words are in a box at a later time.
            while (counter < treeData.Count)
            {
                string[] wordsBrokenDown = Regex.Split(treeData[counter], " ");

                options[counter] = wordsBrokenDown;

                counter++;

            }
       


            return wordComp(input, options);
        }

        else if (this.KMPComparison)
        {

            return KMPcomp(input, treeData);

        }
        else
        {
            return -1; 
        }
    }

    /// <summary>
    /// description: Algorithm which finds words similar in a responses given. It finds words that are the same
    /// then we will find the percentage of words that are the same in a given response. 
    /// 
    /// pre-condition: input cannot be null, options must be turned into a jaggered Array.
    /// 
    /// post-condition: returns a index of the correct node to head down.
    /// 
    /// </summary>
    /// <param name="input">string containing the what the user stated.</param>
    /// <param name="options">the different words in the NPC prompt.</param>
    /// <returns>a index to the path the NPC will take in conversation.</returns>
    private int wordComp(string input, string [][] options)
    {
        // break down user input into seperate words.
        string[] wordBrokenDown = Regex.Split(input, " ");

        // Have a previous high response percentage. If anything beats this precentage, we will change it to,
        // new response.
        double prevIndexPercentage = -1;
        double currentWordPercent = -1;
        double Holder = -1;


        int prevIndex = -1;
        int numbOfSameWords = 0;



        // count through each possible option.
        for (int curUserResp = 0; curUserResp < options.Length; curUserResp++)
        {
            numbOfSameWords = 0;
            currentWordPercent = -1;
            Holder = -1;

            for (int wordsInUserResp = 0; wordsInUserResp < options[curUserResp].Length; wordsInUserResp++)
            {

                for (int userInputWords = 0; userInputWords < wordBrokenDown.Length; userInputWords++)
                {
                    //Debug.Log("input: " + wordBrokenDown[userInputWords] + " options: " + options[curUserResp][wordsInUserResp]);

                    // if a word matches to another word, then we know it could possible be a match to a response.
                    if (wordBrokenDown[userInputWords].Equals(options[curUserResp][wordsInUserResp]))
                    {
                        numbOfSameWords += 1;

                        //Debug.Log("we making it within this cond? " + numbOfSameWords);

                    }


                }

            }


            //Debug.Log("what is inside of numbword " + numbOfSameWords);

            // figure out how many words where discovered to be in a certian response.
            Holder = numbOfSameWords / (double)wordBrokenDown.Length;

            currentWordPercent = Holder * 100; 

            Debug.Log("currentPercent: " + currentWordPercent + " with the following input: " + input + "this is numbOfWords: " + numbOfSameWords + "word broken down: " + wordBrokenDown.Length + "this is holder: " + Holder);


            if (prevIndex == -1 && currentWordPercent != 0)
            {

                prevIndex = curUserResp;

                prevIndexPercentage = currentWordPercent;

                Debug.Log("prevPercent: " + prevIndexPercentage + " with the following input: " + input);

            }
            else if (prevIndexPercentage < currentWordPercent && currentWordPercent > 0)
            {
                // if the current word response is bigger then then the last we will change it.
                prevIndex = curUserResp;

                prevIndexPercentage = currentWordPercent;
                Debug.Log("prevPercent: HAS BEEN UPDATED " + prevIndexPercentage + " with the following input: " + input);

            }

        }


        //throw new NoBestDecision();
        Debug.Assert(prevIndex != -1, "prevIndex did not get changed within the calculation above");

        return prevIndex;

    }

    /// <summary>
    /// Description: KMP string search algorthim, we use a pattern to search to see what is the same in the
    ///  given text. Once we have found a same match in the string then we can give a confidence value due to 
    ///  what matches the Pattern string.
    ///  
    /// pre condition: Text to search cannot be null, need content from GUI tree.
    /// 
    /// post condition: returns a integer number to head down a certain path.
    /// 
    /// code copied and adapted from: https://www.geeksforgeeks.org/kmp-algorithm-for-pattern-searching/
    /// 
    /// </summary>
    /// <param name="pattern"> The string pattern we are searching for in a certain piece of text.</param>
    /// <param name="TextSearching">the text that we are searching through!</param>
    /// <returns>returns the index of a node that we want to head down in.</returns>
    private int KMPcomp(string pattern, List<string>TextSearching)
    {
        int M = pattern.Length;
        int [] lps = LPS(pattern, M);
        int patternIndex = -1;
        int textIndex = -1;
        int ListCounter = 0;

        string contentText = null; 

        while(ListCounter < TextSearching.Count)
        {
            patternIndex = 0;
            textIndex = 0;
            contentText = TextSearching[ListCounter]; 

            while (textIndex < contentText.Length)
            {

                if(pattern[patternIndex] == contentText[textIndex])
                {
                    patternIndex++;
                    textIndex++; 

                }

                if (patternIndex == M)
                {
                    patternIndex = lps[patternIndex - 1];
                }
                else if (textIndex < contentText.Length && pattern[patternIndex] != contentText[textIndex])
                {


                    if(patternIndex != 0)
                    {
                        patternIndex = lps[patternIndex - 1];
                    } 
                    else
                    {
                        textIndex ++; 
                    }
                }


            }

            ListCounter++; 
        }

        return 1;
    }
    /// <summary>
    /// Description: LPS stands for Longest Prefix and Suffix in a common pattern. Basically this helper function
    /// finds the common Prefix and suffix in a pattern and stores it into a array.
    /// 
    /// pre-condition: the string pattern cannot be null, pattern length > 0
    /// 
    /// post-condition: returns the LPS array container for the given pattern. 
    /// 
    /// </summary>
    /// <param name="pat"> the pattern that we want to see what the common prefixs and suffixs are.</param>
    /// <param name="pattLen">the length of the pattern given.</param>
    /// <returns>returns a new array containing the LPS array.</returns>
    private int [] LPS(string pat, int pattLen)
    {
        int[] lps = new int[pattLen];

        int LenPreSuf = 0;
        int counter = 0;
        lps[0] = 0; 


        while(counter < pattLen)
        {
            if (pat[counter] == pat[LenPreSuf])
            {
                LenPreSuf++;
                lps[counter] = LenPreSuf;

                counter++; 
            }
            else
            {

                if(LenPreSuf != 0)
                {
                    LenPreSuf = lps[LenPreSuf - 1];
                }
                else
                {
                    lps[counter] = LenPreSuf; 

                    counter++;
                }

            }


        }

        return lps; 
    }


    /// <summary>
    /// On Startup, say the prompt.
    /// </summary>
    private void Start()
    {
        Debug.Assert(tree != null);
        Debug.Assert(tree.currentNode != null);

        TTS.RunSpeech(tree.GetCurrentPrompt());
    }
}
