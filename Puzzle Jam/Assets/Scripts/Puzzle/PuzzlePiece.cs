using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece
{
    private PuzzleEdge topEdge, leftEdge, rightEdge, bottomEdge;
    private PuzzleColor puzzleColor;

    private Sprite puzzleImage;
    private string puzzleDescription;
    private string puzzleName;

    // creates a PuzzlePiece from the given PuzzleData
    public PuzzlePiece(PuzzleData puzzleData)
    {
        PuzzleShape puzzleShape = puzzleData.GetPuzzleShape();
        topEdge = puzzleShape.GetTop();
        leftEdge = puzzleShape.GetLeft();
        rightEdge = puzzleShape.GetRight();
        bottomEdge = puzzleShape.GetBottom();
        puzzleColor = puzzleData.GetPuzzleColor();
        puzzleImage = puzzleData.GetImage();
        puzzleDescription = puzzleData.GetDescription();
        puzzleName = puzzleData.GetName();
    }

    // rotates the edges of the PuzzlePiece 90 degrees to the right
    public void RotatePiece()
    {
        PuzzleEdge tempEdge = topEdge;
        topEdge = rightEdge;
        rightEdge = bottomEdge;
        bottomEdge = leftEdge;
        leftEdge = tempEdge;
    }

    // returns the top edge
    public PuzzleEdge GetTop()
    {
        return topEdge;
    }

    // returns the left edge
    public PuzzleEdge GetLeft()
    {
        return leftEdge;
    }

    // returns the right edge
    public PuzzleEdge GetRight()
    {
        return rightEdge;
    }

    // returns the bottom edge
    public PuzzleEdge GetBottom()
    {
        return bottomEdge;
    }

    // returns the PuzzleColor
    public PuzzleColor GetPuzzleColor()
    {
        return puzzleColor;
    }

    // returns the Color of the PuzzleColor
    public Color GetColor()
    {
        return puzzleColor.GetColor();
    }

    // returns the Sprite displayed in the center of the PuzzlePiece
    public Sprite GetImage()
    {
        return puzzleImage;
    }

    // returns the name of the PuzzlePiece
    public string GetName()
    {
        return puzzleName;
    }

    // returns the description of the PuzzlePiece
    public string GetDescription()
    {
        return puzzleDescription;
    }
}
