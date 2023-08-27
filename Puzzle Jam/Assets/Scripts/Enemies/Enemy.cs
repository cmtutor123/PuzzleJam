using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string enemyName;
    public int maxHealth, currentHealth;
    public Sprite spriteIdle;
    public EnemyAttackPattern attackPattern;

    public Enemy(EnemyData data)
    {
        enemyName = data.enemyName;
        maxHealth = data.health;
        currentHealth = data.health;
        spriteIdle = data.spriteIdle;
        attackPattern = data.attackPattern;
    }
}
