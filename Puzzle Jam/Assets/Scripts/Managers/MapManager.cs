using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatManager))]
public class MapManager : MonoBehaviour
{
    private CombatManager combatManager;

    [SerializeField] private List<List<PuzzlePiece>> possibilities;

    void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }

    public void GeneratePossiblePaths()
    {

    }
}
