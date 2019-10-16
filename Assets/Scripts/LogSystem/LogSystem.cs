using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LogSystem : MonoBehaviour
{
    // Text file used for logging. Drag and drop file in editor.
    public TextAsset logFile;

    // For writing to log file
    private StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {
        // Clear the log file when scene starts.
        File.WriteAllText(AssetDatabase.GetAssetPath(logFile), string.Empty);

        // Build stream writer for the log file.
        sw = new StreamWriter(AssetDatabase.GetAssetPath(logFile));

        sw.WriteLine("Yeet");
        sw.Close();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
