using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading;

/// <summary>
/// <c>TextToSpeech</c>
/// 
/// Description: This receives a string to then be converted to an audio file.
/// 
/// Pre-condition: needs to have Language engine to send the string to it.
/// 
/// Post-condition: Produces audio in a scene.
/// 
/// </summary>
/// <authors>
/// Matt Radke
/// Mason Demerais
/// </authors>
public class TextToSpeech : MonoBehaviour
{
    // The audio output object of the system.
    public AudioOutput audioOutput;

    #region SpeechLib
    // Voice, uses speechLib to produce speech from a text input.
    private SpVoice voice;

    /// <summary>
    /// Inits the speech lib object, this needs to be on the awake (before Start) because the LE will call RunSpeech on Start.
    /// </summary>
    private void Awake()
    {
        Debug.Log(string.Format("TextToSpeech::Start"));

        voice = new SpVoice();
    }

    /// <summary>
    /// Kills the voice when its destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log(string.Format("TextToSpeech::OnDestroy"));
        Debug.Assert(voice != null);

        voice.Pause();
    }

    /// <summary>
    /// Speaks the string given to it.
    /// </summary>
    /// <param name="toBeSaid">The words to be heard on the speaker directly.</param>
    public void RunSpeech(string toBeSaid)
    {
        Debug.Assert(voice != null);
        Debug.Assert(toBeSaid != null);

        Debug.Log(string.Format("TextToSpeech::RunSpeech: toBeSaid: {0}", toBeSaid));

        // speak the string asyncly
        voice.Speak(toBeSaid, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }

    #endregion

    #region API
    /// <summary>
    /// 
    /// <c>ReceiveText</c>
    /// Description: Converts the textToSay to an audio file and then plays that file to the audioOutputter.
    /// Pre-conditions: None
    /// post-conditions: None
    /// 
    /// </summary>
    /// 
    /// <param name="textToSay">The words to be heard.</param>
    ///  <returns> NULL </returns>

    public void ReceiveText(string textToSay)
    {
    }

    /// <summary>
    /// <c>TextToAudio</c>
    /// Description: Writes to a file that is a TextToSpeech of the input textToSay.
    /// Pre-conditions: None
    /// post-conditions: None
    /// 
    /// </summary>
    /// 
    /// <param name="textToSay">The Text to have the audio contain.</param>
    /// <returns> NULL </returns>
    public void TextToAudio(string textToSay)
    {

    }
    #endregion
}
