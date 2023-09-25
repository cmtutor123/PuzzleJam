using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShapePiece : ActiveEffect
{
    private TargetType targetType;
    private PuzzleColor colorCondition;
    private ShapeChange newShape;
    private int index, repetitions;

    public EffectShapePiece(int index, TargetType targetType, PuzzleColor colorCondition, ShapeChange newShape, int repetitions)
    {
        this.index = index;
        this.targetType = targetType;
        this.colorCondition = colorCondition;
        this.newShape = newShape;
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

    public ShapeChange GetNewShape()
    {
        return newShape;
    }

    public int GetRepetitions()
    {
        return repetitions;
    }
}
