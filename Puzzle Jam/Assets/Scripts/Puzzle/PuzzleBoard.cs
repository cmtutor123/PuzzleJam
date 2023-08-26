using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard
{
    private PuzzlePiece[,] board;
    private int width, height;

    public PuzzleBoard(int size)
    {
        width = size;
        height = size;
        GenerateEmptyBoard();
    }

    public PuzzleBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        GenerateEmptyBoard();
    }

    public void GenerateEmptyBoard()
    {
        board = new PuzzlePiece[width, height];
    }

    public void PlacePiece(PuzzlePiece piece, int x, int y)
    {
        board[x, y] = piece;
    }

    public PuzzlePiece RemovePiece(int x, int y)
    {
        PuzzlePiece piece = board[x, y];
        board[x, y] = null;
        return piece;
    }
}
