using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data on a type of PuzzlePiece
/// </summary>
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
    [Header("Puzzle Effects")]
    [SerializeField] private List<PuzzleEffect> puzzleEffects;

    /// <returns>The PuzzleShape of the PuzzlePiece</returns>
    public PuzzleShape GetPuzzleShape()
    {
        return puzzleShape;
    }

    /// <returns>The PuzzleColor of the PuzzlePiece</returns>
    public PuzzleColor GetPuzzleColor()
    {
        return puzzleColor;
    }

    /// <returns>The Color of the PuzzlePiece object's PuzzleColor</returns>
    public Color GetColor()
    {
        return puzzleColor.GetColor();
    }

    /// <returns>The sprite displayed on the PuzzlePiece</returns>
    public Sprite GetImage()
    {
        return puzzleImage;
    }

    /// <returns>The name of the PuzzlePiece</returns>
    public string GetName()
    {
        return puzzleName;
    }

    /// <returns>The description of the PuzzlePiece</returns>
    public string GetDescription()
    {
        return puzzleDescription;
    }

    /// <returns>A list of PuzzleEffects</returns>
    public List<PuzzleEffect> GetEffects()
    {
        return puzzleEffects;
    }
}
