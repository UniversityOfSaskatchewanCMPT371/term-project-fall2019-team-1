using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToTextSystem : MonoBehaviour
{
    private string phraseSpoken;

    public string MyProperty
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
        };

        dictationRecognizer.Start();
    }
}