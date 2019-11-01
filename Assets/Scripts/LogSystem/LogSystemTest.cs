using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/**
 *Author: James Scarrow
 * 
 * builds and tests for  the Log system.
 */ 
public class LogSystemTest : MonoBehaviour
{
   // text file, used for testing the the log system.
   public TextAsset logFile;

   // Start is called before the first frame update
   void Start()
    {
      StartCoroutine(TestLog());
   }

    /**
     * testLog():
     * pre: none
     * post: corroutine Ienumerator type, waits for when called. once called will wait 3 seconds and run
     * check the log function.
     * 
     * return: force system running this script to wait three seconds.
     */ 
   private IEnumerator TestLog()
   {
      yield return new WaitForSeconds(3);
      CheckLog();
   }

    /**
     * CheckLog():
     * pre: none
     * post: runs the log system through a test scenario. appending a string into the text file and 
     * testings its contents.
     * 
     * return: nothing void.
     */
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
