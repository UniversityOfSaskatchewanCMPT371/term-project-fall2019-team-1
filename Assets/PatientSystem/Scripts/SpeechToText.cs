using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// <c>SpeechToText</c>
/// Description: This handles an audio file as input and will return back a string.
/// 
/// Pre-condition: Audio system needs to be implemented to detect possible audio devices. 
/// 
/// Post-condition: system handels  all player input and produces a string equaliviant of what they said. 
/// </summary>
/// 
/// <authors>
/// Mason Demerais
/// </authors>
public class SpeechToText : MonoBehaviour
{
    
    // The Language Engine of the system.
   
    public LanguageEngine LE;


    // The logger.
  
    public Log log;

    /// <summary>
    /// 
    /// <c>ReceiveAudioFile</c>
    /// 
    /// Description: Receives an audio file path to then send the interpreted string to the LanguageEngine.
    /// 
    /// pre-conditions: The file must exist and be a valid audio file.
    ///
    /// Post-Conditions: None
    /// </summary>
    /// <param name="fileName">The audio file to interpret.</param>
    ///  <returns> NULL </returns>
    public void ReceiveAudioFile(string fileName)
    {
        log.WriteToLog(string.Format("SpeechToText::ReceiveAudioFile: fileName: {0}", fileName));

        string whatWasSaid = AudioToString(fileName);

        log.WriteToLog(string.Format("SpeechToText::ReceiveAudioFile: whatWasSaid: {0}", whatWasSaid));

        LE.RecieveInput(whatWasSaid);
    }

    /// <summary>
    /// 
    /// <c>AudioToString</c>
    /// 
    /// Description:Returns a string from a given audio file.
    /// 
    /// pre-conditions: The file must exist and be a valid audio file.
    /// 
    /// Post-Conditions: None
    /// </summary>
    /// <param name="fileName">The audio file to interpret.</param>
    /// <returns>The Speech To Text</returns>
    public string AudioToString(string fileName)
    {
        return "hello";
    }
}
