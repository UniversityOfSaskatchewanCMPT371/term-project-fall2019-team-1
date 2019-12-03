using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AudioFeedTest
    {
        /// <summary>
        /// Checks that after StartRecording is called the fileName
        /// property of the audio feed is the string provided.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator StartRecordingSetsFileName()
        {
            var audioFeed = setUpAudioFeed();
            yield return null;

            var stringToMatch = "Hello World";
            audioFeed.StartRecording(stringToMatch);
            yield return null;

            Assert.AreEqual(stringToMatch, audioFeed.fileName);
        }

        [UnityTest]
        public IEnumerator StartRecordingCalledTwiceThrowsException()
        {
            var audioFeed = setUpAudioFeed();
            yield return null;

            audioFeed.StartRecording("Hello World");
            yield return null;

            Assert.Throws<InvalidOperationException>(() => {
                audioFeed.StartRecording("Hello Again");
            });
            yield return null;
        }

        [UnityTest]
        public IEnumerator StopRecordingErasesFileName()
        {
            var audioFeed = setUpAudioFeed();
            yield return null;

            var fileName = "Hello World";
            audioFeed.StartRecording(fileName);
            yield return null;

            audioFeed.StopRecording();
            yield return null;

            Assert.AreEqual("", audioFeed.fileName);
            yield return null;
        }

        public IEnumerator StopRecordCalledBeforeStartThrowsException()
        {
            var audioFeed = setUpAudioFeed();
            yield return null;

            Assert.Throws<InvalidOperationException>(() =>
            {
                audioFeed.StopRecording();
            });
            yield return null;
        }

        /// <summary>
        /// Creates a new AudioFeed in the scene so it can be tested.
        /// </summary>
        private AudioFeed setUpAudioFeed()
        {

            // Create a gameobject and give it a audio feed component
            var audioFeedGameObject = MonoBehaviour.Instantiate(new GameObject());
            audioFeedGameObject.AddComponent<AudioFeed>();
            return audioFeedGameObject.GetComponent<AudioFeed>();
        }
    }
}
