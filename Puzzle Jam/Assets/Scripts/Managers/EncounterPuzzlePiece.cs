using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPuzzlePiece
{
    private EncounterType encounterType;
    private PuzzleEdge left, right;
    private PuzzleColor puzzleColor;

    public EncounterPuzzlePiece(EncounterType encounterType, PuzzleColor puzzleColor)
    {
        this.encounterType = encounterType;
        this.puzzleColor = puzzleColor;
    }

    public EncounterType GetEncounterType()
    {
        return encounterType;
    }

    public PuzzleEdge GetTop()
    {
        return PuzzleEdge.Blank;
    }

    public PuzzleEdge GetBottom()
    {
        return PuzzleEdge.Blank;
    }

    public PuzzleEdge GetLeft()
    {
        return left;
    }

    public PuzzleEdge GetRight()
    {
        return right;
    }

    public void SetEdges(PuzzleEdge left, PuzzleEdge right)
    {
        this.left = left;
        this.right = right;
    }

    public PuzzleColor GetPuzzleColor()
    {
        return puzzleColor;
    }
}
