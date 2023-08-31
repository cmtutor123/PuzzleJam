using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRenderer : MonoBehaviour
{
    [Header("Puzzle Piece Segments")]
    [SerializeField] private Sprite blank, socket, key;
    [Header("Center Image Sprite Renderer")]
    [SerializeField] private SpriteRenderer image;
    [Header("Segment Sprite Renderers")]
    [SerializeField] private SpriteRenderer top, topLeft, topMiddle, topRight, left, middleLeft, middle, middleRight, right, bottomLeft, bottomMiddle, bottomRight, bottom;

    // updates the sprite renderers to match a given puzzle piece
    public void UpdateSprites(PuzzlePiece puzzlePiece)
    {
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
        UpdateSprite(topLeft, blank);
        UpdateSprite(topRight, blank);
        UpdateSprite(middle, blank);
        UpdateSprite(bottomLeft, blank);
        UpdateSprite(bottomRight, blank);
        if (topEdge == PuzzleEdge.Socket) UpdateSprite(topMiddle, socket);
        else UpdateSprite(topMiddle, blank);
        if (topEdge == PuzzleEdge.Key) UpdateSprite(top, key);
        else UpdateSprite(top, null);
        if (leftEdge == PuzzleEdge.Socket) UpdateSprite(middleLeft, socket);
        else UpdateSprite(middleLeft, blank);
        if (leftEdge == PuzzleEdge.Key) UpdateSprite(left, key);
        else UpdateSprite(left, null);
        if (rightEdge == PuzzleEdge.Socket) UpdateSprite(middleRight, socket);
        else UpdateSprite(middleRight, blank);
        if (rightEdge == PuzzleEdge.Key) UpdateSprite(right, key);
        else UpdateSprite(right, null);
        if (bottomEdge == PuzzleEdge.Socket) UpdateSprite(bottomMiddle, socket);
        else UpdateSprite(bottomMiddle, blank);
        if (bottomEdge == PuzzleEdge.Key) UpdateSprite(bottom, key);
        else UpdateSprite(bottom, null);
        UpdateSprite(image, puzzleImage);
    }

    // changes the color of a sprite renderer
    public void UpdateColor(SpriteRenderer renderer, Color color)
    {
        renderer.color = color;
    }

    // changes the sprite of a sprite renderer
    public void UpdateSprite(SpriteRenderer renderer, Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    // removes all of the sprites from the sprite renderers
    public void UnloadSprites()
    {
        UpdateSprite(top, null);
        UpdateSprite(topLeft, null);
        UpdateSprite(topMiddle, null);
        UpdateSprite(topRight, null);
        UpdateSprite(left, null);
        UpdateSprite(middleLeft, null);
        UpdateSprite(middle, null);
        UpdateSprite(middleRight, null);
        UpdateSprite(right, null);
        UpdateSprite(bottomLeft, null);
        UpdateSprite(bottomMiddle, null);
        UpdateSprite(bottomRight, null);
        UpdateSprite(bottom, null);
        UpdateSprite(image, null);
    }
}
