using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds and manages all of the PuzzlePiece objects that are currently in play
/// </summary>
public class PuzzleBoard
{
    private const PuzzleEdge boardEdge = PuzzleEdge.Blank;

    private int width, height;
    private PuzzlePiece[,] board;

    /// <param name="size">The width and height of the PuzzleBoard</param>
    public PuzzleBoard(int size)
    {
        width = size;
        height = size;
        GenerateEmptyBoard();
    }

    /// <param name="width">The width of the PuzzleBoard</param>
    /// <param name="height">The height of the PuzzleBoard</param>
    public PuzzleBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        GenerateEmptyBoard();
    }

    /// <summary>
    /// Initializes an empty board
    /// </summary>
    public void GenerateEmptyBoard()
    {
        board = new PuzzlePiece[width, height];
    }

    /// <summary>
    /// Places a PuzzlePiece
    /// </summary>
    /// <param name="piece">The puzzle piece to be placed</param>
    /// <param name="x">The horizontal position</param>
    /// <param name="y">The vertical position</param>
    public void PlacePiece(PuzzlePiece piece, int x, int y)
    {
        board[x, y] = piece;
    }

    /// <summary>
    /// Places a PuzzlePiece
    /// </summary>
    /// <param name="piece">The puzzle piece to be placed</param>
    /// <param name="index">The index of the po</param>
    public void PlacePiece(PuzzlePiece piece, int index)
    {
        int x = index % width;
        int y = (index - x) / width;
        PlacePiece(piece, x, y);
    }

    /// <summary>
    /// Removes a PuzzlePiece object from the PuzzleBoard
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The PuzzlePiece that was removed</returns>
    public PuzzlePiece RemovePiece(int x, int y)
    {
        PuzzlePiece piece = board[x, y];
        board[x, y] = null;
        return piece;
    }

    /// <summary>
    /// Removes a PuzzlePiece object from the PuzzleBoard
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>The PuzzlePiece that was removed</returns>
    public PuzzlePiece RemovePiece(int index)
    {
        int x = index % width;
        int y = (index - x) / width;
        return RemovePiece(x, y);
    }

    /// <summary>
    /// Removes all PuzzlePiece objects on the PuzzleBoard
    /// </summary>
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

    /// <summary>
    /// Determines whether a location has a PuzzlePiece object
    /// </summary>
    /// <param name="x">The horizontal position</param>
    /// <param name="y">The vertical position</param>
    /// <returns>Whether a location is empty</returns>
    public bool LocationEmpty(int x, int y)
    {
        return PositionOnBoard(x, y) && board[x, y] == null;
    }

    /// <summary>
    /// Determines whether a location has a PuzzlePiece object
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>Whether a location is empty</returns>
    public bool LocationEmpty(int index)
    {
        int x = index % width;
        int y = (index - x) / width;
        return PositionOnBoard(x, y) && board[x, y] == null;
    }

    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The PuzzlePiece at the location</returns>
    public PuzzlePiece GetPuzzlePiece(int x, int y)
    {
        return board[x, y];
    }

    /// <param name="index">The index of the location</param>
    /// <returns>The PuzzlePiece at the location</returns>
    public PuzzlePiece GetPuzzlePiece(int index)
    {
        if (index < width * height)
        {
            int x = index % width;
            int y = (index - x) / width;
            return board[x, y];
        }
        else return null;
    }

    /// <summary>
    /// Determines if a PuzzlePiece at fit at a specified location on the PuzzleBoard
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <param name="top">The top PuzzleEdge of the PuzzlePiece</param>
    /// <param name="left">The left PuzzleEdge of the PuzzlePiece</param>
    /// <param name="right">The right PuzzleEdge of the PuzzlePiece</param>
    /// <param name="bottom">The bottom PuzzleEdge of the PuzzlePiece</param>
    /// <returns>Whether the PuzzlePiece can fit at the location</returns>
    public bool PieceCanFit(int x, int y, PuzzleEdge top, PuzzleEdge left, PuzzleEdge right, PuzzleEdge bottom)
    {
        return LocationEmpty(x, y) && EdgesCanConnect(top, GetTopEdge(x, y)) && EdgesCanConnect(left, GetLeftEdge(x, y)) && EdgesCanConnect(right, GetRightEdge(x, y)) && EdgesCanConnect(bottom, GetBottomEdge(x, y));
    }

    /// <summary>
    /// Determines if a PuzzlePiece at fit at a specified location on the PuzzleBoard
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <param name="top">The top PuzzleEdge of the PuzzlePiece</param>
    /// <param name="left">The left PuzzleEdge of the PuzzlePiece</param>
    /// <param name="right">The right PuzzleEdge of the PuzzlePiece</param>
    /// <param name="bottom">The bottom PuzzleEdge of the PuzzlePiece</param>
    /// <returns>Whether the PuzzlePiece can fit at the location</returns>
    public bool PieceCanFit(int index, PuzzleEdge top, PuzzleEdge left, PuzzleEdge right, PuzzleEdge bottom)
    {
        int x = index % width;
        int y = (index - x) / width;
        return LocationEmpty(x, y) && EdgesCanConnect(top, GetTopEdge(x, y)) && EdgesCanConnect(left, GetLeftEdge(x, y)) && EdgesCanConnect(right, GetRightEdge(x, y)) && EdgesCanConnect(bottom, GetBottomEdge(x, y));
    }

    /// <summary>
    /// Determines if a PuzzlePiece at fit at a specified location on the PuzzleBoard
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <param name="piece">The PuzzlePiece object</param>
    /// <returns>Whether the PuzzlePiece can fit at the location</returns>
    public bool PieceCanFit(int x, int y, PuzzlePiece piece)
    {
        PuzzleEdge top = piece.GetTop();
        PuzzleEdge left = piece.GetLeft();
        PuzzleEdge right = piece.GetRight();
        PuzzleEdge bottom = piece.GetBottom();
        return LocationEmpty(x, y) && EdgesCanConnect(top, GetTopEdge(x, y)) && EdgesCanConnect(left, GetLeftEdge(x, y)) && EdgesCanConnect(right, GetRightEdge(x, y)) && EdgesCanConnect(bottom, GetBottomEdge(x, y));
    }

    /// <summary>
    /// Determines if a PuzzlePiece at fit at a specified location on the PuzzleBoard
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <param name="piece">The PuzzlePiece object</param>
    /// <returns>Whether the PuzzlePiece can fit at the location</returns>
    public bool PieceCanFit(int index, PuzzlePiece piece)
    {
        int x = index % width;
        int y = (index - x) / width;
        PuzzleEdge top = piece.GetTop();
        PuzzleEdge left = piece.GetLeft();
        PuzzleEdge right = piece.GetRight();
        PuzzleEdge bottom = piece.GetBottom();
        return LocationEmpty(x, y) && EdgesCanConnect(top, GetTopEdge(x, y)) && EdgesCanConnect(left, GetLeftEdge(x, y)) && EdgesCanConnect(right, GetRightEdge(x, y)) && EdgesCanConnect(bottom, GetBottomEdge(x, y));
    }

    /// <summary>
    /// Determines if a location is within the PuzzleBoard's boundries
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>Whether the location is on the PuzzleBoard</returns>
    public bool PositionOnBoard(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    /// <summary>
    /// Determines if a location is within the PuzzleBoard's boundries
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>Whether the location is on the PuzzleBoard</returns>
    public bool PositionOnBoard(int index)
    {

        int x = index % width;
        int y = (index - x) / width;
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    /// <summary>
    /// Gets the PuzzleEdge touching the top PuzzleEdge of the PuzzlePiece object at a location
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The touching PuzzleEdge</returns>
    public PuzzleEdge GetTopEdge(int x, int y)
    {
        int xPos = x;
        int yPos = y + 1;
        if (!PositionOnBoard(xPos, yPos)) return boardEdge;
        else if (LocationEmpty(xPos, yPos)) return PuzzleEdge.Socket;
        else return GetPuzzlePiece(xPos, yPos).GetBottom();
    }

    /// <summary>
    /// Gets the PuzzleEdge touching the left PuzzleEdge of the PuzzlePiece object at a location
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The touching PuzzleEdge</returns>
    public PuzzleEdge GetLeftEdge(int x, int y)
    {
        int xPos = x - 1;
        int yPos = y;
        if (!PositionOnBoard(xPos, yPos)) return boardEdge;
        else if (LocationEmpty(xPos, yPos)) return PuzzleEdge.Socket;
        else return GetPuzzlePiece(xPos, yPos).GetRight();
    }

    /// <summary>
    /// Gets the PuzzleEdge touching the right PuzzleEdge of the PuzzlePiece object at a location
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The touching PuzzleEdge</returns>
    public PuzzleEdge GetRightEdge(int x, int y)
    {
        int xPos = x + 1;
        int yPos = y;
        if (!PositionOnBoard(xPos, yPos)) return boardEdge;
        else if (LocationEmpty(xPos, yPos)) return PuzzleEdge.Socket;
        else return GetPuzzlePiece(xPos, yPos).GetLeft();
    }

    /// <summary>
    /// Gets the PuzzleEdge touching the bottom PuzzleEdge of the PuzzlePiece object at a location
    /// </summary>
    /// <param name="x">The horizontal location</param>
    /// <param name="y">The vertical location</param>
    /// <returns>The touching PuzzleEdge</returns>
    public PuzzleEdge GetBottomEdge(int x, int y)
    {
        int xPos = x;
        int yPos = y - 1;
        if (!PositionOnBoard(xPos, yPos)) return boardEdge;
        else if (LocationEmpty(xPos, yPos)) return PuzzleEdge.Socket;
        else return GetPuzzlePiece(xPos, yPos).GetTop();
    }

    /// <summary>
    /// Determines if two PuzzleEdges can be placed next to each other
    /// </summary>
    /// <param name="edge1">First PuzzleEdge</param>
    /// <param name="edge2">Second PuzzleEdge</param>
    /// <returns>Whether the two PuzzleEdges can be placed next to each other</returns>
    public bool EdgesCanConnect(PuzzleEdge edge1, PuzzleEdge edge2)
    {
        if (edge1 == PuzzleEdge.Socket || edge2 == PuzzleEdge.Socket) return true;
        else if (edge1 == PuzzleEdge.Key || edge2 == PuzzleEdge.Key) return false;
        else return true;
    }

    /// <returns>The size of the PuzzleBoard</returns>
    public int GetSize()
    {
        return width * height;
    }

    /// <summary>
    /// Gets all PuzzlePieces that are adjacent to the PuzzlePiece at a location
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>A list of PuzzlePiece objects that are adjacent to the PuzzlePiece</returns>
    public List<PuzzlePiece> GetAdjacent(int index)
    {
        return null;
    }

    /// <summary>
    /// Gets all PuzzlePieces that are connected to the PuzzlePiece at a location
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>A list of PuzzlePiece objects that are connected to the PuzzlePiece</returns>
    public List<PuzzlePiece> GetConnected(int index)
    {
        return null;
    }

    /// <summary>
    /// Gets all PuzzlePieces that are chained to the PuzzlePiece at a location
    /// </summary>
    /// <param name="index">The index of the location</param>
    /// <returns>A list of PuzzlePiece objects that are chained to the PuzzlePiece</returns>
    public List<PuzzlePiece> GetChain(int index)
    {
        return null;
    }

    /// <param name="index">The index of the location of the PuzzlePiece</param>
    /// <returns>Whether the PuzzlePiece is in a combo</returns>
    public bool InCombo(int index)
    {
        return false;
    }
}
