using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
     // test:    
    // These Tests are all related to the language engine
    public class LanguageEngineTests
    {

        public LanguageEngine Leng;
        public GameObject LangE;
        // This test confirms that the language engine can be added to a GameObject within Unity
        [Test]
        public void LanguageEngineAddedPasses()

        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();
            Assert.AreNotEqual(LangE.GetComponent<LanguageEngine>(), null);
        }
        [Test]
        // This tests the language engine BestDecisionMethod will return the correct index given 
        // a matching first option when using word Comparison.
        public void LanguageEngineBestDecisionPassesGivenFirstMatchwordcomp()

        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();
            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> {"Hello", "You there", "Let's go there" };

            // Sets the word comparison to true to initiate wordComparison method
            Leng.wordComparison = true;

            // Ensures KMP does not interfere
            Leng.KMPComparison = false;


            int expectedbest = Leng.BestDecision("Hello", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 0);

            // Use the Assert class to test conditions
        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a matching second option when using word Comparison.
        public void LanguageEngineBestDecisionPassesGivenSecondMatchwordcomp()

        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            Leng.wordComparison = true;
            Leng.KMPComparison = false;
            int expectedbest = Leng.BestDecision("You there", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 1);

            
        }

        [Test]
        public void LanguageEngineBestDecisionPassesGivenLastMatchwordcomp()

        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there", "Are you happy" };
            Leng.wordComparison = true;
            Leng.KMPComparison = false;
            int expectedbest = Leng.BestDecision("happy", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 3);

            
        }

        [Test]
        // This tests the language engine Best Decision should return -1 given that there 
        // is no match using word Comparison.
        public void LanguageEngineBestDecisionPassesGivenNoMatchwordcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You then", "Let's go then" };

           
            Leng.wordComparison = true;
            Leng.KMPComparison = false;


            Assert.That(() => Leng.BestDecision("Friday", treemock), Throws.Exception);
        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a matching second option when using the KMP Comparison.
        public void LanguageEngineBestDecisionPassesGivenFirstMatchKMPcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            // Allows KMP algorithm for comparison
            Leng.KMPComparison = true;

            // Ensures only KMP algorithm is useds
            Leng.wordComparison = false;

            int expectedbest = Leng.BestDecision("Hello", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 0);

          
        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a matching second option when using the KMP Comparison.
        public void LanguageEngineBestDecisionPassesGivenSecondMatchKMPcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            Leng.KMPComparison = true;
            int expectedbest = Leng.BestDecision("You there", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 1);

        }



        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a matching last option when using the KMP Comparison.
        public void LanguageEngineBestDecisionPassesGivenLastMatchKMPcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            Leng.KMPComparison = true;
            Leng.wordComparison = false;
            int expectedbest = Leng.BestDecision("Let's go there", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 2);

        }


        [Test]
        // This tests the language engine Best Decision should return -1 given that there 
        // is no match using KMP Comparison.
        public void LanguageEngineBestDecisionPassesGivenNoMatchKMPcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You then", "Let's go then" };


            Leng.wordComparison = false;
            Leng.KMPComparison = true;

            
            // Debug.Log(expectedbest);
            //Assert.AreEqual(expectedbest, -1);

            Assert.Throws<NoBestDecision>(() =>
            {
                int expectedbest = Leng.BestDecision("Friday", treemock);
            });
        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a partial matching last option when using the word Comparison.
        public void LanguageEngineBestDecisionPassesGivenPartialMatchwordcomp()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            Leng.KMPComparison = false;
            Leng.wordComparison = true;
            int expectedbest = Leng.BestDecision("Let's go", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 2);

        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a partiallly matching option when using the KMP Comparison.
        public void LanguageEngineBestDecisionPassesGivenPartialMatchKMPcomp()
        {
            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello", "You there", "Let's go there" };
            Leng.KMPComparison = true;
            Leng.wordComparison = false;
            int expectedbest = Leng.BestDecision("Let's go", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 2);

        }

        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // an edge case when using the KMP Comparison. 
        public void LanguageEngineBestDecisionPassesGivenPartialEdgeCase1MatchKMPcomp()
        {
            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello I am not feeling so well", "Hello I am feeling well today",
                "Hello I am not felling well today" };
            Leng.KMPComparison = true;
            Leng.wordComparison = false;
            int expectedbest = Leng.BestDecision("Hello I am not well", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(0, expectedbest);

        }


        [Test]
        // This tests the language engine BestDecision method will return the correct index given 
        // a partiallly matching edge case option when using the KMP Comparison. 
        // 
        public void LanguageEngineBestDecisionPassesGivenPartialEdgeCase2MatchKMPcomp()

        {
            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();
            List<string> treemock = new List<string> { "Hello Doctor I might have an issue here , my hand is broken", "my hand is in pain",
                "I think my hand is not broken" };
            Leng.KMPComparison = true;
            Leng.wordComparison = false;
            int expectedbest = Leng.BestDecision("I might have broken my hand", treemock);
            // Debug.Log(expectedbest);
            Assert.AreEqual(expectedbest, 0);

        }


        [Test]
        // This test simple ensure the correctness of the LPS method used in the preprocessing stage 
        // in the KPM algorithm
        public void LanguageEngineLPS()
        {

            LangE = new GameObject();
            LangE.AddComponent<LanguageEngine>();

            Leng = LangE.GetComponent<LanguageEngine>();

            string sample1 = "abababca";
            string sample2 = "Cynthia";
            int[] expectedlps1 = new int[] { 0, 0, 1, 2, 3, 4, 0, 1 };
            int[] expectedlps2 = new int[] { 0, 0, 0, 0, 0, 0, 0 };


            Assert.AreEqual(Leng.LPS(sample1, sample1.Length), expectedlps1);
            Assert.AreEqual(Leng.LPS(sample2, sample2.Length), expectedlps2);

        }
    }
}
