using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

namespace Tests
{
    public class SpeechToTextSystemTests
    {
        [UnityTest]
        public IEnumerator OnDictationResultCallsInTreeWithText()
        {
            // Create gameobject to store UI text component and add the text.
            var logTextGameObject = MonoBehaviour.Instantiate(new GameObject());
            logTextGameObject.AddComponent<Text>();
            var uiText = logTextGameObject.GetComponent<Text>();

            // Add uiText to Log System and instantiate
            var prefab = Resources.Load<GameObject>("ID2_Prefabs/LogSystem");
            var logSystem = prefab.GetComponent<LogSystem>();
            logSystem.UIText = uiText;
            MonoBehaviour.Instantiate(prefab);

            yield return null;

            GameObject textGameObject = GameObject.Instantiate(new GameObject());
            Text text = textGameObject.AddComponent<Text>();

            GameObject gameObject = GameObject.Instantiate(new GameObject());
            var speechToTextSystem = gameObject.AddComponent<SpeechToTextSystem>();

            var dialogueTreeMock = new DialogueTreeMock();

            speechToTextSystem.dialogueTree = dialogueTreeMock;
            speechToTextSystem.text = text;
            yield return null;

            string textToMatch = "Hello World";
            speechToTextSystem.OnDictationResult(textToMatch, new ConfidenceLevel());
            Assert.AreEqual(textToMatch, dialogueTreeMock.inTreeLastSpeech);

        }
    }
}
