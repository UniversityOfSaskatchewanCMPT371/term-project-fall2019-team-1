using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class ImportExport : MonoBehaviour
{
    private Object[] Dialogues;

    void Awake()
    {
        string json = string.Empty;

        //get all of the dialogues
        Dialogues = Resources.LoadAll("DialogueTree/Tree1");



        //if there is atleast 1 dialogue
        if (Dialogues.Length != 0)
        {
            //add it to the json
            json = JsonUtility.ToJson(package((Dialogue)Dialogues[0]));
        }

        //for every dialogue after the first
        for (int i = 1; i < Dialogues.Length; i++)
        {
            //make a new line, then add it
            json += "\n" + JsonUtility.ToJson(package((Dialogue)Dialogues[i]));
        }

        //put the json in a file
        File.WriteAllText(Application.dataPath + "/Resources/DialogueTree/Tree2/Tree.txt", json);


        // -------BEGIN EXPORT---------

        //grab the file
        StreamReader inportFile = new StreamReader(Application.dataPath + "/Resources/DialogueTree/Tree2/Tree.txt");

        //read the file
        List<Dialogue> dialogues = new List<Dialogue>();
        List<tempObject> tempobj = new List<tempObject>();
        while (!inportFile.EndOfStream)
        {
            tempobj.Add(JsonUtility.FromJson<tempObject>(inportFile.ReadLine()));
        }

        //for every tempObj
        for (int i = 0; i < tempobj.Count; i++)
        {


            //convert it into a Dialogue
            dialogues.Add(new Dialogue());
            dialogues[i].prompt = tempobj[i].prompt;
            dialogues[i].response = tempobj[i].response;

        }

        //now that all of the dialogues are made, put them into the proper folder.
        for (int i = 0; i < dialogues.Count; i++)
        {
            AssetDatabase.CreateAsset(dialogues[i], "Assets/Resources/DialogueTree/Tree3/Dialogue" + (i + 1) + ".asset");

        }

        //change each Dialogues.next so that it matches the tempobj.next index   
        Dialogues = Resources.LoadAll("DialogueTree/Tree3");

        //for each dialogue
        for (int i = 0; i < Dialogues.Length; i++)
        {
            ((Dialogue)Dialogues[i]).next = new List<Dialogue>();
            //for each next[] in the dialogue
            for (int j = 0; j < tempobj[i].next.Count; j++)
            {
                ((Dialogue)Dialogues[i]).next.Add((Dialogue)(Dialogues[tempobj[i].next[j]]));
            }
        }


    }

    class tempObject
    {
        public string prompt;
        public List<string> response;
        public List<int> next;
    }

    //a helper function to get the index of a given node
    int getNodeIndex(Dialogue node)
    {
        //for every node in the list of node windows
        for (int i = 0; i < Dialogues.Length; i++)
        {
            if (node == (Dialogue)Dialogues[i])
            {
                return i;
            }
        }

        return -1;
    }

    //a helper function that packages a Dialogue into a tempObject, so transfer is easier
    tempObject package(Dialogue dialogue)
    {
        //copy the prompt and responses
        tempObject temp = new tempObject
        {
            prompt = dialogue.prompt,
            response = dialogue.response,
            next = new List<int>()
        };


        //for every response in the dialogue
        for (int i = 0; i < dialogue.next.Count; i++)
        {
            //find the index of dialogue.next, and set the temp.next to that index
            if (dialogue.next[i] != null)
                temp.next.Add(getNodeIndex(dialogue.next[i]));
        }

        return temp;
    }
}