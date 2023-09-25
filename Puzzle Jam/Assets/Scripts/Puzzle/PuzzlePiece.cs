using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What the player sees and manipulates during combat
/// </summary>
public class PuzzlePiece
{
    private PuzzleEdge topEdge, leftEdge, rightEdge, bottomEdge;
    private PuzzleColor puzzleColor;

    private List<PuzzleEffect> puzzleEffects;

    private Sprite puzzleImage;
    private string puzzleDescription;
    private string puzzleName;

    /// <param name="puzzleData">The PuzzleData to create the PuzzlePiece from</param>
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
        puzzleEffects = puzzleData.GetEffects();
    }

    /// <summary>
    /// Rotates the edges of the PuzzlePiece 90 degrees to the right
    /// </summary>
    public void RotatePiece()
    {
        PuzzleEdge tempEdge = topEdge;
        topEdge = rightEdge;
        rightEdge = bottomEdge;
        bottomEdge = leftEdge;
        leftEdge = tempEdge;
    }

    /// <returns>The top PuzzleEdge</returns>
    public PuzzleEdge GetTop()
    {
        return topEdge;
    }

    /// <returns>The left PuzzleEdge</returns>
    public PuzzleEdge GetLeft()
    {
        return leftEdge;
    }

    /// <returns>The right PuzzleEdge</returns>
    public PuzzleEdge GetRight()
    {
        return rightEdge;
    }

    /// <returns>The bottom PuzzleEdge</returns>
    public PuzzleEdge GetBottom()
    {
        return bottomEdge;
    }
    
    /// <returns>The PuzzleColor of the PuzzlePiece</returns>
    public PuzzleColor GetPuzzleColor()
    {
        return puzzleColor;
    }

    /// <returns>The Color of the PUzzlePiece's PuzzleColor</returns>
    public Color GetColor()
    {
        return puzzleColor.GetColor();
    }

    /// <returns>The sprite displayed in the center of the PuzzlePiece</returns>
    public Sprite GetImage()
    {
        return puzzleImage;
    }

    // returns the name of the PuzzlePiece
    /// <summary>
    /// 
    /// </summary>
    /// <returns>the name of the PuzzlePiece</returns>
    public string GetName()
    {
        return puzzleName;
    }

    /// <returns>The description of the PuzzlePiece</returns>
    public string GetDescription()
    {
        return puzzleDescription;
    }

    /// <returns>A list of PuzzleEffects</returns>
    public List<PuzzleEffect> GetEffects()
    {
        return puzzleEffects;
    }

    public void SetPuzzleColor(PuzzleColor color)
    {
        puzzleColor = color;
    }
}
