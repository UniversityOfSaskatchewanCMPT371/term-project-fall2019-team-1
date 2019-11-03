using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;

public class SmokeTest
{
    /// <summary>
    /// 
    /// <c>start</c>
    /// 
    /// Description: At the start of the game, this method is called. initialize the smoke tests data.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: builds a new smoke test.
    /// 
    /// 
    /// </summary>
    /// <returns>NULL</returns>
   /// <authors>
    /// Clayton VanderStelt
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
    /// pre-condition: The scene that we want to run on the test on must exist.
    /// 
    /// post-condition: Smoke test was able to successfully test a certian scene!
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void MainTest()
    {
        Debug.Log("Smoketest Start");
        EditorSceneManager.OpenScene("Assets/Scenes/ID2/ID2_MasterScene.unity");
        Debug.Log("Smoketest Finished");
    }
}
#endif
