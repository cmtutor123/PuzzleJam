using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data of enemies that Enemy objects load from
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    [Header("Name")]
    [SerializeField] private string enemyName;
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [Header("Sprites")]
    [SerializeField] private Sprite spriteIdle;
    [Header("Attack Pattern")]
    [SerializeField] private EnemyAttackPattern attackPattern;

    /// <returns>The enemy's name</returns>
    public string GetName()
    {
        return enemyName;
    }    
    
    /// <returns>The enemy's max health</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <returns>The enemy's idle sprite</returns>
    public Sprite GetSpriteIdle()
    {
        return spriteIdle;
    }

    /// <returns>The enemy's EnemyAttackPattern</returns>
    public EnemyAttackPattern GetEnemyAttackPattern()
    {
        return attackPattern;
    }
}
