using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


/// <summary>
/// <c>SpeechToTextSystem</c>
/// 
/// Description: SpeechToTextSystem takes speech from the player and converts it into useable
/// text(string data type)
/// 
/// Pre-condition: Audio input devices
/// 
/// Post-condition: Returns speech text.
/// </summary>
/// 
/// <author>Matt Radke, James Scarrow</author>

public class SpeechToTextSystem : MonoBehaviour
{
    // a button to press, set this in the inspector.
    public KeyCode toggleKey;

    // text spoken.
    public Text text;

    // reference to the dialougeTree.
    public IDialogueTree dialogueTree;

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

    /// <summary>
    /// <c>OnApplicationQuit</c>
    /// 
    /// Description:special unity function, when the unity stops running we want to dispose of the 
    /// diction recognizer, as to not cause errors.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: removes diction recognizer
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    private void OnApplicationQuit()
    {
        dictationRecognizer.Stop(); 
        dictationRecognizer.Dispose();

    }

    /// <summary>
    /// <c>start</c>
    /// 
    /// Description: Builds data before game begins for Speech-to-text system
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: set up the data required to run the dictationReconginzer. Once the 
    /// dictationReconginzer is up and running we can begin logging what the player says.
    /// only need to set up this content once, so at beginning of play time we will do this.
    /// Once phrase is spoken, we will log it, and then send it to the dialogueTree.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        // When speech has been recognized.
        dictationRecognizer.DictationResult += OnDictationResult;

        text.gameObject.SetActive(false);

        if (Microphone.devices.Length == 0)
        {
            text.text = "Warning: There are no audio input devices connected!";
            text.gameObject.SetActive(true);
        }

        dictationRecognizer.Start();
    }

    public void OnDictationResult(string text, ConfidenceLevel confidence)
    {
            phraseSpoken = text;
            // Write the text to the log.
            GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(phraseSpoken);
            this.text.text = phraseSpoken;

            Debug.Log("what is inside the Phrase Spoken:" + phraseSpoken); 
            if (dialogueTree != null)
                dialogueTree.inTree(phraseSpoken);
    }

// updates frame.
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