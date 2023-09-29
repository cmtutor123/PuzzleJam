using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a group of PuzzlePiece objects
/// </summary>
public class PuzzlePile
{
    private bool hasMaxSize;
    private int maxSize;
    private List<PuzzlePiece> puzzlePieces;

    /// <summary>
    /// Creates a PuzzlePile with no max size
    /// </summary>
    public PuzzlePile()
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = false;
    }

    /// <summary>
    /// Create a PuzzlePiile with a max size
    /// </summary>
    /// <param name="maxSize">The max size of the PuzzlePile</param>
    public PuzzlePile(int maxSize)
    {
        puzzlePieces = new List<PuzzlePiece>();
        hasMaxSize = true;
        this.maxSize = maxSize;
    }

    /// <summary>
    /// Adds a PuzzlePiece to the bottom of the PuzzlePile
    /// </summary>
    /// <param name="puzzlePiece">The PuzzlePiece to add to the PuzzlePile</param>
    public void AddPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        if (puzzlePiece != null) puzzlePieces.Add(puzzlePiece);
    }

    /// <summary>
    /// Adds multiple PuzzlePiece objects to the bottom of the PuzzlePile
    /// </summary>
    /// <param name="newPuzzlePieces">A list of the PuzzlePiece objects to be added</param>
    public void AddPuzzlePieces(List<PuzzlePiece> newPuzzlePieces)
    {
        if (newPuzzlePieces != null) puzzlePieces.AddRange(newPuzzlePieces);
    }

    /// <summary>
    /// Checks whether the PuzzlePile has reached its maximum capacity
    /// </summary>
    /// <returns>Whether the PuzzlePile is at its max size</returns>
    public bool AtMaxSize()
    {
        return hasMaxSize && puzzlePieces.Count >= maxSize;
    }

    /// <summary>
    /// Removes all PuzzlePieces from the PuzzlePile
    /// </summary>
    /// <returns>A list of the PuzzlePiece objects that were removed</returns>
    public List<PuzzlePiece> DiscardPile()
    {
        List<PuzzlePiece> discardedPieces = new List<PuzzlePiece>();
        discardedPieces.AddRange(puzzlePieces);
        EmptyPile();
        return discardedPieces;
    }

    /// <summary>
    /// Draws the PuzzlePiece at the top of the PuzzlePile
    /// </summary>
    /// <returns>The PuzzlePiece being drawn</returns>
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

    /// <summary>
    /// Draws a PuzzlePiece from the PuzzlePile
    /// </summary>
    /// <param name="index">The index of the PuzzlePiece being drawn</param>
    /// <returns>The PuzzlePiece being drawn</returns>
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

    /// <summary>
    /// Removes all PuzzlePieces from the PuzzlePile
    /// </summary>
    public void EmptyPile()
    {
        puzzlePieces.Clear();
    }

    /// <param name="index">The index of the PuzzlePiece</param>
    /// <returns>The PuzzlePiece at the specified index</returns>
    public PuzzlePiece GetPuzzlePiece(int index)
    {
        return puzzlePieces[index];
    }

    /// <returns>A list of all PuzzlePiece objects in the PuzzlePile</returns>
    public List<PuzzlePiece> GetPuzzlePieces()
    {
        return puzzlePieces;
    }



    /// <returns>The number of PuzzlePieces in the PuzzlePile</returns>
    public int GetSize()
    {
        if (puzzlePieces != null) return puzzlePieces.Count;
        else return 0;
    }

    /// <summary>
    /// Randomizes the order of PuzzlePiece objects in the PuzzlePile
    /// </summary>
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
