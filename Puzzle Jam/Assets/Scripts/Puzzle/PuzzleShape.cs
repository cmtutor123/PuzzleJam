    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Shape", menuName = "Puzzle/Shape")]
public class PuzzleShape : ScriptableObject
{
    [Header("Puzzle Piece Shape")]
    [SerializeField] private PuzzleEdge topEdge;
    [SerializeField] private PuzzleEdge leftEdge;
    [SerializeField] private PuzzleEdge rightEdge;
    [SerializeField] private PuzzleEdge bottomEdge;

    // returns the top edge
    public PuzzleEdge GetTop()
    {
        return topEdge;
    }

    // returns the left edge
    public PuzzleEdge GetLeft()
    {
        return leftEdge;
    }

    // returns the right edge
    public PuzzleEdge GetRight()
    {
        return rightEdge;
    }

    // returns the bottom edge
    public PuzzleEdge GetBottom()
    {
        return bottomEdge;
    }
}
