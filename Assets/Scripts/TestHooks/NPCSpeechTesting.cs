using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class NPCSpeechTesting : MonoBehaviour
{
    SpVoice voice;
    public Button btn;
    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        voice = new SpVoice();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPressed()
    {
        voice.Speak(input.GetComponentInChildren<Text>().text);
    }
}
