using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class DialogueTree : MonoBehaviour
{
    Rect Tree_Section;
    Object[] Dialogues;
    Dialogue currentNode;
    public int tree;


    SpVoice voice;

    public void init_Layout()
    {
        Tree_Section.x = 0;
        Tree_Section.y = Screen.height;
        Tree_Section.width = Screen.width / 8f;
        Tree_Section.height = Screen.width - 50;

    }
 

    public void Awake()
    {
        voice = new SpVoice();
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + tree);
        currentNode = (Dialogue)Dialogues[0];
    }


    public bool inTree(string speech)
    {
        Debug.Log(speech);
        //for each response in the current node
        for(int i = 0; i < currentNode.response.Count; i++)
        {
            //if the response is the same as the string
            if(currentNode.response[i] == speech)
            {
                Debug.Log("match");
                //TODO: send to text-to-speech
                GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.next[i].prompt);
                voice = new SpVoice();
                voice.Speak(currentNode.next[i].prompt);

                //swap to next node
                currentNode = currentNode.next[i];
                
                //check if current node doesn't exists
                if(currentNode == null)
                {
                    //TODO: do something
                }
                return true;
            }
        }
        return false;
    }
}
