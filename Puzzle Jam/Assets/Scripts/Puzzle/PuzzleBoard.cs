using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard
{
    private const PuzzleEdge boardEdge = PuzzleEdge.Blank;

    private int width, height;
    private PuzzlePiece[,] board;

    // creates a PuzzleBoard with an equal width and height
    public PuzzleBoard(int size)
    {
        width = size;
        height = size;
        GenerateEmptyBoard();
    }

    // creates a PuzzleBoard with a different width and height
    public PuzzleBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        GenerateEmptyBoard();
    }

    // creates an empty board
    public void GenerateEmptyBoard()
    {
        board = new PuzzlePiece[width, height];
    }

    // places a PuzzlePiece at the specified location
    public void PlacePiece(PuzzlePiece piece, int x, int y)
    {
        board[x, y] = piece;
    }

    // removes and returns the PuzzlePiece at the specified location
    public PuzzlePiece RemovePiece(int x, int y)
    {
        PuzzlePiece piece = board[x, y];
        board[x, y] = null;
        return piece;
    }

    // removes all of the PuzzlePieces on the PuzzleBoard
    public void ClearBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                board[x, y] = null;
            }
        }
    }

    // returns true is there is an empty space at the specified location
    public bool LocationEmpty(int x, int y)
    {
        return PositionOnBoard(x, y) && board[x, y] == null;
    }

    // returns the PuzzlePiece at the specified location
    public PuzzlePiece GetPuzzlePiece(int x, int y)
    {
        return board[x, y];
    }

    // returns the PuzzlePiece at the specified index
    public PuzzlePiece GetPuzzlePieceFromIndex(int index)
    {
        if (index < width * height)
        {
            int x = index % width;
            int y = (index - x) / width;
            return board[x, y];
        }
        else return null;
    }

    // returns true if a PuzzlePiece with the specified edges can fit in the specified location
    public bool PieceCanFit(int x, int y, PuzzleEdge top, PuzzleEdge left, PuzzleEdge right, PuzzleEdge bottom)
    {
        return LocationEmpty(x, y) && EdgesCanConnect(top, GetTopEdge(x, y)) && EdgesCanConnect(left, GetLeftEdge(x, y)) && EdgesCanConnect(right, GetRightEdge(x, y)) && EdgesCanConnect(bottom, GetBottomEdge(x, y));
    }

    // returns true if the specified position is within the bounds of the board
    public bool PositionOnBoard(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    // returns the bottom edge of the PuzzlePiece above the specified position
    public PuzzleEdge GetTopEdge(int x, int y)
    {
        int xPos = x;
        int yPos = y + 1;
        if (PositionOnBoard(xPos, yPos)) return GetPuzzlePiece(xPos, yPos).GetBottom();
        else return boardEdge;
    }

    // returns the right edge of the PuzzlePiece to the left of the specified position
    public PuzzleEdge GetLeftEdge(int x, int y)
    {
        int xPos = x - 1;
        int yPos = y;
        if (PositionOnBoard(xPos, yPos)) return GetPuzzlePiece(xPos, yPos).GetRight();
        else return boardEdge;
    }

    // returns the left edge of the PuzzlePiece to the right of the specified position
    public PuzzleEdge GetRightEdge(int x, int y)
    {
        int xPos = x + 1;
        int yPos = y;
        if (PositionOnBoard(xPos, yPos)) return GetPuzzlePiece(xPos, yPos).GetLeft();
        else return boardEdge;
    }

    // returns the top edge of the PuzzlePiece below the specified position
    public PuzzleEdge GetBottomEdge(int x, int y)
    {
        int xPos = x;
        int yPos = y - 1;
        if (PositionOnBoard(xPos, yPos)) return GetPuzzlePiece(xPos, yPos).GetTop();
        else return boardEdge;
    }

    // returns true if the two edges can be placed next to each other
    public bool EdgesCanConnect(PuzzleEdge edge1, PuzzleEdge edge2)
    {
        if (edge1 == PuzzleEdge.Socket || edge2 == PuzzleEdge.Socket) return true;
        else if (edge1 == PuzzleEdge.Key || edge2 == PuzzleEdge.Key) return false;
        else return true;
    }

    // returns the size of the PuzzleBoard
    public int GetSize()
    {
        return width * height;
    }
}
