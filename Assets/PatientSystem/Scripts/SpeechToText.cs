using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

/// <summary>
/// <c>SpeechToText</c>
/// Description: This handles an audio file as input and will return back a string.
/// 
/// Pre-condition: Audio system needs to be implemented to detect possible audio devices. 
/// 
/// Post-condition: system handels  all player input and produces a string equaliviant of what they said. 
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// Matt Radke
/// </authors>
public class SpeechToText : MonoBehaviour
{
    
    // The Language Engine of the system.
    public LanguageEngine LE;

    #region WindowsSpeechRec
    // Converts speech to text.
    private DictationRecognizer dictationRecognizer;
    
    /// <summary>
    /// Called when the game is started. <para/>
    /// We setup the voice recog
    /// </summary>
    private void Start()
    {
        Debug.Log(string.Format("SpeechToText::Start"));

        // create the DictationRecognizer, it will start recog text from the mic.
        dictationRecognizer = new DictationRecognizer();

        // When speech has been recognized.
        dictationRecognizer.DictationResult += OnDictationResult;

        // make sure to rerun the dictation if it finishes
        dictationRecognizer.DictationComplete += (DictationCompletionCause cause) =>
        {
            Debug.Log("DictationCompletionCause: " + cause);

            if (cause != DictationCompletionCause.Canceled)
                dictationRecognizer.Start();
        };

        // start it now.
        dictationRecognizer.Start();
    }

    /// <summary>
    /// When windows recognizes text, this will be called.
    /// </summary>
    /// <param name="text">the text that was said</param>
    /// <param name="confidence">confidence of the recongized text</param>
    private void OnDictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.Log(string.Format("SpeechToText::OnDictationResult: text: {0}, confidence: {1}", text, confidence));

        // send the string to the lang engine
        Debug.Assert(LE != null);
        LE.RecieveInput(text);
    }

    /// <summary>
    /// Called when destroied. Clean up the speech reg.
    /// </summary>
    private void OnDestroy()
    {
        Debug.Assert(dictationRecognizer != null);

        Debug.Log(string.Format("SpeechToText::OnDestroy"));

        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }
    #endregion

    #region API
    /// <summary>
    /// <c>ReceiveAudioFile</c>
    /// 
    /// Description: Receives an audio file path to then send the interpreted string to the LanguageEngine.
    /// 
    /// pre-conditions: The file must exist and be a valid audio file.
    ///
    /// Post-Conditions: None
    /// </summary>
    /// <param name="fileName">The audio file to interpret.</param>
    ///  <returns> NULL </returns>
    public void ReceiveAudioFile(string fileName)
    {
        Debug.Log(string.Format("SpeechToText::ReceiveAudioFile: fileName: {0}", fileName));

        string whatWasSaid = AudioToString(fileName);

        Debug.Log(string.Format("SpeechToText::ReceiveAudioFile: whatWasSaid: {0}", whatWasSaid));

        LE.RecieveInput(whatWasSaid);
    }

    /// <summary>
    /// <c>AudioToString</c>
    /// 
    /// Description:Returns a string from a given audio file.
    /// 
    /// pre-conditions: The file must exist and be a valid audio file.
    /// 
    /// Post-Conditions: None
    /// </summary>
    /// <param name="fileName">The audio file to interpret.</param>
    /// <returns>The Speech To Text</returns>
    public string AudioToString(string fileName)
    {
        return "hello";
    }
    #endregion
}
