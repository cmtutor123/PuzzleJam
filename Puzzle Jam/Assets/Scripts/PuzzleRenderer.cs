using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRenderer : MonoBehaviour
{
    public Sprite blank, socket, key;
    public SpriteRenderer top, topLeft, topMiddle, topRight, left, middleLeft, middle, middleRight, right, bottomLeft, bottomMiddle, bottomRight, bottom;

    public void UpdateSprites(PuzzleData data)
    {
        if (data != null)
        {
            UpdateColor(top, data.puzzleColor.color);
            UpdateColor(topLeft, data.puzzleColor.color);
            UpdateColor(topMiddle, data.puzzleColor.color);
            UpdateColor(topRight, data.puzzleColor.color);
            UpdateColor(left, data.puzzleColor.color);
            UpdateColor(middleLeft, data.puzzleColor.color);
            UpdateColor(middle, data.puzzleColor.color);
            UpdateColor(middleRight, data.puzzleColor.color);
            UpdateColor(right, data.puzzleColor.color);
            UpdateColor(bottomLeft, data.puzzleColor.color);
            UpdateColor(bottomMiddle, data.puzzleColor.color);
            UpdateColor(bottomRight, data.puzzleColor.color);
            UpdateColor(bottom, data.puzzleColor.color);
            UpdateSprite(topLeft, blank);
            UpdateSprite(topRight, blank);
            UpdateSprite(middle, blank);
            UpdateSprite(bottomLeft, blank);
            UpdateSprite(bottomRight, blank);
            if (data.puzzleShape.top == PuzzleEdge.Socket) UpdateSprite(topMiddle, socket);
            else UpdateSprite(topMiddle, blank);
            if (data.puzzleShape.top == PuzzleEdge.Key) UpdateSprite(top, key);
            else UpdateSprite(top, null);
            if (data.puzzleShape.left == PuzzleEdge.Socket) UpdateSprite(middleLeft, socket);
            else UpdateSprite(middleLeft, blank);
            if (data.puzzleShape.left == PuzzleEdge.Key) UpdateSprite(left, key);
            else UpdateSprite(left, null);
            if (data.puzzleShape.right == PuzzleEdge.Socket) UpdateSprite(middleRight, socket);
            else UpdateSprite(middleRight, blank);
            if (data.puzzleShape.right == PuzzleEdge.Key) UpdateSprite(right, key);
            else UpdateSprite(right, null);
            if (data.puzzleShape.bottom == PuzzleEdge.Socket) UpdateSprite(bottomMiddle, socket);
            else UpdateSprite(bottomMiddle, blank);
            if (data.puzzleShape.bottom == PuzzleEdge.Key) UpdateSprite(bottom, key);
            else UpdateSprite(bottom, null);
        }
        else
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
        }
    }

    public void UpdateColor(SpriteRenderer renderer, Color color)
    {
        renderer.color = color;
    }

    public void UpdateSprite(SpriteRenderer renderer, Sprite sprite)
    {
        renderer.sprite = sprite;
    }
}
