using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character/Data")]
public class CharacterData : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite spritePuzzleBoard;
    [Header("Starting Puzzle Pieces")]
    [SerializeField] private List<PuzzleData> startingPuzzlePieces;

    // returns the puzzle board Sprite
    public Sprite GetSpritePuzzleBoard()
    {
        return spritePuzzleBoard;
    }

    // returns a list of PuzzlePieces from PuzzleData
    public List<PuzzlePiece> GetStartingPuzzlePieces()
    {
        List<PuzzlePiece> startingPieces = new List<PuzzlePiece>();
        foreach (PuzzleData puzzleData in startingPuzzlePieces)
        {
            startingPieces.Add(new PuzzlePiece(puzzleData));
        }
        return startingPieces;
    }
}
