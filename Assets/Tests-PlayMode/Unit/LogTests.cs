using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            string text = getLogText();
            Assert.AreEqual(string.Empty, text);
            yield return null;
        }

        /// <summary>
        /// Checks that the uiText is empty if the log is empty.
        /// </summary>
        [UnityTest]
        public IEnumerator PrintToTextTextUIEmptyOnEmptyLog()
        {
            // Should clear log file.
            var logSystem = setUpLogSystem();
            yield return null;

            logSystem.PrintToTextField();
            yield return null;

            var uiText = logSystem.UIText.text;
            Assert.AreEqual("", uiText);
            yield return null;
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
            yield return null;
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
            yield return null;
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

        /// <summary>
        /// Checks that the given text is written to the log file after the time.
        /// </summary>
        [UnityTest]
        public IEnumerator WriteToFileDisplaysGivenText()
        {
            var logSystem = setUpLogSystem();
            yield return null;

            string textToMatch = "Hello World";

            logSystem.WriteToFile(textToMatch);
            yield return null;

            string text = getLogText();
            string[] messages = splitLog(text);

            Assert.AreEqual(2, messages.Length);

            checkMessage(messages[0], textToMatch);
            yield return null;
        }

        /// <summary>
        /// Checks that WriteToFile() writes the time to the log file before
        /// writing the given Text.
        /// </summary>
        [UnityTest]
        public IEnumerator WriteToFileDisplaysTimeOfLog()
        {
            var logSystem = setUpLogSystem();
            yield return null;

            // Log an empty string. Should still write time.
            logSystem.WriteToFile("");

            string text = getLogText();
            yield return null;

            // Check that the string contatins the time
            Assert.IsTrue(Regex.IsMatch(text, "\\d{1,2}:\\d{2}:\\d{2} [a-zA-Z]{2}: "));

            yield return null;
        }

        /// <summary>
        /// Checks that the given text is written to the log file twice.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator WriteToFileLogsMessageTwice()
        {
            var logSystem = setUpLogSystem();
            yield return null;

            string textToMatch1 = "Hello World";
            string textToMatch2 = "Message 2";

            logSystem.WriteToFile(textToMatch1);
            yield return null;
            logSystem.WriteToFile(textToMatch2);

            string text = getLogText();
            string[] messages = splitLog(text);

            // Check that there are the correct amount of lines in the log.
            Assert.AreEqual(3, messages.Length);

            // Check the second message
            checkMessage(messages[0], textToMatch1);
            // Check the first message
            checkMessage(messages[1], textToMatch2);
            yield return null;
        }

        /// <summary>
        /// Checks that the message in a single log entry matches textToMatch
        /// </summary>
        /// <param name="message">The individual log message.</param>
        /// <param name="textToMatch">The text to match the content of the log message.</param>
        private void checkMessage(string message, string textToMatch)
        {
            Assert.AreEqual(textToMatch, message.Substring(message.Length - textToMatch.Length, textToMatch.Length));
        }

        /// <summary>
        /// Reads the log file and returns its contents.
        /// </summary>
        private string getLogText()
        {
            // Check that the file is empty
            StreamReader sr = new StreamReader("logfile.txt");
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }

        /// <summary>
        /// Creates a new log system and populates it's properties so it can
        /// be tested. (These properties would normally be set in the editor)
        /// </summary>
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

        /// <summary>
        /// Splits a log into individual lines.
        /// </summary>
        /// <param name="log">The content of the log to split.</param>
        /// <returns>An array of strings containing the lines of the log file.</returns>
        private string[] splitLog(string log)
        {
            return log.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
        }
    }
}
