using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToTextSystem : MonoBehaviour
{
    // Converts speech to text.
    private DictationRecognizer dictationRecognizer;

    // Start is called before the first frame update.
    public void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        // When speech has been recognized.
        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            // Write the text to the log.
            GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(text);
        };

        dictationRecognizer.Start();
    }
}