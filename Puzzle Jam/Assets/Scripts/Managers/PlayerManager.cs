using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PuzzlePile puzzleDeck;

    private void Start()
    {
        puzzleDeck = new PuzzlePile();
    }

    public List<PuzzlePiece> GetPuzzleDeck()
    {
        return puzzleDeck.GetPuzzlePieces();
    }
}
