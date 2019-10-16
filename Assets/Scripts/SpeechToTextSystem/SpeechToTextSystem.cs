using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechToTextSystem : MonoBehaviour
{
    [SerializeField]
    private Text hypothesis;

    [SerializeField]
    private Text recognitions;

    private DictationRecognizer dictationRecognizer;

    // Start is called before the first frame update
    public void StartSpeechToText()
    {
        dictationRecognizer = new DictationRecognizer();

        recognitions.text = "Say a phrase into the audio device...";
        if (hypothesis)
            hypothesis.text = "";

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            recognitions.text = text + "\n";
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            if (hypothesis)
                hypothesis.text += text;
        };

        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        dictationRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}