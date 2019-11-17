using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ImportExportTests
    {
        ImportExport test;
        GameObject sample;


        // This simply tests that the import export functionality can be added to a GameObject in Unity
        [Test]
        public void ImportExportAccessTest()
        {
            sample = new GameObject();
            sample.AddComponent<ImportExport>();
            test = sample.GetComponent<ImportExport>();
            Assert.AreNotEqual(test, null);

            // Debug.Log(test.Dialogues.Length);
        }

        public void ImportExportGetNodeIndexTest()
        {
            //TODO Test the getnodeIndex Function within Import Export

            AssertAreEqual(true, null);
            // Failing test due to lack of implementation


            //TODO Might have to refactor import export and the Resources.Load cannot be tested and 
            // the ImportExport.dialogues remains null
        }







    }
}
