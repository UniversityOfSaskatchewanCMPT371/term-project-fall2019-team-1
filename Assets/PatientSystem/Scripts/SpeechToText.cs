using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles an audio file as input and will return back a string.
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class SpeechToText : MonoBehaviour
{
    /// <summary>
    /// The Language Engine of the system.
    /// </summary>
    public LanguageEngine LE;

    /// <summary>
    /// Receives an audio file path to then send the interpreted string to the LanguageEngine.
    /// </summary>
    /// <preconditions>
    /// The file must exist and be a valid audio file.
    /// </preconditions>
    /// <param name="fileName">The audio file to interpret.</param>
    public void ReceiveAudioFile(string fileName)
    {

    }

    /// <summary>
    /// Returns a string from a given audio file.
    /// </summary>
    /// <preconditions>
    /// The file must exist and be a valid audio file.
    /// </preconditions>
    /// <param name="fileName">The audio file to interpret.</param>
    /// <returns>The Speech To Text</returns>
    public string AudioToString(string fileName)
    {
        return null;
    }
}
