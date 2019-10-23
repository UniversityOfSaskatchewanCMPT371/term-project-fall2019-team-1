using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading;
using System.Security.Permissions;
using System.Runtime.InteropServices; 


public class DialogueTree : MonoBehaviour
{
    Rect Tree_Section;
    Object[] Dialogues;
    Dialogue currentNode;
    public int tree;

    // Voice, uses speechLib to produce speech from a text input.
    SpVoice voice;

    // a thread that we want to run the speech system from.
    Thread newThread; 
    bool newThreadPause = false; 



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

        // speak the first prompt that the NPC gives us!
        newThread = new Thread(runSpeech);

        // begin the thread, if it is not alive, begin this thread.
        if (!newThread.IsAlive)
        {

            newThread.Start(); 
        } 
        else
        {

            newThreadPause = !newThreadPause; 

        }

        //display the prompt
        GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
    }

    // force the NPC to stop speaking if unity stops running.
    public void OnApplicationQuit()
    {

        
        // Because we create a new thread when the NPC speaks, it wont stop once the main thread application stops running.
        if (newThread != null)
        {

            if (newThread.IsAlive)
            {
                
                // skip the squence, and finish it.
                voice.Skip("Sentence", int.MaxValue);
            }

        }


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

                    newThread = new Thread(runSpeech);
                    
                 

                    if (!newThread.IsAlive)
                    {
                        newThread.Start();
                    }
                    else
                    {
                        newThreadPause = !newThreadPause; 
                    }

                    //and dislpay prompt in log
                    GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);

                }
                return true;
            }
        }
        return false;
    }



    // the method we want to run in our new thread.
   private void runSpeech()
    {

        this.voice.Speak(currentNode.prompt); 


    }
}
