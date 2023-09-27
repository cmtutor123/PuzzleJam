using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHealPlayer : ActiveEffect
{
    private int amount, repetitions;

    public EffectHealPlayer(int amount, int repetitions)
    {
        this.amount = amount;
        this.repetitions = repetitions;
    }

    public int GetAmount()
    {
        return amount;
    }

    public override int GetRepetitions()
    {
        return repetitions;
    }
}
