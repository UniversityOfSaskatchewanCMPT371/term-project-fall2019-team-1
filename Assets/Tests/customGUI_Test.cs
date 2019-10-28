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
            CustomGUI GUI = new CustomGUI();

            // Testing that relies on awake().
            Assert.IsTrue(GUI != null, "failure to create GUI");
            Assert.IsTrue(GUI.Trees != null, "failure to load the trees");

            // Testing that relies on OnGUI().
            GUI.OnGUI();
            if ((GUI.Trees != null) && (GUI.Dialogues.Length > 0)) // If the tree exists, and there is atleast one node.
            {
                Assert.IsTrue(GUI.findCurrent() != null, "Failure to load currentNode"); // There has to be a current node.
                Assert.IsTrue(GUI.getNodeIndex((Dialogue)GUI.Dialogues[0]) == 0, "")
            }

            


        }
    }
}
