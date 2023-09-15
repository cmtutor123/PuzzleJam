using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatManager))]
public class MapManager : MonoBehaviour
{
    private CombatManager combatManager;

    void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }

    void Update()
    {
        
    }
}
