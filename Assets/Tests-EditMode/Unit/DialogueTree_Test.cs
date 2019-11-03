using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SpeechLib;

namespace Tests
{
    public class Dialogue_Tree_Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Dialogue_Tree_TestSimplePasses()
        {
            SpVoice testVoice = new SpVoice();
            Object[] tree1;
            tree1 = Resources.LoadAll("DialogueTree/Tree1");
            var obj = new GameObject();

            obj.AddComponent<DialogueTree>();
            var test = obj.GetComponent<DialogueTree>();

            test.Dialogues = tree1;
            test.currentNode = (Dialogue)tree1[0];
            test.voice = testVoice;

            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Dialogue_Tree_TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
