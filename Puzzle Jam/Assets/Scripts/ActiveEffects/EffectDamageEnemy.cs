using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamageEnemy : ActiveEffect
{
    private TargetType targetType;
    private int damage, repetitions;

    public EffectDamageEnemy(TargetType targetType, int damage, int repetitions)
    {
        this.targetType = targetType;
        this.damage = damage;
        this.repetitions = repetitions;
    }

    public TargetType GetTargetType()
    {
        return targetType;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetRepetitions()
    {
        return repetitions;
    }
}
