using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This object records from the microphone for a time and stores to a file.
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class AudioFeed : MonoBehaviour
{
    /// <summary>
    /// The file name to be created and recorded to.
    /// </summary>
    private string fileName;

    /// <summary>
    /// Starts recording to the filename.
    /// </summary>
    /// <preconditions>
    /// fileName must be "", ie: there must not be another recording taking place.
    /// The application must have access to a audio input device.
    /// </preconditions>
    /// <postconditions>
    /// this.fileName will equal fileName and a new file will be created and opened as the audio will be recorded to it.
    /// </postconditions>
    /// <param name="fileName">The file to be recording to.</param>
    public void StartRecording(string fileName)
    {

    }

    /// <summary>
    /// Stops recording and closes the file.
    /// </summary>
    /// <preconditions>
    /// this.fileName must not be "", ie: There must be a recording happening.
    /// </preconditions>
    /// <postconditions>
    /// this.fileName is now "" and the file is closed.
    /// </postconditions>
    public void StopRecording()
    {

    }
}
