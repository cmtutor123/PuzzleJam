using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types of Effects that a PuzzlePiece can do
/// </summary>
public enum EffectType
{
    None,
    Damage,
    Heal,
    Shield,
    Buff,
    Debuff,
    ModifyPiece,
    SelfDamage
}
