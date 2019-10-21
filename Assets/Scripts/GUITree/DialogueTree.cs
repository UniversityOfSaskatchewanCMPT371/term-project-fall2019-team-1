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
        voice.Speak(currentNode.prompt);
        
        //display the prompt
        GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
    }


    public bool inTree(string speech)
    {
        //for each response in the current node
        for(int i = 0; i < currentNode.response.Count; i++)
        {
            //if the response is the same as the string
            if(currentNode.response[i].ToLower() == speech.ToLower())
            {
                Debug.Log(speech + " matches "  + currentNode.response[i]);
                voice = new SpVoice();
                
                //check if current node has a next 
                if(currentNode.next.Count == 0)
                {
                    //if it doesnt, display a message
                    Debug.Log("Finished Tree");
                }
                else
                { 
                    //swap to next node, and speak the prompt
                    currentNode = currentNode.next[i];
                    voice.Speak(currentNode.prompt);

                    //and dislpay prompt in log
                    GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
                }
                return true;
            }
        }
        return false;
    }
}
