using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;

#if UNITY_EDITOR

public class DialogueTreeEditor : EditorWindow
{
    public void Awake()
    {
        Debug.Log("open");
    }

    [MenuItem("Window/DialogueTreeEditor")]
    static void ShowEditor()
    {
        var editor = EditorWindow.GetWindow<DialogueTreeEditor>();
        Debug.Assert(editor != null, "there is no editor");
        editor.Show();
    }

    // Called several times per frame, used to redraw the GUI
    public void OnGUI()
    {
        Debug.Log("gui");
    }
}
#endif