using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueGUI : EditorWindow
{

    public Dialogue dialogue;
     void OnEnable(){
         dialogue = ScriptableObject.CreateInstance<Dialogue>();
     }


    [MenuItem("Window/Dialogue")]
     static void OpenWindow(){
         
        DialogueGUI window = (DialogueGUI)EditorWindow.GetWindow(typeof(DialogueGUI));
        window.minSize = new Vector2(200, 400);
        window.maxSize = new Vector2(400, 400);
        window.Show();

    }
      void OnGUI()
    {
        if(dialogue == null){
             dialogue = new Dialogue();
        }

        dialogue.id = EditorGUILayout.IntField("Enter dialogue id", dialogue.id);
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
            AssetDatabase.CreateAsset(dialogue, "Assets/Resources/Dialogues/Dialogue"+dialogue.id+".asset");    
        }

        // if(GUILayout.Button(("Reset Dial")))
    }



}
