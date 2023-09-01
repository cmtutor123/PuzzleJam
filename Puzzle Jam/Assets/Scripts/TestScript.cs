using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public PuzzleData puzzleData;
    public PuzzleRenderer puzzleRenderer;

    void Update()
    {
        puzzleRenderer.UpdateSprites(puzzleData);
    }
}
