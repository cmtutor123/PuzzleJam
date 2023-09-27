using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathingManager : MonoBehaviour
{
    private PuzzlePiece currentPuzzlePiece;
    private List<PuzzlePiece> nextPuzzlePieces;
    private CombatManager combatManager;

    void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }

    void Update()
    {
        
    }

    void CreatePlayerPaths()
    {

    }

    void DisplayPlayerPaths()
    {

    }
}
