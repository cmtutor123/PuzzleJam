using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBuffPlayer : ActiveEffect
{
    private BuffID buff;
    private int amount, repetitions;

    public EffectBuffPlayer(BuffID buff, int amount, int repetitions)
    {
        this.buff = buff;
        this.amount = amount;
        this.repetitions = repetitions;
    }

    public BuffID GetBuff()
    {
        return buff;
    }

    public int GetAmount()
    {
        return amount;
    }

    public int GetRepetitons()
    {
        return repetitions;
    }
}
