using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>PatientSystem</c>
/// 
/// Description: The top level game object for the Patient System.
/// 
/// </summary>
/// <authors>
/// 
/// Mason Demerais
/// </authors>
public class PatientSystem : MonoBehaviour
{
    /// <summary>
    /// The canvas to show when the tree is completed.
    /// </summary>
    public Canvas endGameCanvas;

    /// <summary>
    /// Resets the scene entirely. (everything gets destroyed and recreated)
    /// 
    /// pre-condition: None
    /// 
    /// post-condition: the entire scene is restarted.
    /// </summary>
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Called when the tree has been completed. Show the endgame screen.
    /// 
    /// pre-condition: None
    /// 
    /// post-condition: The end game canvas is set to active.
    /// </summary>
    public void FinishedTree()
    {
        StartCoroutine(WaitBeforeShowEndgameScreen());
    }

    /// <summary>
    /// Waits 5 seconds and then shows the endgame screen.
    /// </summary>
    private IEnumerator WaitBeforeShowEndgameScreen()
    {
        yield return new WaitForSeconds(5);

        endGameCanvas.gameObject.SetActive(true);
    }
}
