using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;

/// <summary>
/// <c>LogSystem</c>
/// Description: stores text from NPC and Player conversation into text file
/// 
/// pre-condition: need both player and NPC systems running in unity scene editor.
/// 
/// Post-condition: Once text grabbed from player store in text file. 
/// 
/// </summary>
/// <authors>Matt Radke, James Scarrow</authors>
public class LogSystem : MonoBehaviour
{
    // Text file used for logging. Drag and drop file in editor.
    public TextAsset logFile;

    // the Text box that data from text file is displayed in.
    public Text UIText;

    // Key that is pressed to toggle the debug UI.
    public KeyCode debugToggle;

    // The scenes dialogue tree to use for displaying availible responses
    public DialogueTree dialogueTree;
    
    /// <summary>
    /// <c>Start</c>
    /// Description: Starts system, by clearing file and preping it for new text.
    /// 
    /// Pre-condition: need text file and Text ui in scene.
    /// 
    /// post-condition: sets system up for future use.
    /// </summary>
    /// <returns>NULL</returns>
    void Start()
    {
        // Clear the log file when scene starts.
        File.WriteAllText("logfile.txt", string.Empty);

        UIText.gameObject.SetActive(false);

        // Get the Dialogue Tree in the scene
        dialogueTree = FindObjectOfType<DialogueTree>().GetComponent<DialogueTree>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
        {
            ToggleUIText();
        }
    }

    /// <summary>
    /// Toggles the UI text belonging to this LogSystem.
    /// Inputs: None
    /// Outputs: None
    /// Pre-Conditions: None
    /// Post-Conditions: If the UI was active before invoking the method it will
    /// be inactive after. If it was inactive, it will be active.
    /// </summary>
    public void ToggleUIText()
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
    }


    /// <summary>
    /// <c>writeToFile</c>
    /// Description: stores text into a text file
    /// 
    /// pre-condition: need a text file in order to store text.
    /// 
    /// post-condition: writes text to a text file.
    /// 
    /// </summary>
    /// <param name="text">the text you want to put into a text file.</param>
   /// <returns>NULL</returns>
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

    /// <summary>
    /// <c>PrintToTextField</c>
    /// Description: print log file to text ui within system scene. 
    /// 
    /// Pre-condition: need text ui in scene
    /// 
    /// post-condition: text ui updated with text from log file. 
    /// </summary>
    /// 
    /// <returns>NULL</returns>
    public void PrintToTextField()
    {
        if (logFile != null)
        {
            StreamReader sr = new StreamReader("logfile.txt");
            UIText.GetComponent<Text>().text = sr.ReadToEnd();
            sr.Close();
        }
    }

    /// <summary>
    /// <c>ShowAllOptions</c>
    /// Description: Displays all the current phrases the user
    /// can say from the current node in the dialogue tree.
    /// 
    /// Pre-condition: need text ui in scene
    /// 
    /// post-condition: text ui updated with possible phrases the user can say.
    /// </summary>
    ///
    /// <returns>NULL</returns>
    public void ShowAllOptions()
    {

    }
}
