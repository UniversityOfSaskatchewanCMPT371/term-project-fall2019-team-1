using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>PatientSystem</c>
/// 
/// Description: The top level game object for the Patient System.
/// 
/// pre-condition: None
/// 
/// post-condition: container for entire system.
/// 
/// </summary>
/// <authors>
/// 
/// Mason Demerais
/// </authors>
public class PatientSystem : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FinishedTree()
    {

    }
}
