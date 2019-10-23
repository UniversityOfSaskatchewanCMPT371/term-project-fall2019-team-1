using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests
{
    public class LogTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TurnsOffUITextOnStart()
        {
            var logSystem = setUpLogSystem();
            yield return null;
            Assert.AreEqual(true, logSystem.UIText.gameObject.activeSelf);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ToggleDebugTurnsOnUIText()
        {
            var logSystem = setUpLogSystem();
            yield return null;
            logSystem.ToggleDebug();
            yield return null;
            Assert.AreEqual(false, logSystem.UIText.gameObject.activeSelf);
        }

        private LogSystem setUpLogSystem()
        {
            // Create gameobject to store UI text component and add the text.
            var textGameObject = MonoBehaviour.Instantiate(new GameObject());
            textGameObject.AddComponent<Text>();
            var uiText = textGameObject.GetComponent<Text>();

            // Add uiText to Log System and instantiate
            var prefab = Resources.Load<GameObject>("ID2_Prefabs/LogSystem");
            var logSystem = prefab.GetComponent<LogSystem>();
            logSystem.UIText = uiText;
            GameObject logSystemObject = MonoBehaviour.Instantiate(prefab);
            return logSystemObject.GetComponent<LogSystem>();
            
        }
    }
}
