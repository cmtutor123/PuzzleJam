using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data for attack patterns used by enemies
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Attack Pattern", menuName = "Enemy/Attack Pattern")]
public class EnemyAttackPattern : ScriptableObject
{
    [Header("Pattern")]
    [SerializeField] private AttackPatternType patternType;
    [Header("Attacks")]
    [SerializeField] private List<PuzzleData> enemyAttacks;

    public PuzzleData GetAttack(int turn)
    {
        if (enemyAttacks == null || enemyAttacks.Count == 0)
        {
            return null;
        }
        else
        {
            switch (patternType)
            {
                case AttackPatternType.Single:
                    return enemyAttacks[0];
                case AttackPatternType.RepeatLast:
                    if (turn >= enemyAttacks.Count)
                    {
                        turn = enemyAttacks.Count - 1;
                    }
                    return enemyAttacks[turn];
                case AttackPatternType.Loop:
                    turn = turn % enemyAttacks.Count;
                    return enemyAttacks[turn];
                case AttackPatternType.Random:
                    return enemyAttacks[Random.Range(0, enemyAttacks.Count)];
            }
            return null;
        }
    }
}
