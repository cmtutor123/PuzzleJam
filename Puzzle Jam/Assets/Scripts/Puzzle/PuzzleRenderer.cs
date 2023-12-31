using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages sprites to form puzzle pieces dynamically
/// </summary>
public class PuzzleRenderer : MonoBehaviour
{
    [Header("Puzzle Piece Segments")]
    [SerializeField] private Sprite blankV;
    [SerializeField] private Sprite blankH;
    [SerializeField] private Sprite socketV;
    [SerializeField] private Sprite socketH;
    [SerializeField] private Sprite keyV;
    [SerializeField] private Sprite keyH;
    [SerializeField] private Sprite center;
    [SerializeField] private Sprite cornerDL;
    [SerializeField] private Sprite cornerDR;
    [Header("Empty Sprite")]
    [SerializeField] private Sprite empty;
    [Header("Center Image Sprite Renderer")]
    [SerializeField] private Image image;
    [Header("Segment Sprite Renderers")]
    [SerializeField] private Image top;
    [SerializeField] private Image topLeft;
    [SerializeField] private Image topMiddle;
    [SerializeField] private Image topRight;
    [SerializeField] private Image left;
    [SerializeField] private Image middleLeft;
    [SerializeField] private Image middle;
    [SerializeField] private Image middleRight;
    [SerializeField] private Image right;
    [SerializeField] private Image bottomLeft;
    [SerializeField] private Image bottomMiddle;
    [SerializeField] private Image bottomRight;
    [SerializeField] private Image bottom;

    private void Start()
    {
        UnloadSprites();
    }

    /// <summary>
    /// Updates the sprite renderers to match a given PuzzlePiece
    /// </summary>
    /// <param name="puzzlePiece">The PuzzlePiece to match</param>
    public void UpdateSprites(PuzzlePiece puzzlePiece)
    {;
        // reads the data from the puzzle piece
        Color color = puzzlePiece.GetColor();
        PuzzleEdge topEdge = puzzlePiece.GetTop();
        PuzzleEdge leftEdge = puzzlePiece.GetLeft();
        PuzzleEdge rightEdge = puzzlePiece.GetRight();
        PuzzleEdge bottomEdge = puzzlePiece.GetBottom();
        Sprite puzzleImage = puzzlePiece.GetImage();
        // updates the color of all of the sprite renderers (except for the center image)
        UpdateColor(top, color);
        UpdateColor(topLeft, color);
        UpdateColor(topMiddle, color);
        UpdateColor(topRight, color);
        UpdateColor(left, color);
        UpdateColor(middleLeft, color);
        UpdateColor(middle, color);
        UpdateColor(middleRight, color);
        UpdateColor(right, color);
        UpdateColor(bottomLeft, color);
        UpdateColor(bottomMiddle, color);
        UpdateColor(bottomRight, color);
        UpdateColor(bottom, color);
        // updates the sprite of all of the sprite renderers
        UpdateSprite(topLeft, cornerDL);
        UpdateSprite(topRight, cornerDR);
        UpdateSprite(middle, center);
        UpdateSprite(bottomLeft, cornerDR);
        UpdateSprite(bottomRight, cornerDL);
        if (topEdge == PuzzleEdge.Socket) UpdateSprite(topMiddle, socketV);
        else UpdateSprite(topMiddle, blankV);
        if (topEdge == PuzzleEdge.Key) UpdateSprite(top, keyV);
        else UpdateSprite(top, empty);
        if (leftEdge == PuzzleEdge.Socket) UpdateSprite(middleLeft, socketH);
        else UpdateSprite(middleLeft, blankH);
        if (leftEdge == PuzzleEdge.Key) UpdateSprite(left, keyH);
        else UpdateSprite(left, empty);
        if (rightEdge == PuzzleEdge.Socket) UpdateSprite(middleRight, socketH);
        else UpdateSprite(middleRight, blankH);
        if (rightEdge == PuzzleEdge.Key) UpdateSprite(right, keyH);
        else UpdateSprite(right, empty);
        if (bottomEdge == PuzzleEdge.Socket) UpdateSprite(bottomMiddle, socketV);
        else UpdateSprite(bottomMiddle, blankV);
        if (bottomEdge == PuzzleEdge.Key) UpdateSprite(bottom, keyV);
        else UpdateSprite(bottom, empty);
        UpdateSprite(image, puzzleImage);
    }

    /// <summary>
    /// Updates the sprite renderers to match a given PuzzlePiece
    /// </summary>
    /// <param name="puzzleData">The PuzzleData of the PuzzlePiece to match</param>
    public void UpdateSprites(PuzzleData puzzleData)
    {
        UpdateSprites(new PuzzlePiece(puzzleData));
    }

    /// <summary>
    /// Changes the color of a sprite renderer
    /// </summary>
    /// <param name="renderer">The sprite renderer to change</param>
    /// <param name="color">The color to change it to</param>
    public void UpdateColor(Image renderer, Color color)
    {
        renderer.color = color;
    }

    /// <summary>
    /// Changes the sprite of a sprite renderer
    /// </summary>
    /// <param name="renderer">The sprite renderer to change</param>
    /// <param name="sprite">The sprite to change it to</param>
    public void UpdateSprite(Image renderer, Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    /// <summary>
    /// Removes all of the sprites from the sprite renderers
    /// </summary>
    public void UnloadSprites()
    {
        UpdateSprite(top, empty);
        UpdateSprite(topLeft, empty);
        UpdateSprite(topMiddle, empty);
        UpdateSprite(topRight, empty);
        UpdateSprite(left, empty);
        UpdateSprite(middleLeft, empty);
        UpdateSprite(middle, empty);
        UpdateSprite(middleRight, empty);
        UpdateSprite(right, empty);
        UpdateSprite(bottomLeft, empty);
        UpdateSprite(bottomMiddle, empty);
        UpdateSprite(bottomRight, empty);
        UpdateSprite(bottom, empty);
        UpdateSprite(image, empty);
    }

    public void SetPosition(float xPos, float yPos)
    {
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    public Sprite GetImage()
    {
        return image.sprite;
    }
}
