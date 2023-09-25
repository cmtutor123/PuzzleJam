using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyPiece : ActiveEffect
{
    private TargetType targetType;
    private PuzzleColor colorCondition;
    private int index, repetitions;

    public EffectDestroyPiece(int index, TargetType targetType, PuzzleColor colorCondition, int repetitions)
    {
        this.index = index;
        this.targetType = targetType;
        this.colorCondition = colorCondition;
        this.repetitions = repetitions;
    }

    public int GetIndex()
    {
        return index;
    }

    public TargetType GetTargetType()
    {
        return targetType;
    }

    public PuzzleColor GetColorCondition()
    {
        return colorCondition;
    }

    public int GetRepetitions()
    {
        return repetitions;
    }
}
