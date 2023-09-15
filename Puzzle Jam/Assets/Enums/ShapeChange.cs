using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The types of changes that can be applied to the shape of a PuzzlePiece
/// </summary>
public enum ShapeChange
{
    None,
    SmoothEdges,
    ConnectSurrounding
}
