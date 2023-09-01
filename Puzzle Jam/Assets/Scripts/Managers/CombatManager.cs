using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class CombatManager : MonoBehaviour
{
    private const int defaultBoardSize = 6;

    private bool inCombat;

    private PuzzlePile drawPile, discardPile, handPile;
    private PuzzleBoard puzzleBoard;
    private List<Enemy> enemies;

    //[SerializeField] private GameObject combatCanvas;

    [Header("UI Elements")]
    [SerializeField] private SpriteManager puzzleBoardSpriteManager;
    [SerializeField] private TooltipManager tooltipManager;
    [SerializeField] private SpriteManager mouseSpriteManager;
    [SerializeField] private PuzzleRenderer mousePuzzleRenderer;
    [SerializeField] private List<PuzzleRenderer> handPuzzlePieceRenderers, boardPuzzlePieceRenderers;
    [SerializeField] private List<SpriteManager> enemySpriteManagers;

    [Header("Test Encounter")]
    public EnemyEncounter testEncounter;

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
        StartEncounter(testEncounter);
    }

    // starts an encounter using data from PlayerData and the EnemyEncounter
    public void StartEncounter(EnemyEncounter encounter)
    {
        if (encounter != null && encounter.GetEnemies() != null && encounter.GetEnemyCount() > 0)
        {
            puzzleBoard.ClearBoard();
            enemies.Clear();
            enemies = encounter.GetEnemies();
            drawPile.EmptyPile();
            discardPile.EmptyPile();
            handPile.EmptyPile();
            drawPile.AddPuzzlePieces(playerManager.GetPuzzleDeck());
            drawPile.ShufflePile();
            LoadEnemySprites();
            LoadPuzzleBoardBackground();
            UpdateHandSprites();
            UpdateBoardPieceSprites();
            inCombat = true;
        }
    }

    // ends an encounter
    public void EndEncounter()
    {
        inCombat = false;
    }

    // displays all of enemy's idle Sprites on screen
    public void LoadEnemySprites()
    {
        for (int i = 0; i < 4 && i < enemies.Count; i++)
        {
            enemySpriteManagers[i].SetSprite(enemies[i].GetSpriteIdle());
        }
    }

    // removes a specified enemy's Sprite from screen
    public void UnloadEnemySprite(int index)
    {
        enemySpriteManagers[index].SetSprite(null);
    }

    // removes all enemy Sprites from screen
    public void UnloadEnemySprites()
    {
        foreach (SpriteManager spriteManager in enemySpriteManagers)
        {
            spriteManager.SetSprite(null);
        }
    }

    // displays the puzzle board's background Sprite
    public void LoadPuzzleBoardBackground()
    {
        puzzleBoardSpriteManager.SetSprite(playerManager.GetPuzzleBoardSprite());
    }

    // removes the puzzle board's background Sprite
    public void UnloadPuzzleBoardBackground()
    {
        puzzleBoardSpriteManager.SetSprite(null);
    }

    // updates all of the Sprites of the PuzzlePieces in the player's hand PuzzlePile
    public void UpdateHandSprites()
    {
        for (int i = 0; i < handPuzzlePieceRenderers.Count; i++)
        {
            if (i < handPile.GetSize())
            {
                PuzzlePiece piece = handPile.GetPuzzlePiece(i);
                handPuzzlePieceRenderers[i].UpdateSprites(piece);
            }
            else
            {
                handPuzzlePieceRenderers[i].UnloadSprites();
            }
        }
    }

    // removes all of the Sprites of the PuzzlePieces in the player's hand PuzzlePile
    public void UnloadHandSprites()
    {
        foreach (PuzzleRenderer puzzleRenderer in handPuzzlePieceRenderers)
        {
            puzzleRenderer.UnloadSprites();
        }
    }

    // updates the tooltip ui with information from a PuzzlePiece
    public void SetTooltipUIFromPuzzlePiece(PuzzlePiece piece)
    {
        tooltipManager.SetSprite(piece.GetImage());
        tooltipManager.SetText(piece.GetName(), piece.GetDescription());
    }

    // updates the tooltip ui based on the ui id and index provided
    public void UpdateTooltipUI(UIID uiid, int index)
    {
        switch (uiid)
        {
            case UIID.Hand:
                if (handPile.GetSize() > index)
                {
                    SetTooltipUIFromPuzzlePiece(handPile.GetPuzzlePiece(index));
                }
                break;
            case UIID.Board:
                PuzzlePiece boardPiece = puzzleBoard.GetPuzzlePieceFromIndex(index);
                if (boardPiece != null)
                {
                    SetTooltipUIFromPuzzlePiece(boardPiece);
                }
                break;
            case UIID.Enemy:

                break;
            case UIID.EnemyAttack:

                break;
        }
    }

    // removes tooltip ui from screen
    public void UnloadTooltipUI()
    {
        tooltipManager.UnloadSprites();
    }
    
    // updates all of the Sprites of the PuzzlePieces on the puzzle board
    public void UpdateBoardPieceSprites()
    {
        for (int i = 0; i < puzzleBoard.GetSize(); i++)
        {
            UpdateBoardPieceSprite(i);
        }
    }
    
    // updates the Sprite of the PuzzlePiece at a specified location on the PuzzleBoard
    public void UpdateBoardPieceSprite(int index)
    {
        PuzzlePiece boardPiece = puzzleBoard.GetPuzzlePieceFromIndex(index);
        if (boardPiece != null)
        {
            boardPuzzlePieceRenderers[index].UpdateSprites(boardPiece);
        }
        else
        {
            boardPuzzlePieceRenderers[index].UnloadSprites();
        }
    }

    // changes the Sprite that follows the mouse
    public void SetMouseSprite(Sprite sprite)
    {
        mousePuzzleRenderer.UnloadSprites();
        mouseSpriteManager.SetSprite(sprite);
    }

    // changes the PuzzlePiece Sprites that follow the mouse
    public void SetMousePuzzle(PuzzlePiece piece)
    {
        mouseSpriteManager.UnloadSprites();
        mousePuzzleRenderer.UpdateSprites(piece);
    }

    // removes all Sprites that are following the mouse
    public void ClearMouseImage()
    {
        mousePuzzleRenderer.UnloadSprites();
        mouseSpriteManager.UnloadSprites();
    }

    // start player turn
    public void StartPlayerTurn()
    {

    }

    // end player turn
    public void EndPlayerTurn()
    {

    }

    // start enemy turn
    public void StartEnemyTurn()
    {

    }

    // end enemy turn
    public void EndEnemyTurn()
    {

    }

    // triggers click events
    public void ObjectClicked(UIID uiid, int index)
    {
        Debug.Log("Clicked " + uiid + " " + index);
    }
}
