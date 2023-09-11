using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(PuzzleEffect))]
public class PuzzleEffectEditor : PropertyDrawer
{
    private float fieldSize = 16;
    private float padding = 2;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int fieldCount = 0;

        SerializedProperty triggerType = property.FindPropertyRelative("triggerType");
        SerializedProperty hasTriggerColor = property.FindPropertyRelative("hasTriggerColor");
        SerializedProperty effectType = property.FindPropertyRelative("effectType");
        SerializedProperty modificationType = property.FindPropertyRelative("modificationType");
        SerializedProperty limitPieceSelection = property.FindPropertyRelative("limitPieceSelection");
        SerializedProperty limitTargetColor = property.FindPropertyRelative("limitTargetColor");
        SerializedProperty amount = property.FindPropertyRelative("amount");
        SerializedProperty amountVariableSource = property.FindPropertyRelative("amountVariableSource");
        SerializedProperty amountLimitColor = property.FindPropertyRelative("amountLimitColor");
        SerializedProperty repeats = property.FindPropertyRelative("repeats");
        SerializedProperty repetitions = property.FindPropertyRelative("repetitions");
        SerializedProperty repetitionsVariableSource = property.FindPropertyRelative("repetitionsVariableSource");
        SerializedProperty repetitionsLimitColor = property.FindPropertyRelative("repetitionsLimitColor");

        fieldCount++;

        if ((TriggerType)triggerType.enumValueIndex == TriggerType.Adjacent || (TriggerType)triggerType.enumValueIndex == TriggerType.Connected || (TriggerType)triggerType.enumValueIndex == TriggerType.Chain)
        {
            fieldCount++;

            if (hasTriggerColor.boolValue)
            {
                fieldCount++;
            }
        }

        fieldCount++;

        if ((EffectType)effectType.enumValueIndex == EffectType.Buff || (EffectType)effectType.enumValueIndex == EffectType.Debuff)
        {
            fieldCount++;
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            fieldCount++;

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Destroy)
            {
                fieldCount++;
            }

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Color)
            {
                fieldCount++;
            }

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Shape)
            {
                fieldCount++;
            }
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.Damage || (EffectType)effectType.enumValueIndex == EffectType.Debuff || (EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            fieldCount++;
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            fieldCount++;

            if (limitPieceSelection.boolValue)
            {
                fieldCount++;

                if (limitTargetColor.boolValue)
                {
                    fieldCount++;
                }
            }
        }

        if ((EffectType)effectType.enumValueIndex != EffectType.None)
        {
            fieldCount++;

            if ((ValueType)amount.enumValueIndex == ValueType.Constant)
            {
                fieldCount++;
            }

            if ((ValueType)amount.enumValueIndex == ValueType.Random)
            {
                fieldCount++;
                fieldCount++;
            }

            if ((ValueType)amount.enumValueIndex == ValueType.Variable)
            {
                fieldCount++;

                if ((ConditionSource)amountVariableSource.enumValueIndex == ConditionSource.PuzzleBoard)
                {
                    fieldCount++;
                    fieldCount++;

                    if (amountLimitColor.boolValue)
                    {
                        fieldCount++;
                    }
                }

                fieldCount++;
                fieldCount++;
            }

            fieldCount++;

            if (repeats.boolValue)
            {
                fieldCount++;

                if ((ValueType)repetitions.enumValueIndex == ValueType.Constant)
                {
                    fieldCount++;
                }

                if ((ValueType)repetitions.enumValueIndex == ValueType.Random)
                {
                    fieldCount++;
                    fieldCount++;
                }

                if ((ValueType)repetitions.enumValueIndex == ValueType.Variable)
                {
                    fieldCount++;

                    if ((ConditionSource)repetitionsVariableSource.enumValueIndex == ConditionSource.PuzzleBoard)
                    {
                        fieldCount++;
                        fieldCount++;

                        if (repetitionsLimitColor.boolValue)
                        {
                            fieldCount++;
                        }
                    }

                    fieldCount++;
                    fieldCount++;
                }
            }
        }

        return (fieldCount * fieldSize) + ((fieldCount + 1) * padding);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = fieldSize;
        float positionIncrement = fieldSize + padding;

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty triggerType = property.FindPropertyRelative("triggerType");
        SerializedProperty hasTriggerColor = property.FindPropertyRelative("hasTriggerColor");
        SerializedProperty triggerColor = property.FindPropertyRelative("triggerColor");
        SerializedProperty effectType = property.FindPropertyRelative("effectType");
        SerializedProperty buffID = property.FindPropertyRelative("buffID");
        SerializedProperty modificationType = property.FindPropertyRelative("modificationType");
        SerializedProperty destroyCompletely = property.FindPropertyRelative("destroyCompletely");
        SerializedProperty newColor = property.FindPropertyRelative("newColor");
        SerializedProperty shapeChange = property.FindPropertyRelative("shapeChange");
        SerializedProperty targetType = property.FindPropertyRelative("targetType");
        SerializedProperty limitPieceSelection = property.FindPropertyRelative("limitPieceSelection");
        SerializedProperty limitTargetColor = property.FindPropertyRelative("limitTargetColor");
        SerializedProperty targetColor = property.FindPropertyRelative("targetColor");
        SerializedProperty amount = property.FindPropertyRelative("amount");
        SerializedProperty amountConstant = property.FindPropertyRelative("amountConstant");
        SerializedProperty amountMin = property.FindPropertyRelative("amountMin");
        SerializedProperty amountMax = property.FindPropertyRelative("amountMax");
        SerializedProperty amountVariableSource = property.FindPropertyRelative("amountVariableSource");
        SerializedProperty amountPuzzlePieceRange = property.FindPropertyRelative("amountPuzzlePieceRange");
        SerializedProperty amountLimitColor = property.FindPropertyRelative("amountLimitColor");
        SerializedProperty amountColor = property.FindPropertyRelative("amountColor");
        SerializedProperty amountVariableCoefficient = property.FindPropertyRelative("amountVariableCoefficient");
        SerializedProperty amountVariableConstant = property.FindPropertyRelative("amountVariableConstant");
        SerializedProperty repeats = property.FindPropertyRelative("repeats");
        SerializedProperty repetitions = property.FindPropertyRelative("repetitions");
        SerializedProperty repetitionsConstant = property.FindPropertyRelative("repetitionsConstant");
        SerializedProperty repetitionsMin = property.FindPropertyRelative("repetitionsMin");
        SerializedProperty repetitionsMax = property.FindPropertyRelative("repetitionsMax");
        SerializedProperty repetitionsVariableSource = property.FindPropertyRelative("repetitionsVariableSource");
        SerializedProperty repetitionsPuzzlePieceRange = property.FindPropertyRelative("repetitionsPuzzlePieceRange");
        SerializedProperty repetitionsLimitColor = property.FindPropertyRelative("repetitionsLimitColor");
        SerializedProperty repetitionsColor = property.FindPropertyRelative("repetitionsColor");
        SerializedProperty repetitionsVariableCoefficient = property.FindPropertyRelative("repetitionsVariableCoefficient");
        SerializedProperty repetitionsVariableConstant = property.FindPropertyRelative("repetitionsVariableConstant");

        EditorGUI.PropertyField(position, triggerType);
        position.y += positionIncrement;

        if ((TriggerType)triggerType.enumValueIndex == TriggerType.Adjacent || (TriggerType)triggerType.enumValueIndex == TriggerType.Connected || (TriggerType)triggerType.enumValueIndex == TriggerType.Chain)
        {
            EditorGUI.PropertyField(position, hasTriggerColor);
            position.y += positionIncrement;

            if (hasTriggerColor.boolValue)
            {
                EditorGUI.PropertyField(position, triggerColor);
                position.y += positionIncrement;
            }
        }

        EditorGUI.PropertyField(position, effectType);
        position.y += positionIncrement;

        if ((EffectType)effectType.enumValueIndex == EffectType.Buff || (EffectType)effectType.enumValueIndex == EffectType.Debuff)
        {
            EditorGUI.PropertyField(position, buffID);
            position.y += positionIncrement;
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            EditorGUI.PropertyField(position, modificationType);
            position.y += positionIncrement;

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Destroy)
            {
                EditorGUI.PropertyField(position, destroyCompletely);
                position.y += positionIncrement;
            }

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Color)
            {
                EditorGUI.PropertyField(position, newColor);
                position.y += positionIncrement;
            }

            if ((ModificationType)modificationType.enumValueIndex == ModificationType.Shape)
            {
                EditorGUI.PropertyField(position, shapeChange);
                position.y += positionIncrement;
            }
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.Damage || (EffectType)effectType.enumValueIndex == EffectType.Debuff || (EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            EditorGUI.PropertyField(position, targetType);
            position.y += positionIncrement;
        }

        if ((EffectType)effectType.enumValueIndex == EffectType.ModifyPiece)
        {
            EditorGUI.PropertyField(position, limitPieceSelection);
            position.y += positionIncrement;

            if (limitPieceSelection.boolValue)
            {
                EditorGUI.PropertyField(position, limitTargetColor);
                position.y += positionIncrement;

                if (limitTargetColor.boolValue)
                {
                    EditorGUI.PropertyField(position, targetColor);
                    position.y += positionIncrement;
                }
            }
        }

        if ((EffectType)effectType.enumValueIndex != EffectType.None)
        {
            EditorGUI.PropertyField(position, amount);
            position.y += positionIncrement;

            if ((ValueType)amount.enumValueIndex == ValueType.Constant)
            {
                EditorGUI.PropertyField(position, amountConstant);
                position.y += positionIncrement;
            }

            if ((ValueType)amount.enumValueIndex == ValueType.Random)
            {
                EditorGUI.PropertyField(position, amountMin);
                position.y += positionIncrement;
                EditorGUI.PropertyField(position, amountMax);
                position.y += positionIncrement;
            }

            if ((ValueType)amount.enumValueIndex == ValueType.Variable)
            {
                EditorGUI.PropertyField(position, amountVariableSource);
                position.y += positionIncrement;

                if ((ConditionSource)amountVariableSource.enumValueIndex == ConditionSource.PuzzleBoard)
                {
                    EditorGUI.PropertyField(position, amountPuzzlePieceRange);
                    position.y += positionIncrement;
                    EditorGUI.PropertyField(position, amountLimitColor);
                    position.y += positionIncrement;

                    if (amountLimitColor.boolValue)
                    {
                        EditorGUI.PropertyField(position, amountColor);
                        position.y += positionIncrement;
                    }
                }

                EditorGUI.PropertyField(position, amountVariableCoefficient);
                position.y += positionIncrement;
                EditorGUI.PropertyField(position, amountVariableConstant);
                position.y += positionIncrement;
            }

            EditorGUI.PropertyField(position, repeats);
            position.y += positionIncrement;

            if (repeats.boolValue)
            {
                EditorGUI.PropertyField(position, repetitions);
                position.y += positionIncrement;

                if ((ValueType)repetitions.enumValueIndex == ValueType.Constant)
                {
                    EditorGUI.PropertyField(position, repetitionsConstant);
                    position.y += positionIncrement;
                }

                if ((ValueType)repetitions.enumValueIndex == ValueType.Random)
                {
                    EditorGUI.PropertyField(position, repetitionsMin);
                    position.y += positionIncrement;
                    EditorGUI.PropertyField(position, repetitionsMax);
                    position.y += positionIncrement;
                }

                if ((ValueType)repetitions.enumValueIndex == ValueType.Variable)
                {
                    EditorGUI.PropertyField(position, repetitionsVariableSource);
                    position.y += positionIncrement;

                    if ((ConditionSource)repetitionsVariableSource.enumValueIndex == ConditionSource.PuzzleBoard)
                    {
                        EditorGUI.PropertyField(position, repetitionsPuzzlePieceRange);
                        position.y += positionIncrement;
                        EditorGUI.PropertyField(position, repetitionsLimitColor);
                        position.y += positionIncrement;

                        if (repetitionsLimitColor.boolValue)
                        {
                            EditorGUI.PropertyField(position, repetitionsColor);
                            position.y += positionIncrement;
                        }
                    }

                    EditorGUI.PropertyField(position, repetitionsVariableCoefficient);
                    position.y += positionIncrement;
                    EditorGUI.PropertyField(position, repetitionsVariableConstant);
                    position.y += positionIncrement;
                }
            }
        }

        EditorGUI.EndProperty();
    }
}
