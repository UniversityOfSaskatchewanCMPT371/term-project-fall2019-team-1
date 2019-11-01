using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object will play the audio file in the world.
/// </summary>
public class AudioOutput : MonoBehaviour
{
    /// <summary>
    /// The NPC to play the sound file.
    /// </summary>
    public NPC npc;

    /// <summary>
    /// Plays the sound.
    /// </summary>
    /// <preconditions>
    /// The file must exist and be a proper sound file.
    /// </preconditions>
    /// <param name="fileName">The sound file to play.</param>
    public void PlaySound(string fileName)
    {

    }
}
