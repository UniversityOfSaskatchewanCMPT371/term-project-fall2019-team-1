using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeTree_Test : MonoBehaviour
{
    public string test = "hello one";

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogueTree>().inTree(test);
            
    }
   
}
