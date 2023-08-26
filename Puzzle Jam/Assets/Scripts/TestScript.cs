using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject puzzlePrefab;
    public PuzzleData puzzleData;

    public void SpawnPuzzlePiece(PuzzleData data, Vector2 position)
    {
        GameObject puzzlePiece = GameObject.Instantiate(puzzlePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        puzzlePiece.GetComponent<PuzzleRenderer>().UpdateSprites(data);
    }

    public void Start()
    {
        SpawnPuzzlePiece(puzzleData, new Vector2(0, 0));
    }
}
