using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
[CreateAssetMenu(fileName = "DialogueNode", menuName = "ScriptableObjects/DialogueNode", order = 2)]
public class DialogueNode : ScriptableObject
{
    // The thing that the NPC will say.
    public string prompt;

    // the list of options this node can take.
    public List<DialogueOption> options;

    public List<List<string>> GetStringOptions()
    {
        Debug.Assert(options != null);

        // init the result
        var result = new List<List<string>>();

        // iterate through the options
        options.ForEach((option) =>
        {
            Debug.Assert(option.options != null);

            // init this branch
            var thisBranch = new List<string>();

            // interate through the avail string for this branch
            option.options.ForEach((strOpt) =>
            {
                // add this option to the list of this branch
                thisBranch.Add(strOpt);
            });

            // add this branch to the result
            result.Add(thisBranch);
        });

        return result;
    }
}

[Serializable]
public class DialogueOption
{
    // the list of string options for this branch
    public List<string> options;

    // the next node this branch points to
    public DialogueNode node;
}
