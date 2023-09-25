using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBuffEnemy : ActiveEffect
{
    private BuffID buff;
    private TargetType targetType;
    private int amount, repetitions;

    public EffectBuffEnemy(BuffID buff, TargetType targetType, int amount, int repetitions)
    {
        this.buff = buff;
        this.targetType = targetType;
        this.amount = amount;
        this.repetitions = repetitions;
    }

    public BuffID GetBuff()
    {
        return buff;
    }

    public TargetType GetTargetType()
    {
        return targetType;
    }

    public int GetAmount()
    {
        return amount;
    }

    public int GetRepetitions()
    {
        return repetitions;
    }
}
