using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Encounter", menuName = "Enemy/Encounter")]
public class EnemyEncounter : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemies;

    public List<Enemy> GetEnemies()
    {
        List<Enemy> enemyList = new List<Enemy>();
        foreach (EnemyData enemyData in enemies)
        {
            enemyList.Add(new Enemy(enemyData));
        }
        return enemyList;
    }

    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}
