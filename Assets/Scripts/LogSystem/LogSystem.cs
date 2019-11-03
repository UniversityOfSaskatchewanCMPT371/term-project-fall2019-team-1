using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;

/**
 * Authors: Matt Radke, James Scarrow
 * 
 * Log System:
 * 
 * uses a text file to log conversation that is said within a a certain player and NPC
 * interaction.
 * 
 */ 
public class LogSystem : MonoBehaviour
{
    // Text file used for logging. Drag and drop file in editor.
    public TextAsset logFile;

    // the Text box that data from text file is displayed in.
    public Text UIText;

   // Key that is pressed to toggle the debug UI.
   public KeyCode debugToggle;

    
    /**
     * Start():
     * preCond: None
     * post:text file is reset, and UITEXT is set to false.
     * return: None void.
     * 
     */  
    void Start()
    {
        // Clear the log file when scene starts.
        File.WriteAllText("logfile.txt", string.Empty);

        UIText.gameObject.SetActive(false);
    }

    /**
     * update():
     * pre:None
     * post:monitors when the spacebar button is pressed! when pressed activates text field.
     * return: Nothing void.
     * 
     */ 
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
        {
            if (UIText.gameObject.activeSelf)
            {

                UIText.gameObject.SetActive(false);
            }
            else
            {
                UIText.gameObject.SetActive(true);
                PrintToTextField();
            }
        }*/
    }


     /*
      * writeToFile(text):
      * pre:    text: string, the string text that is wanted to be stored in a text file.
      * post:  builds a stream writer and appends the text string value into the text file. Also
      *        finds the current time and gives the string a time stamp before adding to text file.
      *       
      * return: Nothing void.
      * 
      */ 
    public void WriteToFile(string text)
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

    /**
     * PrintToTextField():
     * pre: none
     * post: transfers the content of logfile.txt into the UI text field UITEXT.
     * 
     * return Nothing void.
     * 
     */ 
    public void PrintToTextField()
    {
        if (logFile != null)
        {
            StreamReader sr = new StreamReader("logfile.txt");
            UIText.GetComponent<Text>().text = sr.ReadToEnd();
            sr.Close();
        }

    }
}
