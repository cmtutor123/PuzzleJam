using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data for active enemies in combat
/// </summary>
public class Enemy
{
    private string enemyName;
    private int maxHealth, currentHealth;
    private Sprite spriteIdle;
    private EnemyAttackPattern attackPattern;

    /// <param name="enemyData">The EnemyData to load from</param>
    public Enemy(EnemyData enemyData)
    {
        enemyName = enemyData.GetName();
        maxHealth = enemyData.GetMaxHealth();
        currentHealth = maxHealth;
        spriteIdle = enemyData.GetSpriteIdle();
        attackPattern = enemyData.GetEnemyAttackPattern();
    }

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

    /// <returns>The enemy's current health</returns>
    public int GetCurrentHealth()
    {
        return currentHealth;
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

    /// <returns>Whether the enemy has health remaining</returns>
    public bool Alive()
    {
        return currentHealth > 0;
    }

    /// <returns>Whether the enemy can be targeted</returns>
    public bool ValidTarget()
    {
        return Alive();
    }
}
