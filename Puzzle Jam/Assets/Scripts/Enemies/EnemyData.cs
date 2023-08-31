using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // returns the enemy's name
    public string GetName()
    {
        return enemyName;
    }    
    
    // returns the enemy's max health
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // returns the enemy's idle Sprite
    public Sprite GetSpriteIdle()
    {
        return spriteIdle;
    }

    // returns the enemy's EnemyAttackPattern
    public EnemyAttackPattern GetEnemyAttackPattern()
    {
        return attackPattern;
    }
}
