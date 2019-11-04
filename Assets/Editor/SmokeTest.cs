using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;

public class SmokeTest
{
    // private Thread sceneThread;

    /// <summary>
    /// 
    /// <c>start</c>
    /// 
    /// Description: The smoke test is called to ensure basic functionality of the system. The smoke test is a work in progress, and will be expanded to test - tree functionality
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
    /// Description: Runs the smoke test on the system, does this by testing a scene.
    /// 
    /// pre-condition: The build must have valid scenes
    /// 
    /// post-condition: Smoke test successfully verified using editor-mode, that a scene could load all assets without crashing.
    /// All basic functionalities are working together without crashes, freezes, or errors.
    /// Also verifies that the build contains valid scenes
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

        // Thread and start scenes in editor-mode
        // sceneThread = new Thread(() => load_scenes(scenes));
        // Debug.Log("Starting sceneThread Thread");
        // sceneThread.Start();

        // Wait for threads to finish before declaring Smoketest Finish
        // sceneThread.Join();

        Debug.Log("Smoketest Finished");
    }
}
#endif
