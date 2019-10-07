using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Node : ScriptableObject
{
    public Rect windowRect;

    public bool hasInputs;

    public string windowTitle;

    public virtual void DrawWindow(){
    }

    public virtual void DrawCurves(){

    }

}
