using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text bufferTextUI;

    /// <summary>
    /// Subscribes to the logger when it receives a message
    /// </summary>
    private void Start()
    {
        // init the buffer
        logBuffer = new List<string>();

        // sub to the messages.
        Application.logMessageReceived += LogMessageReceived;

        Debug.Assert(bufferTextUI != null);
        bufferTextUI.text = "";
    }
    
    /// <summary>
    /// When the logger received a message, this is called.
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
        });
    }
}
