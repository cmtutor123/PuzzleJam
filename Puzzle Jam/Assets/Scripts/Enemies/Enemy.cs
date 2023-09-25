using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Holds the data for active enemies in combat
/// </summary>
[Serializable]
public class Enemy
{
    private string enemyName;
    private int maxHealth, currentHealth, turnCounter;
    private Sprite spriteIdle;
    private EnemyAttackPattern attackPattern;
    private Dictionary<BuffID, int> buffs;

    /// <param name="enemyData">The EnemyData to load from</param>
    public Enemy(EnemyData enemyData)
    {
        buffs = new Dictionary<BuffID, int>();
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

    public PuzzleData GetNextAttack()
    {
        return attackPattern.GetAttack(turnCounter);
    }

    public void Damage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, maxHealth);
        currentHealth -= damage;
    }

    public void ApplyBuff(BuffID buff, int amount)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] += amount;
        }
        else
        {
            buffs.Add(buff, amount);
        }
    }

    public void RemoveBuff(BuffID buff, int amount)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] -= amount;
            if (buffs[buff] < 0) buffs[buff] = 0;
        }
        else
        {
            buffs.Add(buff, 0);
        }
    }

    public void RemoveAllBuff(BuffID buff)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] = 0;
        }
        else
        {
            buffs.Add(buff, 0);
        }
    }

    public bool HasBuff(BuffID buff)
    {
        return buffs.ContainsKey(buff) && buffs[buff] > 0;
    }

    public int GetBuffAmount(BuffID buff)
    {
        if (HasBuff(buff)) return buffs[buff];
        else return 0;
    }
}
