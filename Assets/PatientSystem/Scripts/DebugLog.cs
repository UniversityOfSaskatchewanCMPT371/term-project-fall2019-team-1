using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    void Start()
    {
        Application.logMessageReceived += (string condition, string stackTrace, LogType type) =>
        {
            Debug.Log("ya woo");
        };
    }
}
