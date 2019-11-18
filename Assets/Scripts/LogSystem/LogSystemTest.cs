using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// <c>LogSystemTest</c>
/// 
/// Description: Tests for log system
/// 
/// pre-condition: need log system within scene in order to run this test.
/// 
/// post-condition: returns to system that test was done correctly or not. 
/// 
/// </summary>
/// 
/// <authors>Matt Radke, James Scarrow </authors>
public class LogSystemTest : MonoBehaviour
{
   // text file, used for testing the the log system.
   public TextAsset logFile;

   // Start is called before the first frame update
   void Start()
    {
      StartCoroutine(TestLog());
   }

    /// <summary>
    /// <c>TestLog</c>
    /// 
    /// Desciption: coroutine waits to be called by test log. 
    /// 
    /// Pre-condition: None
    /// 
    /// post-condition: returns a time in seconds that waits before executing in code again.
    /// 
    /// </summary>
    /// <returns>force system running this script to wait three seconds.</returns>
    private IEnumerator TestLog()
   {
      yield return new WaitForSeconds(3);
      CheckLog();
   }

    /// <summary>
    /// Description: runs log test on system 
    /// 
    /// pre-condition: none
    /// 
    /// post-condition: test ran on system. 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
   private void CheckLog()
   {
      // GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile("Hello World");
      // StreamReader sr = new StreamReader(AssetDatabase.GetAssetPath(logFile));
      // string line = sr.ReadLine();
      // if (!line.Contains("Hello World"))
      // {
      //    throw new System.Exception("Line not correctly written to file");
      // }
      // Debug.Log("Tests Passed");
   }  // 
}
