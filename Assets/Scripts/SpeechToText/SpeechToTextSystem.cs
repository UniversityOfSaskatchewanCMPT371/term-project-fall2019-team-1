using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechToTextSystem : MonoBehaviour
{
    public KeyCode toggleKey;

    public Text text;

    public DialogueTree dialogueTree;

    private string phraseSpoken;

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


    private void OnApplicationQuit()
    {
        dictationRecognizer.Stop(); 
        dictationRecognizer.Dispose(); 

    }

    // Start is called before the first frame update.
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

    public void Update()
    {

        //Debug.Log("this is a test to see if pausing happens!"); 
        if (Input.GetKeyDown(toggleKey))
        {
            if (text.gameObject.activeSelf)
            {
                text.gameObject.SetActive(false);
            } else
            {
                text.gameObject.SetActive(true);
            }
        }
    }
}