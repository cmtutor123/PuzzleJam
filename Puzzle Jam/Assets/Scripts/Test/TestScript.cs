using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject puzzlePrefab;
    public PuzzleData puzzleData;
    private GameObject puzzlePiece;

    public void SpawnPuzzlePiece(PuzzleData data, Vector2 position)
    {
        puzzlePiece = GameObject.Instantiate(puzzlePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        puzzlePiece.GetComponent<PuzzleRenderer>().UpdateSprites(data);
    }

    public void Update()
    {
        if (puzzlePiece != null) DestroyImmediate(puzzlePiece);
        SpawnPuzzlePiece(puzzleData, new Vector2(0, 0));
    }
}
