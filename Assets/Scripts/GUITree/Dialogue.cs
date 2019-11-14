using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    // The tree that this node belongs to.
    public int tree;

    // The thing that the NPC will say.
    public string prompt;

    // The expected response from the player.
    public List<string> response;

    // A reference to the next node.
    public List<Dialogue> next;

    //whether or not this node is the root of a tree.
    public bool start;

    public AnimationClip anim;


}
