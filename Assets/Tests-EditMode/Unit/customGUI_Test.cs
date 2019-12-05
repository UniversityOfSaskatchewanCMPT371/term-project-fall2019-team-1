using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class customGUI_Test
    {
        // A Test behaves as an ordinary method.
        [Test]
        public void cumstomGUI_TestSimplePasses()
        {
            // Creating variables for testing.
            Object[] nodes = Resources.LoadAll("DialogueTree/Tree1");
            DialogueBuilder GUI = ScriptableObject.CreateInstance<DialogueBuilder>();

            // Testing that relies on awake().
            Debug.Assert(GUI.layers == 0, "failure to create found");
            Debug.Assert(GUI.trees != null, "failure to create found");
            Debug.Assert(GUI.treeDialogues != null, "failure to create treesDialogues");
            Debug.Assert(GUI.treesToDelete != null, "failure to create treesToDelete");
            Debug.Assert(GUI.atLayer != null, "failure to create atlayer");
            Debug.Assert(GUI.Dialogues != null, "failure loading dialgoues");
            Debug.Assert(GUI.NodeLayer != null, "failure to create nodeLayer");
            Debug.Assert(GUI.dialwindows != null, "failure to create dialwindows");
            Debug.Assert(GUI.responsewindows != null, "failure to create dialwindows");
        }
    }
}
