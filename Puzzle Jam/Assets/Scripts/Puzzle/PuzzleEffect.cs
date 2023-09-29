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

    ///<returns> Returns the TriggerColor </returns> 
    public PuzzleColor GetTriggerColor()
    {
        return triggerColor;
    }

    ///<returns> Returns the EffectType </returns> 
    public EffectType GetEffectType()
    {
        return effectType;
    }

    ///<returns> Returns the BuffID </returns> 
    public BuffID GetBuffID()
    {
        return buffID;
    }

    ///<returns> Returns the ModificationType </returns> 
    public ModificationType GetModificationType()
    {
        return modificationType;
    }

    ///<returns> Returns if it is completely destroyed </returns> 
    public bool GetDestroyCompletely()
    {
        return destroyCompletely;
    }

    ///<returns> Returns the new Color </returns> 
    public PuzzleColor GetNewColor()
    {
        return newColor;
    }

    ///<returns> Returns the ShapeChange </returns> 
    public ShapeChange GetShapeChange()
    {
        return shapeChange;
    }

    ///<returns> Returns the TargetType </returns> 
    public TargetType GetTargetType()
    {
        return targetType;
    }

    ///<returns> Returns if PieceSelection is limited </returns> 
    public bool GetLimitPieceSelection()
    {
        return limitPieceSelection;
    }

    ///<returns> Returns if TargetColor is limited </returns> 
    public bool GetLimitTargetColor()
    {
        return limitTargetColor;
    }

    ///<returns> Returns the TargetColor </returns> 
    public PuzzleColor GetTargetColor()
    {
        return targetColor;
    }

    ///<returns> Returns the Amount ValueType </returns> 
    public ValueType GetAmount()
    {
        return amount;
    }

    ///<returns> Returns the Amount Constant </returns>
    public int GetAmountConstant()
    {
        return amountConstant;
    }

    // ... continue for all other getters


    ///<returns> Returns the minimum Amount </returns> 
    public int GetAmountMin()
    {
        return amountMin;
    }

    ///<returns> Returns the maximum Amount </returns> 
    public int GetAmountMax()
    {
        return amountMax;
    }

    ///<returns> Returns the Amount Variable Source Condition </returns> 
    public ConditionSource GetAmountVariableSource()
    {
        return amountVariableSource;
    }

    ///<returns> Returns the Amount Puzzle Piece Range </returns> 
    public PuzzlePieceRange GetAmountPuzzlePieceRange()
    {
        return amountPuzzlePieceRange;
    }

    ///<returns> Returns if Amount Color is limited </returns> 
    public bool GetAmountLimitColor()
    {
        return amountLimitColor;
    }

    ///<returns> Returns the Amount Color </returns> 
    public PuzzleColor GetAmountColor()
    {
        return amountColor;
    }

    ///<returns> Returns the Amount Variable Coefficient </returns> 
    public int GetAmountVariableCoefficient()
    {
        return amountVariableCoefficient;
    }

    ///<returns> Returns the Amount Variable Constant </returns>
    public int GetAmountVariableConstant()
    {
        return amountVariableConstant;
    }

    ///<returns> Returns if it repeats </returns>
    public bool GetRepeats()
    {
        return repeats;
    }

    ///<returns> Returns the Repetitions ValueType </returns>
    public ValueType GetRepetitions()
    {
        return repetitions;
    }

    ///<returns> Returns the Repetitions Constant </returns>
    public int GetRepetitionsConstant()
    {
        return repetitionsConstant;
    }

    ///<returns> Returns the minimum Repetitions </returns>
    public int GetRepetitionsMin()
    {
        return repetitionsMin;
    }

    ///<returns> Returns the maximum Repetitions </returns>
    public int GetRepetitionsMax()
    {
        return repetitionsMax;
    }

    ///<returns> Returns the Repetitions Variable Source Condition </returns>
    public ConditionSource GetRepetitionsVariableSource()
    {
        return repetitionsVariableSource;
    }

    ///<returns> Returns the Repetitions Puzzle Piece Range </returns>
    public PuzzlePieceRange GetRepetitionsPuzzlePieceRange()
    {
        return repetitionsPuzzlePieceRange;
    }

    ///<returns> Returns if Repetitions Color is limited </returns>
    public bool GetRepetitionsLimitColor()
    {
        return repetitionsLimitColor;
    }

    ///<returns> Returns the Repetitions Color </returns>
    public PuzzleColor GetRepetitionsColor()
    {
        return repetitionsColor;
    }

    ///<returns> Returns the Repetitions Variable Coefficient </returns>
    public int GetRepetitionsVariableCoefficient()
    {
        return repetitionsVariableCoefficient;
    }

    ///<returns> Returns the Repetitions Variable Constant </returns>
    public int GetRepetitionsVariableConstant()
    {
        return repetitionsVariableConstant;
    }


    /*
    public EffectType GetEffectType()
    {
        return effectType;
    }

    public ValueType GetAmountValueType()
    {
        return amount;
    }

    public int GetAmountConstant()
    {
        return amountConstant;
    }

    public int GetAmountMin()
    {
        return amountMin;
    }

    public int GetAmountMax()
    {
        return amountMax;
    }

    public int GetAmountVariableConstant()
    {
        return amountVariableConstant;
    }

    public int GetAmountVariableCoefficient()
    {
        return amountVariableCoefficient;
    }

    public ConditionSource GetAmountVariableSource()
    {
        return amountVariableSource;
    }

    public PuzzlePieceRange GetAmountPuzzlePieceRange()
    {
        return amountPuzzlePieceRange;
    }

    public bool GetAmountLimitColor()
    {
        return amountLimitColor;
    }

    public PuzzleColor GetAmountColor()
    {
        return amountColor;
    }

    public bool GetRepeats()
    {
        return repeats;
    }
    public ValueType GetRepetitionsValueType()
    {
        return repetitions;
    }

    public int GetRepetitionsConstant()
    {
        return repetitionsConstant;
    }

    public int GetRepetitionsMin()
    {
        return repetitionsMin;
    }

    public int GetRepetitionsMax()
    {
        return repetitionsMax;
    }

    public TargetType GetTargetType()
    {
        return targetType;
    }
    */
}
