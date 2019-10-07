using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class DialogueTreeWindow : EditorWindow
{
    public static ScriptableObject dialogue;
    private Object[] dialogues;



    void onEnable()
    {
        dialogues = Resources.LoadAll("Dialogue");
    }
    
    [MenuItem("Window/DialogueTree")]
    static void OpenWindow(){

        DialogueTreeWindow window = (DialogueTreeWindow)EditorWindow.GetWindow(typeof(DialogueTreeWindow));
        window.Show();
    }

    static void DrawDialogueTree(){

    }

    void onGUI(){
        foreach(Dialogue element in dialogues){
            Debug.Log(element.prompt);
        }
    }
}
