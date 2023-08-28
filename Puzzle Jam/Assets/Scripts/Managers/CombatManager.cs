using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class CombatManager : MonoBehaviour
{
    public const int defaultBoardSize = 6;

    private PuzzlePile drawPile, discardPile, handPile;
    private PuzzleBoard puzzleBoard;
    private List<Enemy> enemies;

    public GameObject combatCanvas;

    public SpriteManager puzzleBoardSpriteManager;
    public TooltipManager tooltipManager;
    public List<SpriteManager> puzzlePieceSpriteManagers;
    public List<SpriteManager> enemySpriteManagers;

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
            LoadEnemySprites();
        }
    }

    public void EndEncounter()
    {

    }

    public void LoadEnemySprites()
    {
        for (int i = 0; i < 4 && i < enemies.Count; i++)
        {
            enemySpriteManagers[i].SetSprite(enemies[i].spriteIdle);
        }
    }

    public void UnloadEnemySprite(int index)
    {
        enemySpriteManagers[index].SetSprite(null);
    }

    public void UnloadEnemySprites()
    {
        foreach (SpriteManager spriteManager in enemySpriteManagers)
        {
            spriteManager.SetSprite(null);
        }
    }

    public void LoadPuzzleBoardBackground()
    {

    }
}
