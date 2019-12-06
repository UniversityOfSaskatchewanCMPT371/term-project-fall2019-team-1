using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    // The ID of the tree that this node belongs to.
    [HideInInspector]
    public int tree;

    // The name of the tree that his node belongs to.
    [HideInInspector]
    public string treeName;

    // The thing that the NPC will say.
    [HideInInspector]
    public string prompt;

    // The expected response from the player.
    [HideInInspector]
    public List<string> response;

    // A reference to the next node.
    [HideInInspector]
    public List<Dialogue> next;

    // Whether or not this node is the root of a tree.
    [HideInInspector]
    public bool start;

    // An animation that will be played when the prompt is spoken.
    [HideInInspector]
    public AnimationClip anim;


}
