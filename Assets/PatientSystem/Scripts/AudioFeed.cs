using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Description:This object records from the microphone for a time and stores to a file.
/// 
/// pre-condition: audio input device.
/// 
/// post-condition: creates a .wav file. 
/// 
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// </authors>
public class AudioFeed : MonoBehaviour
{

    // The file name to be created and recorded to.

    public string fileName { get; private set; } = "";


    // The STT system.
 
    public SpeechToText STT;

    /// <summary>
    /// <c>StartRecording</c>
    /// 
    /// Description: Starts recording to the filename.
    /// 
    /// preconditions: fileName must be "", ie: there must not be another recording taking place.
    /// The application must have access to a audio input device.
    /// 
    /// post-conditions: this.fileName will equal fileName and a new file will be created and opened
    /// as the audio will be recorded to it.
    /// 
    /// </summary>
    /// 
    /// <param name="fileName">The file to be recording to.</param>
    /// <returns> NULL </returns>
    public void StartRecording(string fileName)
    {
        if (this.fileName != "")
        {
            throw new InvalidOperationException("Cannot change fileName after it has been set.");
        }

        Debug.Assert(this.fileName == "");

        Debug.Log(string.Format("AudioFeed::StartRecording: fileName: {0}", fileName));

        this.fileName = fileName;
    }

    /// <summary>
    /// <c>StopRecording</c>
    /// 
    /// Description: Stops recording and closes the file.
    /// 
    /// pre-conditions: this.fileName must not be "", ie: There must be a recording happening.
    /// 
    /// post-conditions: this.fileName is now "" and the file is closed.
    /// </summary>
    /// 
    /// <returns> NULL </returns>
    public void StopRecording()
    {
        Debug.Assert(fileName != "");

        Debug.Log(string.Format("AudioFeed::StopRecording: fileName: {0}", fileName));

        STT.ReceiveAudioFile(fileName);

        fileName = "";
    }
}
