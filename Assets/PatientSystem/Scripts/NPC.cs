using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description: The NPC to play animations and sounds on.
/// 
/// Pre-Conditions: Dialouge Tree System needs to be up and running.
/// 
/// Post-Conditions: NPC Enitiy in the world
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// </authors>
public class NPC : MonoBehaviour
{
    /// <summary>
    /// The audio source for which the sound will come out of.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The animation state handler to play animations on.
    /// </summary>
    public Animator animator;
}
