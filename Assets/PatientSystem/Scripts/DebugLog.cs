using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// 
/// <c>DebugLog</c>
/// 
/// Description: This grabs messages from the log and pipes it to a text UI element.
/// 
/// Pre-condition: A ui text element.
/// 
/// Post-condition: The text on that ui text element will be changed.
/// 
/// </summary>
/// <authors>
/// Mason Demerais
/// </authors>
public class DebugLog : MonoBehaviour
{
    /// <summary>
    /// The buffer of the log messages. Populated by the LogMessageReceived
    /// </summary>
    private List<string> logBuffer;

    /// <summary>
    /// Number of lines to buffer.
    /// </summary>
    public int logBufferCount = 10;

    /// <summary>
    /// The text object to show the log
    /// </summary>
    public Text bufferTextUI;

    public TextAsset logFile;


    /// <summary>
    /// Subscribes to the logger when it receives a message, this needs to be on Awake (before Start) because it needs to hook the log messages before Start.
    /// </summary>
    private void Start()
    {
        // init the buffer
        logBuffer = new List<string>();

        // Clear the log file when scene starts.
        File.WriteAllText("logfile.txt", string.Empty); 


        // clear the text now.
        Debug.Assert(bufferTextUI != null);
        bufferTextUI.text = "";

        // sub to the messages.
        Application.logMessageReceived += LogMessageReceived;
    }

    /// <summary>
    /// When the logger received a message, this is called.
    /// 
    /// Pre-condition: 
    /// 
    /// Post-condition: The text on that ui text element will be changed.
    /// </summary>
    /// <param name="condition">The log message.</param>
    /// <param name="stackTrace">Stack trace of the debug log.</param>
    /// <param name="type">The type, log, warning or error.</param>
    private void LogMessageReceived(string condition, string stackTrace, LogType type)
    {
        Debug.Assert(logBuffer != null);

        logBuffer.Add(condition);

        Debug.Assert(logBufferCount > 0);
        // cut off the last log message if we are going to exceed the buffer count
        if (logBuffer.Count > logBufferCount)
        {
            logBuffer.RemoveAt(0);
        }

        // now update the text element
        Debug.Assert(bufferTextUI != null);

        bufferTextUI.text = "";
        logBuffer.ForEach((logMsg) =>
        {
            bufferTextUI.text += logMsg + "\n";

            writeToFile(logMsg + "\n"); 
        });
    }

    private void writeToFile(string text)
    {
        if (logFile != null)
        {
            // Build stream writer for the log file.
            StreamWriter sw = new StreamWriter("logfile.txt", append: true);
            // Prepend time to text.
            string finalAnswer = DateTime.Now.ToString("h:mm:ss tt") + ": " + text;

            sw.WriteLine(finalAnswer);
            sw.Close();
        }

    }


}
