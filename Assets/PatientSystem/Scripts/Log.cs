using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// <c>Log</c>
/// Description: This logs various operations that happen within our system to console and an optional file.
/// 
/// Pre-condition: Need speech-to-text system and NPC communicating to record log information. 
/// 
/// Post-condition: posts log of conversation into a text file for future reference beyond scope of conversation.
/// 
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class Log : MonoBehaviour
{
    /// <summary>
    /// 
    /// <c>WriteToLog</c>
    /// 
    /// Description: This writes the string to the log.
    /// 
    /// Pre-condition: None
    /// Post-condition: None
    /// 
    /// </summary>
    /// <param name="w">The string to write.</param>
    /// <returns> NULL </returns>
    public void WriteToLog(string w)
    {
        Debug.Log(w);
    }
}
