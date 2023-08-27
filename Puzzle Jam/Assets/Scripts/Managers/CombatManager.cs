using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public const int defaultBoardSize = 6;

    public PuzzlePile drawPile, discardPile, handPile;
    public PuzzleBoard puzzleBoard;

    private void Start()
    {
        drawPile = new PuzzlePile();
        discardPile = new PuzzlePile();
        handPile = new PuzzlePile();
        puzzleBoard = new PuzzleBoard(defaultBoardSize);
    }
}
