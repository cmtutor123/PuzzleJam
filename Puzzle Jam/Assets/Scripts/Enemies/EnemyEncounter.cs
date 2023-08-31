using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Encounter", menuName = "Enemy/Encounter")]
public class EnemyEncounter : ScriptableObject
{
    [Header("Enemies")]
    [SerializeField] private List<EnemyData> enemies;

    // returns a list of Enemys created from the list of EnemyData
    public List<Enemy> GetEnemies()
    {
        List<Enemy> enemyList = new List<Enemy>();
        foreach (EnemyData enemyData in enemies)
        {
            enemyList.Add(new Enemy(enemyData));
        }
        return enemyList;
    }

    // returns the number of enemies in the encounter
    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}
