using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This logs various operations that happen within our system to console and an optional file.
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class Log : MonoBehaviour
{
    /// <summary>
    /// This writes the string to the log.
    /// </summary>
    /// <param name="w">The string to write.</param>
    public void WriteToLog(string w)
    {
        Debug.Log(w);
    }
}
