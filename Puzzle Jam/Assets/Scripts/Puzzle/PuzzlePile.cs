using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePile
{
    public List<PuzzlePiece> puzzlePieces;
    public bool hasMaxSize;
    public int maxSize;

    public PuzzlePile()
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = false;
        maxSize = 0;
    }

    public PuzzlePile(int maxSize)
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = true;
        this.maxSize = maxSize;
    }

    public void AddPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        if (puzzlePiece != null) puzzlePieces.Add(puzzlePiece);
    }

    public void AddPuzzlePieces(List<PuzzlePiece> newPuzzlePieces)
    {
        if (newPuzzlePieces != null) puzzlePieces.AddRange(newPuzzlePieces);
    }

    public bool AtMaxSize()
    {
        return hasMaxSize && puzzlePieces.Count == maxSize;
    }

    public List<PuzzlePiece> DiscardPile()
    {
        return puzzlePieces;
    }

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

    public void EmptyPile()
    {
        puzzlePieces.Clear();
    }

    public PuzzlePiece GetPuzzlePiece(int index)
    {
        return puzzlePieces[index];
    }

    public List<PuzzlePiece> GetPuzzlePieces()
    {
        return puzzlePieces;
    }

    public int GetSize()
    {
        return puzzlePieces.Count;
    }

    public void ShufflePile()
    {
        List<PuzzlePiece> newList = new List<PuzzlePiece>();
        while (puzzlePieces.Count > 0)
        {
            newList.Insert(Random.Range(0, newList.Count), DrawPuzzlePiece());
        }
        puzzlePieces.AddRange(newList);
    }

    public int Size()
    {
        if (puzzlePieces != null) return puzzlePieces.Count;
        else return 0;
    }
}
