using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;

public class SmokeTest
{
    CustomGUI UsrInterface;
    public GameObject LEngine;

    /// <summary>
    /// 
    /// <c>start</c>
    /// 
    /// Description: The smoke test is called to ensure basic functionality of the system.
    /// The Smoke Test currently does the following:
    /// -Ensures there exists valid scenes in build
    /// -For each scene, ensures that the scene can run without issues
    /// -Ensure all assets are capable of loading properly
    /// -Ensures Vital GUI components can be created and function properly
    /// -Ensures Language Engine can be created properly
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: builds a new smoke test.
    /// 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    /// <authors>
    /// Mathew Cathcart
    /// </authors>
    public static void Start()
    {
        SmokeTest smoke = new SmokeTest();
        smoke.MainTest();
    }

    /// <summary>
    /// 
    /// <c>MainTest</c>
    /// 
    /// Description: Runs the smoke test on the system as defined above
    /// -Ensures there exists valid scenes in build
    /// -For each scene, ensures that the scene can run without issues
    /// -Ensure all assets are capable of loading properly
    /// -Ensures Vital GUI components can be created and function properly
    /// -Ensures Language Engine can be created properly
    /// 
    /// pre-condition: The build must have valid scenes
    /// 
    /// post-condition: Smoke test successfully verified the system, or threw an error
    /// if the system is broken
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void MainTest()
    {
        Debug.Log("Smoketest Start");

        // Get num scenes present in system build
        int numScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        Debug.Assert(numScenes > 0, "There aren't any main scenes in the build path");
        Debug.Log("Num Scenes In Build: " + numScenes);

        //Declare variables for scene management
        string[] scenes = new string[numScenes];
        Debug.Assert(scenes != null, "scenes initialization in smoketest is null");

        // Get all build scene names
        for (int i = 0; i < numScenes; i++)
        {
            Debug.Assert(i < numScenes, "Smoketest Error: Index out of range");
            scenes[i] = (UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            Debug.Log("Scene Added: " + scenes[i]);
        }

        // Load scehes in editor-mode without threading
        Debug.Assert(scenes != null, "Passed in scenes parameter can't be null");
        Debug.Assert(scenes.Length > 0, "Passed in scenes must have at least 1 scene object");

        // Start Scenes in editor-mode. Load scene assets and ensure no crashes, exceptions, null references, or memory leaks happen
        Debug.Log("Loading Scenes in Editor Mode");
        Array.ForEach(scenes, Scene => {
            EditorSceneManager.OpenScene(Scene);
            Debug.Log("Successfully loaded assets and tested " + Scene);
        });


        // Processes Testing
        UsrInterface = ScriptableObject.CreateInstance<CustomGUI>();
        Assert.AreNotEqual(UsrInterface.trees, null);
        Assert.AreNotEqual(UsrInterface.treeDialogues, null);
        Assert.AreNotEqual(UsrInterface.treesToDelete, null);
        Assert.AreNotEqual(UsrInterface.Dialogues, null);
        Assert.AreNotEqual(UsrInterface.NodeLayer, null);

        LEngine = new GameObject();
        LEngine.AddComponent<LanguageEngine>();
        Assert.AreNotEqual(LEngine.GetComponent<LanguageEngine>(), null);

        Debug.Log("Smoketest Finished");
    }
}
#endif
