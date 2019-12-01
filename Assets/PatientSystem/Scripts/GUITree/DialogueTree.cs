using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading;
using System.Security.Permissions;
using System.Runtime.InteropServices;

/// <summary>
/// 
/// <c>DialougeTree</c>
/// 
/// Description:DialogueTree, is the under laying system that runs the NPC. It stores NPC responses and 
/// allows the NPC to interact with the player by converting text to speech using the SpeechLib.
/// 
/// post-condition: finds the correct prompt to respond to the players speech input
/// 
/// pre-condition: nothing 
/// 
/// </summary>
/// <authors>Clayton VanderStelt, Kareem Oluwaseyi, Matt Radke</authors>
public class DialogueTree : MonoBehaviour, IDialogueTree
{   
    // tree_Section is the UI tree.
    Rect Tree_Section;

    // objects called dialogues stored within this array.
    public Object[] Dialogues;
    
    // the current node in the dialouge array.
    public Dialogue currentNode { get; set; }

    // the entire tree itself.
    public int tree;

    public bool usingSpeechLib; 
    // Voice, uses speechLib to produce speech from a text input.
    public SpVoice voice;

    // a thread that we want to run the speech system from.
    Thread newThread; 
    
    // has this thread been ran yet true or false?
    bool newThreadPause = false;



    /// <summary>
    /// Description: builds layout for tree.
    /// 
    /// pre-condition: none
    /// 
    /// post-condition: layout for tree.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
    public void init_Layout()
    {
        Tree_Section.x = 0;
        Tree_Section.y = Screen.height;
        Tree_Section.width = Screen.width / 8f;
        Tree_Section.height = Screen.width - 50;

    }



    /// <summary>
    /// 
    /// <c>Awake</c>
    /// 
    /// Description: Builds data before game is ran. 
    /// 
    /// pre-condition: None
    /// 
    /// post-Condition: All nessarcy items for tree is built and ready to use. 
    /// 
    /// </summary>
    /// 
    /// <returns>NULL</returns>
    public void Awake()
    {
        voice = new SpVoice();
        Dialogues = Resources.LoadAll("DialogueTree/Tree" + tree);

        //find the head node
        this.currentNode = ScriptableObject.CreateInstance<Dialogue>();

        for (int i = 0; i < Dialogues.Length; i++)
        {
            //if this node belongs to the current tree, and is the head of that tree
            if ((((Dialogue)Dialogues[i]).tree == tree) && (((Dialogue)Dialogues[i]).start == true))
            {
                

                currentNode = (Dialogue)Dialogues[i];

                goto Found;
            }
            else
            {

                //if it cant be found, set it ot the first one that appears.
                currentNode = (Dialogue)Dialogues[0];
            }
        }



        Found:

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
        //GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);

    }


    /// <summary>
    /// <c>OnApplicationQuit</c>
    /// 
    /// Description: takes care of speechLib talking once the main thread is terminated.
    /// 
    /// Pre-condition: None
    /// 
    /// Post-condition: stops AI from speaking in world.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
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
 
    /// <summary>
    /// <c><inTree</c>
    /// Description: find a node in a certian tree.
    /// 
    /// pre-condition: The text that was said by the player.
    /// 
    /// Post-condition finds the correct prompt within the treee and responds to the player.
    /// 
    /// </summary>
    /// 
    /// <param name="speech">string for speech system</param>
    /// 
    /// <returns>true if successful, else false.</returns>
    public bool inTree(string speech)
    {
        if (this.usingSpeechLib)
        {

            return this.speechLib(speech); 

        }

        return false; 
    }
   
    /// <summary>
    /// 
    /// <c>runSpeech</c>
    /// 
    /// Description: small function that runs on a thread.
    /// Pre-Condition: None
    /// Post-Condition:for our speech thread, when we want to talk we will set it up on a sperate thread. This
    /// function is placed on that thread.
    /// 
    /// </summary>
    /// <returns>NULL</returns>
   private void runSpeech()
    {

        this.voice.Speak( this.currentNode.prompt ); 


    }


    private bool speechLib(string speech)
    {

        Debug.Log("yeetyaw"); 

        //for each response in the current node
        for (int i = 0; i < currentNode.response.Count; i++)
        {
            //if the response is the same as the string
            if (currentNode.response[i].ToLower() == speech.ToLower())
            {
                Debug.Log(speech + " matches " + currentNode.response[i]);

                //check if current node has a next 
                if (currentNode.next.Count == 0)
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
                   // GameObject.FindGameObjectWithTag("Log").GetComponent<LogSystem>().WriteToFile(currentNode.prompt);
                }

                return true;
            }

        }

        return false;

    }
}
