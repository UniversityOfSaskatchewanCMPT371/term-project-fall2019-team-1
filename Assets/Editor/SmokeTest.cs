using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmokeTest : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Start()
    {
        SmokeTest smoke = new SmokeTest();
        smoke.MainTest();
    }

    public void MainTest()
    {
        StartCoroutine(LoadAsyncScene());
    }

    public IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ID2_MasterScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Scene Loaded Successfully");
    }
}
