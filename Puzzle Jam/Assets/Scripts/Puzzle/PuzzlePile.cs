using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePile
{
    public List<PuzzlePiece> puzzlePieces;

    public PuzzlePile()
    {
        puzzlePieces = new List<PuzzlePiece>();
    }

    public void AddPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        if (puzzlePiece != null) puzzlePieces.Add(puzzlePiece);
    }

    public void AddPuzzlePieces(List<PuzzlePiece> newPuzzlePieces)
    {
        if (newPuzzlePieces != null) puzzlePieces.AddRange(newPuzzlePieces);
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

    public List<PuzzlePiece> GetPuzzlePieces()
    {
        return puzzlePieces;
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
}
