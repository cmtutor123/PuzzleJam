using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] private CharacterData characterData;
    private PuzzlePile puzzleDeck;

    private void Start()
    {
        puzzleDeck = new PuzzlePile();
        puzzleDeck.AddPuzzlePieces(characterData.GetStartingPuzzlePieces());
    }

    // returns the PuzzlePieces in the deck PuzzlePile
    public List<PuzzlePiece> GetPuzzleDeck()
    {
        return puzzleDeck.GetPuzzlePieces();
    }

    // returns the puzzle board Sprite from CharacterData
    public Sprite GetPuzzleBoardSprite()
    {
        return characterData.GetSpritePuzzleBoard();
    }
}
