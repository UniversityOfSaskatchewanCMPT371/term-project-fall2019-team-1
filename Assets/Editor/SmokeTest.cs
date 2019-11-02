using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;

public class SmokeTest
{
    // Start is called before the first frame update
    public static void Start()
    {
        SmokeTest smoke = new SmokeTest();
        smoke.MainTest();
    }

    public void MainTest()
    {
        Debug.Log("Smoketest Start");
        EditorSceneManager.OpenScene("Assets/Scenes/ID2/ID2_MasterScene.unity");
        Debug.Log("Smoketest Finished");
    }
}
#endif
