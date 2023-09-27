using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The effects that occur on the battlefield
/// </summary>
public abstract class ActiveEffect
{
    public virtual void ReduceRepetition()
    {

    }

    public virtual int GetRepetitions()
    {
        return 0;
    }
}
