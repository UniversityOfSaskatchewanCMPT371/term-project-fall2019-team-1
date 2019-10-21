using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeTree_Test : MonoBehaviour
{   
    private string test;

    // Start is called before the first frame update
    void Start()
    {
        test = "hello";
        GetComponent<DialogueTree>().inTree(test);
        test = "goodbye";
        GetComponent<DialogueTree>().inTree(test);
        test = "finish the tree";
        GetComponent<DialogueTree>().inTree(test);

    }
   
}
