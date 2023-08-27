using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class CombatManager : MonoBehaviour
{
    public const int defaultBoardSize = 6;

    public PuzzlePile drawPile, discardPile, handPile;
    public PuzzleBoard puzzleBoard;
    public List<Enemy> enemies;

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        drawPile = new PuzzlePile();
        discardPile = new PuzzlePile();
        handPile = new PuzzlePile();
        puzzleBoard = new PuzzleBoard(defaultBoardSize);
        enemies = new List<Enemy>();
    }

    public void StartEncounter(EnemyEncounter encounter)
    {
        if (encounter != null && encounter.enemies != null && encounter.enemies.Count > 0)
        {
            puzzleBoard.ClearBoard();
            enemies.Clear();
            enemies.AddRange(encounter.enemies);
            drawPile.EmptyPile();
            discardPile.EmptyPile();
            handPile.EmptyPile();
            drawPile.AddPuzzlePieces(playerManager.GetPuzzleDeck());
            drawPile.ShufflePile();
            LoadSprites();
        }
    }

    public void LoadSprites()
    {

    }
}
