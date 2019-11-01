using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This receives a string to then be converted to an audio file.
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class TextToSpeech : MonoBehaviour
{
    /// <summary>
    /// The audio output object of the system.
    /// </summary>
    public AudioOutput audioOutput;

    /// <summary>
    /// Converts the textToSay to an audio file and then plays that file to the audioOutputter.
    /// </summary>
    /// <param name="textToSay">The words to be heard.</param>

    public void ReceiveText(string textToSay)
    {
    }

    /// <summary>
    /// Writes to a file that is a TextToSpeech of the input textToSay.
    /// </summary>
    /// <param name="textToSay">The Text to have the audio contain.</param>
    public void TextToAudio(string textToSay)
    {

    }
}
