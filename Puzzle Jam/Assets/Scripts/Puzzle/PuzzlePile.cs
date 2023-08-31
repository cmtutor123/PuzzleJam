using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePile
{
    private bool hasMaxSize;
    private int maxSize;
    private List<PuzzlePiece> puzzlePieces;

    // creates a PuzzlePile with no max size
    public PuzzlePile()
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = false;
    }

    // creates a PuzzlePile with the specified max size
    public PuzzlePile(int maxSize)
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = true;
        this.maxSize = maxSize;
    }

    // adds a PuzzlePiece to the bottom of the PuzzlePile
    public void AddPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        if (puzzlePiece != null) puzzlePieces.Add(puzzlePiece);
    }

    // adds multiple PuzzlePieces to the bottom of the PuzzlePile
    public void AddPuzzlePieces(List<PuzzlePiece> newPuzzlePieces)
    {
        if (newPuzzlePieces != null) puzzlePieces.AddRange(newPuzzlePieces);
    }

    // checks whether the PuzzlePile has reached or gone over its maximum capacity
    public bool AtMaxSize()
    {
        return hasMaxSize && puzzlePieces.Count >= maxSize;
    }

    // removes all PuzzlePieces from the PuzzlePile and returns them
    public List<PuzzlePiece> DiscardPile()
    {
        List<PuzzlePiece> discardedPieces = new List<PuzzlePiece>();
        discardedPieces.AddRange(puzzlePieces);
        EmptyPile();
        return discardedPieces;
    }

    // draws the PuzzlePiece at the top of the PuzzlePile
    public PuzzlePiece DrawPuzzlePiece()
    {
        if (puzzlePieces.Count == 0) return null;
        else
        {
            PuzzlePiece puzzlePiece = puzzlePieces[0];
            puzzlePieces.RemoveAt(0);
            return puzzlePiece;
        }
    }

    // draws the PuzzlePiece at the specified index of the PuzzlePile
    public PuzzlePiece DrawPuzzlePiece(int index)
    {
        if (puzzlePieces.Count == 0) return null;
        else if (index < 0 || index >= puzzlePieces.Count) return DrawPuzzlePiece();
        else
        {
            PuzzlePiece puzzlePiece = puzzlePieces[index];
            puzzlePieces.RemoveAt(index);
            return puzzlePiece;
        }
    }

    // removes all PuzzlePieces from the PuzzlePile
    public void EmptyPile()
    {
        puzzlePieces.Clear();
    }

    // returns the PuzzlePiece at the specified index
    public PuzzlePiece GetPuzzlePiece(int index)
    {
        return puzzlePieces[index];
    }

    // returns all PuzzlePieces in the PuzzlePile
    public List<PuzzlePiece> GetPuzzlePieces()
    {
        return puzzlePieces;
    }

    // returns the number of PuzzlePieces in the PuzzlePile
    public int GetSize()
    {
        if (puzzlePieces != null) return puzzlePieces.Count;
        else return 0;
    }

    // randomizes the order of PuzzlePieces in the PuzzlePile
    public void ShufflePile()
    {
        List<PuzzlePiece> newList = new List<PuzzlePiece>();
        while (puzzlePieces.Count > 0)
        {
            newList.Insert(Random.Range(0, newList.Count), DrawPuzzlePiece());
        }
        puzzlePieces.AddRange(newList);
    }
}
