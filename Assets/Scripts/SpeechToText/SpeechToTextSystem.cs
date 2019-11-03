using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


/**
 * Authors: Matt Radke, James Scarrow
 * 
 * SpeechToTextSystem takes speech from the player and converts it into useable
 * text(string data type)
 * 
 */
public class SpeechToTextSystem : MonoBehaviour
{
    // a button to press, set this in the inspector.
    public KeyCode toggleKey;

    // text spoken.
    public Text text;

    // reference to the dialougeTree.
    public DialogueTree dialogueTree;

    // the phrase that was spoken, this value is the acutal value.
    private string phraseSpoken;

    // getter/setter for phraseSpoken!
    public string PhraseSpoken
    {
        get {
            if (phraseSpoken != null)
            {
                return phraseSpoken;
            }
            else
            {
                throw new UnassignedReferenceException("No recognized speech.");
            }
        }
    }

    // Converts speech to text.
    private DictationRecognizer dictationRecognizer;

    /**
     * OnApplicationQuit():
     * pre:none
     * post: special unity function, when the unity stops running we want to dispose of the 
     * diction reconginzer, as to not cause errors.
     * 
     * return: none void.
     */
    private void OnApplicationQuit()
    {
        dictationRecognizer.Stop(); 
        dictationRecognizer.Dispose(); 

    }

    /**
     * start():
     * 
     * pre: none
     * 
     * post: setup the data required to run the dictationReconginzer. Once the 
     * dictationReconginzer is up and running we can begin logging what the player says.
     * only need to setup this content once, so at beginning of play time we will do this.
     * Once phrase is spoken, we will log it, and then send it to the dialogueTree. 
     * 
     * return: nothing void. 
     */
    public void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        // When speech has been recognized.
        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            phraseSpoken = text;
            // Write the text to the log.
            GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(phraseSpoken);
            this.text.text = phraseSpoken;
           if (dialogueTree != null)
            dialogueTree.inTree(phraseSpoken);
        };

        text.gameObject.SetActive(false);

        if (Microphone.devices.Length == 0)
        {
            text.text = "Warning: There are no audio input devices connected!";
            text.gameObject.SetActive(true);
        }

        dictationRecognizer.Start();
    }

    /**
     * update():
     * pre: none
     * post: checks to see if the toggle key was pressed, if it was activate the debug log. Else unactivate it.
     * return: none void.
     */ 
    public void Update()
    {

        //Debug.Log("this is a test to see if pausing happens!"); 
        /*if (Input.GetKeyDown(toggleKey))
        {
            if (text.gameObject.activeSelf)
            {


                text.gameObject.SetActive(false);
            }
            else
            {

                text.gameObject.SetActive(true); 

            }
            
        }*/
        
    }
}