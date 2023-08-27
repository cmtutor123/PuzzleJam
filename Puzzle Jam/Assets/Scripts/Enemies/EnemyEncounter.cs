using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Encounter", menuName = "Enemy/Encounter")]
public class EnemyEncounter : ScriptableObject
{
    public List<Enemy> enemies;
}
