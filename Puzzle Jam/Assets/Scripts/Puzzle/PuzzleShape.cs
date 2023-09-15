    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Holds data on the shape of a PuzzlePiece
/// </summary>
[Serializable]
public class PuzzleShape
{
    [SerializeField] private PuzzleEdge topEdge;
    [SerializeField] private PuzzleEdge leftEdge;
    [SerializeField] private PuzzleEdge rightEdge;
    [SerializeField] private PuzzleEdge bottomEdge;

    /// <returns>The top PuzzleEdge</returns>
    public PuzzleEdge GetTop()
    {
        return topEdge;
    }

    /// <returns>The left PuzzleEdge</returns>
    public PuzzleEdge GetLeft()
    {
        return leftEdge;
    }

    /// <returns>The right PuzzleEdge</returns>
    public PuzzleEdge GetRight()
    {
        return rightEdge;
    }

    /// <returns>The bottom PuzzleEdge</returns>
    public PuzzleEdge GetBottom()
    {
        return bottomEdge;
    }
}
