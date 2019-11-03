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
            CustomGUI GUI = ScriptableObject.CreateInstance<CustomGUI>();

            // Testing that relies on awake().
            Assert.IsTrue(GUI != null, "failure to create GUI");
            Assert.IsTrue(GUI.Trees != null, "failure to load the trees");
        }
    }
}
