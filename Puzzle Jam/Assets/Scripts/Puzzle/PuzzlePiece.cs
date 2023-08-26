using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece
{
    public PuzzleEdge top, left, right, bottom;
    public Color color;

    public PuzzlePiece(PuzzleData data)
    {
        top = data.puzzleShape.top;
        left = data.puzzleShape.left;
        right = data.puzzleShape.right;
        bottom = data.puzzleShape.bottom;
        color = data.puzzleColor.color;
    }
}
