using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using System.Diagnostics;
using UnityEngine.UI;
using SpeechLib; 
using UnityEngine.Windows.Speech;





/**
 * Rough prototype to see how Text-to-Speech could theoritically could work. 
 * https://forum.unity.com/threads/text-to-speech-windows-success-and-failure.497313/
 * 
 */
public class speechToText : MonoBehaviour
{


    public GameObject textField;

   

    

    // Start is called before the first frame update
    void Start()
    {

      

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            SpVoice voice;
            voice = new SpVoice();
            voice.Speak(textField.GetComponent<Text>().text);


            // XML test:

            voice.Speak("< speak version = '1.0' xmlns = 'http://www.w3.org/2001/10/synthesis' xml: lang = 'en-US' > "
                        + " HI DUDE <spell>test</spell> ",
                        SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML); 
            


        }
        
    }
}
