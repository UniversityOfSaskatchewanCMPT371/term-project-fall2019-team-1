using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class DebugLogTests
    {

        /// <summary>
        /// Checks that the bufferTextUI is empty after awake is called.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator StartBufferTextUIEmpty()
        {
            var debugLog = setUpDebugLog();
            yield return null;

            Assert.AreEqual(string.Empty, debugLog.bufferTextUI.text);
        }

        /// <summary>
        /// Checks that the bufferTextUI text is the log message
        /// that was provided to debug.log.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator LogMessageReceivedTextIsMessage()
        {
            var debugLog = setUpDebugLog();
            yield return null;

            string stringToMatch = "Hello World";
            Debug.Log(stringToMatch);
            yield return null;

            Assert.AreEqual(stringToMatch + "\n", debugLog.bufferTextUI.text);
        }

        /// <summary>
        /// Instantiates a gameobject with a DebugLog
        /// and returns the debug log.
        /// </summary>
        /// <returns>The DebugLog on the instantiated gameobject.</returns>
        private DebugLog setUpDebugLog()
        {
            // Create gameobject to store UI text component and add the text.
            var textGameObject = MonoBehaviour.Instantiate(new GameObject());
            textGameObject.AddComponent<Text>();
            var uiText = textGameObject.GetComponent<Text>();


            GameObject gameObject = new GameObject();
            var debugLog = gameObject.AddComponent<DebugLog>();
            debugLog.bufferTextUI = uiText;
            gameObject = GameObject.Instantiate(gameObject);
            return gameObject.GetComponent<DebugLog>();
        }
    }
}
