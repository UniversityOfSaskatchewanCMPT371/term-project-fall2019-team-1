using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
}
