using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class CombatManager : MonoBehaviour
{
    public const int defaultBoardSize = 6;

    public bool inCombat;

    private PuzzlePile drawPile, discardPile, handPile;
    private PuzzleBoard puzzleBoard;
    private List<Enemy> enemies;

    public GameObject combatCanvas;

    public SpriteManager puzzleBoardSpriteManager;
    public TooltipManager tooltipManager;
    public List<PuzzleRenderer> handPuzzlePieceRenderers;
    public List<SpriteManager> enemySpriteManagers;

    private PlayerManager playerManager;

    private void Start()
    {
        inCombat = false;
        playerManager = GetComponent<PlayerManager>();
        drawPile = new PuzzlePile();
        discardPile = new PuzzlePile();
        handPile = new PuzzlePile(6);
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
            LoadPuzzleBoardBackground();

            inCombat = true;
        }
    }

    public void EndEncounter()
    {
        inCombat = false;
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
        puzzleBoardSpriteManager.SetSprite(playerManager.GetPuzzleBoardSprite());
    }

    public void UnloadPuzzleBoardBackground()
    {
        puzzleBoardSpriteManager.SetSprite(null);
    }

    public void UpdateHandSprites()
    {
        for (int i = 0; i < handPuzzlePieceRenderers.Count; i++)
        {
            if (handPile.GetSize() - 1 < i)
            {
                PuzzlePiece piece = handPile.GetPuzzlePiece(i);
                handPuzzlePieceRenderers[i].UpdateSprites(piece.top, piece.left, piece.right, piece.bottom, piece.color);
            }
            else
            {
                handPuzzlePieceRenderers[i].UnloadSprites();
            }
        }
    }

    public void UnloadHandSprites()
    {
        foreach (PuzzleRenderer puzzleRenderer in handPuzzlePieceRenderers)
        {
            puzzleRenderer.UnloadSprites();
        }
    }

    public void UpdateTooltipUI(UIID uiid, int index)
    {
        switch (uiid)
        {
            case UIID.Hand:

                break;
            case UIID.Board:

                break;
            case UIID.Enemy:

                break;
            case UIID.EnemyAttack:

                break;
        }
    }

    public void UnloadTooltipUI()
    {
        tooltipManager.UnloadSprites();
    }
}
