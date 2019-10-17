using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{ 
    Object[] Dialogues;
    Dialogue currentNode;
        
    public void Awake()
    {
        Dialogues = Resources.LoadAll("DialogueTree/Tree1");
        currentNode = (Dialogue)Dialogues[0];
    }


    public bool inTree(string speech)
    {
        //for each response in the current node
        for(int i = 0; i < currentNode.response.Count; i++)
        {
            //if the response is the same as the string
            if(currentNode.response[i] == speech)
            {
                //TODO: send to text-to-speech
                Debug.Log(speech);

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
