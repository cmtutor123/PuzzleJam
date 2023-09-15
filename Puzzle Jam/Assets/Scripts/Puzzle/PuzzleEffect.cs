using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// An effect that is triggered by a PuzzlePiece
/// </summary>
[Serializable]
public class PuzzleEffect
{
    //  Trigger
    [SerializeField] private TriggerType triggerType;
    //      if Adjacent, Connected, or Chain
    [SerializeField] private bool hasTriggerColor;
    //          if hasTriggerColor
    [SerializeField] private PuzzleColor triggerColor;
    //  Effect
    [SerializeField] private EffectType effectType;
    //      if Buff or Debuff
    [SerializeField] private BuffID buffID;
    //      if ModifyPiece
    [SerializeField] private ModificationType modificationType;
    //          if Destroy
    [SerializeField] private bool destroyCompletely;
    //          if Color
    [SerializeField] private PuzzleColor newColor;
    //          if Shape
    [SerializeField] private ShapeChange shapeChange;
    //      if Damage, Debuff, ModifyPiece
    [SerializeField] private TargetType targetType;
    //      if ModifyPiece
    [SerializeField] private bool limitPieceSelection;
    //          if limitPieceSelection
    [SerializeField] private bool limitTargetColor;
    //              if limitTargetColor
    [SerializeField] private PuzzleColor targetColor;
    //      if not None  
    [SerializeField] private ValueType amount;
    //      if Constant
    [SerializeField] private int amountConstant;
    //      if Random
    [SerializeField] private int amountMin;
    [SerializeField] private int amountMax;
    //      if Variable
    [SerializeField] private ConditionSource amountVariableSource;
    //          if PuzzleBoard
    [SerializeField] private PuzzlePieceRange amountPuzzlePieceRange;
    [SerializeField] private bool amountLimitColor;
    //              if amountLimitColor
    [SerializeField] private PuzzleColor amountColor;
    //      if Variable
    [SerializeField] private int amountVariableCoefficient;
    [SerializeField] private int amountVariableConstant;
    //      if not None
    [SerializeField] private bool repeats;
    //      if repeats
    [SerializeField] private ValueType repetitions;
    //          if Constant
    [SerializeField] private int repetitionsConstant;
    //          if Random
    [SerializeField] private int repetitionsMin;
    [SerializeField] private int repetitionsMax;
    //      if Variable
    [SerializeField] private ConditionSource repetitionsVariableSource;
    //          if PuzzleBoard
    [SerializeField] private PuzzlePieceRange repetitionsPuzzlePieceRange;
    [SerializeField] private bool repetitionsLimitColor;
    //              if amountLimitColor
    [SerializeField] private PuzzleColor repetitionsColor;
    //      if Variable
    [SerializeField] private int repetitionsVariableCoefficient;
    [SerializeField] private int repetitionsVariableConstant;

    /// <returns>The TriggerType of the PuzzleEffect</returns>
    public TriggerType GetTriggerType()
    {
        return triggerType;
    }

    /// <returns>Whether the PuzzleEffect has a condition to its trigger</returns>
    public bool HasTriggerConditions()
    {
        if (triggerType == TriggerType.Adjacent || triggerType == TriggerType.Connected || triggerType == TriggerType.Chain)
        {
            if (hasTriggerColor)
            {
                return true;
            }
        }
        return false;
    }

    public EffectType GetEffectType()
    {
        return effectType;
    }


}
