using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColorPiece : ActiveEffect
{
    private TargetType targetType;
    private PuzzleColor colorCondition, newColor;
    private int index, repetitions;

    public EffectColorPiece(int index, TargetType targetType, PuzzleColor colorCondition, PuzzleColor newColor, int repetitions)
    {
        this.index = index;
        this.targetType = targetType;
        this.colorCondition = colorCondition;
        this.newColor = newColor;
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

    public PuzzleColor GetNewColor()
    {
        return newColor;
    }

    public override int GetRepetitions()
    {
        return repetitions;
    }
    
    public override void ReduceRepetition()
    {
        repetitions--;
    }
}
