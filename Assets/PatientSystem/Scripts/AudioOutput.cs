using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Audio Input</c>
/// Description: This object will play the audio file in the world.
/// 
/// PreCondition: Need audio from dialouge system.
/// 
/// PostCondition: plays a sound in the world
/// 
/// </summary>
/// 
/// <author>Mason Demerais</author>
public class AudioOutput : MonoBehaviour
{

    // The NPC to play the sound file.

    public NPC npc;

    /// <summary>
    /// <c>PlaySound</c>
    /// 
    /// Description: Plays the sound.
    ///
    /// preconditions: The file must exist and be a proper sound file.
    /// 
    /// PostConditions: None
    /// 
    /// </summary> 
    /// <param name="fileName">The sound file to play.</param>
    /// <returns> NULL </returns>
   
    public void PlaySound(string fileName)
    {

    }
}
