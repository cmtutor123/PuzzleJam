    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Shape", menuName = "Puzzle/Shape")]
public class PuzzleShape : ScriptableObject
{
    public PuzzleEdge top, left, right, bottom;
}
