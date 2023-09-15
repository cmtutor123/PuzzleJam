using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the data relevant to each character
/// </summary>
[CreateAssetMenu(fileName = "New Character Data", menuName = "Character/Data")]
public class CharacterData : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite spritePuzzleBoard;
    [Header("Starting Puzzle Pieces")]
    [SerializeField] private List<PuzzleData> startingPuzzlePieces;
    [Header("Health")]
    [SerializeField] private int health;

    /// <returns>The Sprite for the puzzle board</returns>
    public Sprite GetSpritePuzzleBoard()
    {
        return spritePuzzleBoard;
    }

    /// <returns>A list of PuzzlePiece objects from PuzzleData</returns>
    public List<PuzzlePiece> GetStartingPuzzlePieces()
    {
        List<PuzzlePiece> startingPieces = new List<PuzzlePiece>();
        foreach (PuzzleData puzzleData in startingPuzzlePieces)
        {
            startingPieces.Add(new PuzzlePiece(puzzleData));
        }
        return startingPieces;
    }

    /// <returns>The character's max health</returns>
    public int GetHealth()
    {
        return health;
    }
}
