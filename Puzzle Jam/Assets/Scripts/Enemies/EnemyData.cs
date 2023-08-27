using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public Sprite spriteIdle;
    public EnemyAttackPattern attackPattern;
}
