using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading;
using System.Security.Permissions;
using System.Runtime.InteropServices; 

/**
 * author: Clayton VanderStelt, Kareem Oluwaseyi, Matt Radke
 * 
 * DialogueTree, is the under laying system that runs the NPC. It stores NPC responses and 
 * allows the NPC to interact with the player by converting text to speech using the SpeechLib.
 * 
 */
public class DialogueTree : MonoBehaviour
{   
    // tree_Section is the UI tree.
    Rect Tree_Section;

    // objects called dialogues stored within this array.
    public Object[] Dialogues;
    
    // the current node in the dialouge array.
    public Dialogue currentNode;
    
    // the entire tree itself.
    public int tree;

    public bool usingSpeechLib; 
    // Voice, uses speechLib to produce speech from a text input.
    public SpVoice voice;

    // a thread that we want to run the speech system from.
    Thread newThread; 
    
    // has this thread been ran yet true or false?
    bool newThreadPause = false; 

       

    /**
     * init_Layout():
     * pre: none
     * post: builds the tree graphical components outside in unity
     * return: none void.
     */
    public void init_Layout()
    {
        Tree_Section.x = 0;
        Tree_Section.y = Screen.height;
        Tree_Section.width = Screen.width / 8f;
        Tree_Section.height = Screen.width - 50;

    }
 

    /**
     * Awake():
     * pre: none
     * post: Builds data when unity player first runs the game. Creates the dialouge tree, Voice synthisers for
     * the NPC. Runs the first dialouge prompt in the tree. This awake function does that  by building a new thread.
     * return: None void.
     */
    public void Awake()
    {
        voice = new SpVoice();
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + tree);
        currentNode = (Dialogue)Dialogues[0];

        if (usingSpeechLib)
        {
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
        }
        //display the prompt
        GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);

    }

    // force the NPC to stop speaking if unity stops running.
    /**
     * OnApplicationQuit():
     * pre:none
     * post: when unity player stops running, we will ensure the npc stops talking.
     * return:nothing void.
     */
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
    /**
     * inTree(string speech):
     * pre: the text that was said by the player.
     * post: finds the correct dialouge prompt within the tree and responds to the player. 
     * return: true if successfully  found prompt. Else false if not.
     */ 
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
                    
                    if (usingSpeechLib)
                    {


                        newThread = new Thread(runSpeech);



                        if (!newThread.IsAlive)
                        {
                            newThread.Start();
                        }
                        else
                        {
                            newThreadPause = !newThreadPause;
                        }
                    }

                    //and dislpay prompt in log

                    // GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
                    GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
                }
                return true;
            }
        }
        return false;
    }



   /**
    * runSpeech():
    * pre:none
    * post: for our speech thread, when we want to talk we will set it up on a sperate thread. This
    * function is placed on that thread.
    * 
    * return: nothing void.
    */ 

   private void runSpeech()
    {

        this.voice.Speak(currentNode.prompt); 


    }
}
