using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;


public class LogSystem : MonoBehaviour
{
    // Text file used for logging. Drag and drop file in editor.
    public TextAsset logFile;

    public Text UIText;

   // Key that is pressed to toggle the debug UI.
   public KeyCode debugToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Clear the log file when scene starts.
        File.WriteAllText(AssetDatabase.GetAssetPath(logFile), string.Empty);

        UIText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
        {
            ToggleDebug();
        }
    }

   public void ToggleDebug()
   {
      UIText.gameObject.SetActive(!UIText.gameObject.activeSelf);
      PrintToTextField();
   }

    public void WriteToFile(string text)
    {
        // Build stream writer for the log file.
        StreamWriter sw = new StreamWriter(AssetDatabase.GetAssetPath(logFile), append: true);
        // Prepend time to text.
        string finalAnswer = DateTime.Now.ToString("h:mm:ss tt") + ": " + text;

        sw.WriteLine(finalAnswer);
        sw.Close();
    }

    public void PrintToTextField()
    {
        StreamReader sr = new StreamReader(AssetDatabase.GetAssetPath(logFile));
        UIText.GetComponent<Text>().text = sr.ReadToEnd();
        sr.Close();
    }
}
