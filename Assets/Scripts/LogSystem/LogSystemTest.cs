using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LogSystemTest : MonoBehaviour
{
   public TextAsset logFile;

   // Start is called before the first frame update
   void Start()
    {
      StartCoroutine(TestLog());
   }

   private IEnumerator TestLog()
   {
      yield return new WaitForSeconds(3);
      CheckLog();
   }

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
