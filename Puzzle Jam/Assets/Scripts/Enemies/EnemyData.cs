using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private int maxHealth;
    [SerializeField] private Sprite spriteIdle;
    [SerializeField] private EnemyAttackPattern attackPattern;

    public string GetName()
    {
        return enemyName;
    }    
    
    public int GetMaxHealth()
    {
        return maxHealth;
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
