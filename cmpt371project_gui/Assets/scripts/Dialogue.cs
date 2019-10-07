using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public int id;
    public string prompt;
    public string[] response;
    public int[] next; 


}
