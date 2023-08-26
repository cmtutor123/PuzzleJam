using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Data", menuName = "Puzzle/Data")]
public class PuzzleData : ScriptableObject
{
    public PuzzleShape puzzleShape;
    public PuzzleColor puzzleColor;
}
