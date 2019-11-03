using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

/// <summary>
/// 
/// Author: Clayton VanderStelt
/// 
/// <c>DialougeGui</c>
/// 
/// Description: Enables ability to find the custom GUI tree diagram within unity. It is built within the
/// unity editor its self.
/// 
/// Pre-condtion: System needs access to Custom GUI and Node editor scripts when running.
/// 
/// Post-condition: Backbone unity editor view. Allows us to see the dialouge tree in unity.
///
/// </summary>
/// 
 /// <authors>
/// Clayton VanderStelt
/// </authors>
[CustomEditor(typeof(Dialogue))]

public class DialogueGUI : EditorWindow
{

    public Dialogue dialogue;
    private int id;

    /// <summary>
    /// 
    /// <c>OnEnable</c>
    /// 
    /// Description: enables  dialouge option
    /// 
    /// Pre-condtion: Dialouge option must not be NULL;
    /// 
    /// Post-Condtion: enables dialouge option.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
     void OnEnable(){
         dialogue = ScriptableObject.CreateInstance<Dialogue>();
     }


    /// <summary>
    /// 
    /// <c>OpenWindow</c>
    /// 
    /// Description: opens up the editor window for the gui tree graphical panel.
    /// 
    /// Pre-condtion: unity must be open.
    /// 
    /// Post-Condtion: returns new panel with GUI tree inside of it.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    [MenuItem("Window/Dialogue")]
     static void OpenWindow(){
         
        DialogueGUI window = (DialogueGUI)EditorWindow.GetWindow(typeof(DialogueGUI));
        window.minSize = new Vector2(200, 400);
        window.maxSize = new Vector2(400, 400);
        window.Show();

    }


    /// <summary>
    /// <c>OnGUI</c>
    /// 
    /// Description: Enables the use of GUI, a reserved unity method call. OnGUI controls
    /// graphical objects within this script.
    /// 
    /// pre-condtion: data must not be null, as nothing can run.
    /// 
    /// post-condtion: ability to control and use GUI for CustomGUI panel.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
      void OnGUI()
    {
        if(dialogue == null){
             dialogue = new Dialogue();
        }

        dialogue.prompt = EditorGUILayout.TextField("Enter prompt", dialogue.prompt);



        SerializedObject dialogue_s = new SerializedObject(dialogue);
        SerializedProperty response_prop = dialogue_s.FindProperty("response");
        SerializedProperty nextid_prop = dialogue_s.FindProperty("next");

         EditorGUILayout.PropertyField(response_prop, true);
         EditorGUILayout.PropertyField(nextid_prop, true); // True means show children
         dialogue_s.ApplyModifiedProperties(); 

         if(GUI.changed){
             EditorUtility.SetDirty(dialogue);
         }


        if(GUILayout.Button("button"))
        {
            AssetDatabase.CreateAsset(dialogue, "Assets/Resources/Dialogues/Dialogue"+id+".asset");
            id++;
        }

        // if(GUILayout.Button(("Reset Dial")))
    }



}

#endif