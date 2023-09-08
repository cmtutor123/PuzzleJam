using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private string enemyName;
    private int maxHealth, currentHealth;
    private Sprite spriteIdle;
    private EnemyAttackPattern attackPattern;

    // creates an Enemy from EnemyData
    public Enemy(EnemyData enemyData)
    {
        enemyName = enemyData.GetName();
        maxHealth = enemyData.GetMaxHealth();
        currentHealth = maxHealth;
        spriteIdle = enemyData.GetSpriteIdle();
        attackPattern = enemyData.GetEnemyAttackPattern();
    }

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

    // returns the enemy's current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // returns the idle Sprite
    public Sprite GetSpriteIdle()
    {
        return spriteIdle;
    }

    // returns the enemy's EnemyAttackPattern
    public EnemyAttackPattern GetEnemyAttackPattern()
    {
        return attackPattern;
    }

    // returns true if the enemy has hp remaining
    public bool Alive()
    {
        return currentHealth > 0;
    }

    // returns true if the enemy can be targeted
    public bool ValidTarget()
    {
        return Alive();
    }
}
