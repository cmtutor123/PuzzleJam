using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamagePlayer : ActiveEffect
{
    private int damage, repetitions;

    public EffectDamagePlayer(int damage, int repetitions)
    {
        this.damage = damage;
        this.repetitions = repetitions;
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
