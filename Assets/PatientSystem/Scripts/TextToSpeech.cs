using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
/// Mason Demerais
/// </authors>
public class TextToSpeech : MonoBehaviour
{
    // The audio output object of the system.
    
    public AudioOutput audioOutput;


    /// <summary>
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
}
