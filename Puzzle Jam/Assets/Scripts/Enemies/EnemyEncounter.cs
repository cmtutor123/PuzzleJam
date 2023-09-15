using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data on the number and type of enemies in an enemy encounter
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Encounter", menuName = "Enemy/Encounter")]
public class EnemyEncounter : ScriptableObject
{
    [Header("Enemies")]
    [SerializeField] private List<EnemyData> enemies;

    /// <returns>A list of Enemy objects generated from the list of EnemyData</returns>
    public List<Enemy> GetEnemies()
    {
        List<Enemy> enemyList = new List<Enemy>();
        foreach (EnemyData enemyData in enemies)
        {
            enemyList.Add(new Enemy(enemyData));
        }
        return enemyList;
    }

    /// <returns>The number of enemies in the encounter</returns>
    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}
