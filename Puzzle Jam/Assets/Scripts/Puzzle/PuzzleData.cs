using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Data", menuName = "Puzzle/Data")]
public class PuzzleData : ScriptableObject
{
    [Header("Puzzle Shape")]
    [SerializeField] private PuzzleShape puzzleShape;
    [Header("Puzzle Color")]
    [SerializeField] private PuzzleColor puzzleColor;
    [Header("Puzzle Image")]
    [SerializeField] private Sprite puzzleImage;
    [Header("Puzzle Information")]
    [SerializeField] private string puzzleName;
    [SerializeField] private string puzzleDescription;

    // returns the PuzzleShape
    public PuzzleShape GetPuzzleShape()
    {
        return puzzleShape;
    }

    // returns the PuzzleColor
    public PuzzleColor GetPuzzleColor()
    {
        return puzzleColor;
    }

    // returns the Color of the PuzzleColor
    public Color GetColor()
    {
        return puzzleColor.GetColor();
    }

    // returns the Sprite displayed in the center of the PuzzlePiece
    public Sprite GetImage()
    {
        return puzzleImage;
    }

    // returns the name of the PuzzlePiece
    public string GetName()
    {
        return puzzleName;
    }

    // returns the description of the PuzzlePiece
    public string GetDescription()
    {
        return puzzleDescription;
    }
}
