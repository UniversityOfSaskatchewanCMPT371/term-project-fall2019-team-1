using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.IO;

namespace Tests
{
    public class LogTests
    {
        /// <summary>
        /// Checks that the log file is empty after creating a new log system.
        /// </summary>
        [UnityTest]
        public IEnumerator ClearsLogFileOnStart()
        {
            // Write to the file so it isn't empty
            StreamWriter sw = new StreamWriter("logfile.txt");
            sw.Write("Hello World");
            sw.Close();

            // Create log system. Should empty file.
            var logSystem = setUpLogSystem();
            yield return null;

            // Check that the file is empty
            StreamReader sr = new StreamReader("logfile.txt");
            string text = sr.ReadToEnd();
            sr.Close();
            Assert.AreEqual(string.Empty, text);
        }

        /// <summary>
        /// Checks that calling toggle debug once turns on the UIText
        /// of the log system.
        /// </summary>
        [UnityTest]
        public IEnumerator ToggleDebugTurnsOnUIText()
        {
            var logSystem = setUpLogSystem();
            yield return null;
            logSystem.ToggleUIText();
            yield return null;
            Assert.AreEqual(true, logSystem.UIText.gameObject.activeSelf);
        }

        /// <summary>
        /// Checks that when toggle debug is called twice the UIText
        /// of the log system is inactive.
        /// </summary>
        [UnityTest]
        public IEnumerator ToggleDebugTwiceTurnsOffUIText()
        {
            var logSystem = setUpLogSystem();
            yield return null;
            logSystem.ToggleUIText();
            yield return null;
            logSystem.ToggleUIText();
            yield return null;
            Assert.AreEqual(false, logSystem.UIText.gameObject.activeSelf);
        }

        /// <summary>
        /// Checks that when a logSystem is created it's UIText is inactive.
        /// </summary>
        [UnityTest]
        public IEnumerator TurnsOffUITextOnStart()
        {
            var logSystem = setUpLogSystem();
            yield return null;
            Assert.AreEqual(false, logSystem.UIText.gameObject.activeSelf);
            yield return null;
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
