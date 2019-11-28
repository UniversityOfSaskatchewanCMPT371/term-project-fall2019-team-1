using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IDialogueTree
{
    Dialogue currentNode
    {
        get;
        set;
    }
}
