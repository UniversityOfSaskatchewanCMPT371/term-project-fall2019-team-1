using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DialogueTreeMock : IDialogueTree
{
    public Dialogue currentNode { get; set; }

    public string inTreeLastSpeech;

    public bool inTree(string speech)
    {
        inTreeLastSpeech = speech;
        if (speech == "y") return true;
        return false;
    }
}
