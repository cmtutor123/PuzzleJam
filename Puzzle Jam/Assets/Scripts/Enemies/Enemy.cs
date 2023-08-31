using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private string enemyName;
    private int maxHealth, currentHealth;
    private Sprite spriteIdle;
    private EnemyAttackPattern attackPattern;

    public Enemy(EnemyData enemyData)
    {
        enemyName = enemyData.GetName();
        maxHealth = enemyData.GetMaxHealth();
        currentHealth = maxHealth;
        spriteIdle = enemyData.GetSpriteIdle();
        attackPattern = enemyData.GetEnemyAttackPattern();
    }

    public string GetName()
    {
        return enemyName;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public Sprite GetSpriteIdle()
    {
        return spriteIdle;
    }

    public EnemyAttackPattern GetEnemyAttackPattern()
    {
        return attackPattern;
    }
}
