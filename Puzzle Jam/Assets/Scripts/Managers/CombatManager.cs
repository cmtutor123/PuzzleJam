using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all of the logic required to run combat
/// </summary>
[RequireComponent(typeof(PlayerManager), typeof(MapManager))]
public class CombatManager : MonoBehaviour
{
    private CombatState combatState;

    private bool playerTurn;
    private bool enemyTurn;

    private const int defaultBoardSize = 6;
    private const int turnDrawAmount = 6;
    private const int energyCount = 3;

    private int puzzlePiecesPlayed;

    private PuzzlePiece selectedPuzzlePiece;

    private PuzzlePile drawPile, discardPile, handPile;
    private PuzzleBoard puzzleBoard;
    private List<Enemy> enemies;

    private List<ActiveEffect> effectQueue;

    //[SerializeField] private GameObject combatCanvas;

    [Header("UI Elements")]
    [SerializeField] private SpriteManager puzzleBoardSpriteManager;
    [SerializeField] private TooltipManager tooltipManager;
    [SerializeField] private SpriteManager mouseSpriteManager;
    [SerializeField] private PuzzleRenderer mousePuzzleRenderer;
    [SerializeField] private List<PuzzleRenderer> handPuzzlePieceRenderers, boardPuzzlePieceRenderers;
    [SerializeField] private List<EnemySpriteManager> enemySpriteManagers;
    [SerializeField] private SpriteManager endButton;

    [Header("Sprites")]
    [SerializeField] private Sprite targetingSprite;
    [SerializeField] private Sprite buttonEndSprite;
    [SerializeField] private Sprite buttonCancelSprite;

    [Header("Test Variables")]
    public EnemyEncounter testEncounter;
    public PuzzleData testPuzzlePiece;

    private PlayerManager playerManager;
    private MapManager mapManager;



    private void Start()
    {
        combatState = CombatState.OutOfCombat;
        playerManager = GetComponent<PlayerManager>();
        mapManager = GetComponent<MapManager>();
        drawPile = new PuzzlePile();
        discardPile = new PuzzlePile();
        handPile = new PuzzlePile(6);
        puzzleBoard = new PuzzleBoard(defaultBoardSize);
        enemies = new List<Enemy>();
        selectedPuzzlePiece = null;
        effectQueue = new List<ActiveEffect>();
        //StartEncounter(testEncounter);
    }

    /// <summary>
    /// Starts an encounter using data from the current PlayerData and EnemyEncounter objects
    /// </summary>
    /// <param name="encounter">The EnemyEncounter object containing the enemies that are going to be fought</param>
    public void StartEncounter(EnemyEncounter encounter)
    {
        if (encounter != null && encounter.GetEnemies() != null && encounter.GetEnemyCount() > 0)
        {
            SetCombatState(CombatState.DoingStuff);
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
            ClearMouseImage();
            ShowPlayerHealth();
            StartPlayerTurn();
        }
    }

    /// <summary>
    /// Ends the current encounter
    /// </summary>
    public void EndEncounter()
    {
        SetCombatState(CombatState.OutOfCombat);
        UnloadEnemySprites();
        UnloadHandSprites();
        UnloadPuzzleBoardBackground();
        UnloadTooltipUI();
        HideEndButton();
        HidePlayerHealth();
        ClearMouseImage();
    }

    /// <summary>
    /// Displays all of the enemy's idle sprites
    /// </summary>
    public void LoadEnemySprites()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < enemies.Count)
            {
                enemySpriteManagers[i].SetSprite(enemies[i].GetSpriteIdle(), enemies[i].GetCurrentHealth(), enemies[i].GetMaxHealth(), enemies[i].GetNextAttack());
            }
            else UnloadEnemySprite(i);
        }
    }

    /// <summary>
    /// Removes an enemy's sprites
    /// </summary>
    /// <param name="index">The index of the enemy</param>
    public void UnloadEnemySprite(int index)
    {
        enemySpriteManagers[index].UnloadSprites();
    }

    /// <summary>
    /// Removes all enemy sprites
    /// </summary>
    public void UnloadEnemySprites()
    {
        foreach (SpriteManager spriteManager in enemySpriteManagers)
        {
            spriteManager.UnloadSprites();
        }
    }

    /// <summary>
    /// Displays the puzzle board's background sprite
    /// </summary>
    public void LoadPuzzleBoardBackground()
    {
        puzzleBoardSpriteManager.SetSprite(playerManager.GetPuzzleBoardSprite());
    }

    /// <summary>
    /// Removes the puzzle board's background sprite
    /// </summary>
    public void UnloadPuzzleBoardBackground()
    {
        puzzleBoardSpriteManager.SetSprite(null);
    }

    /// <summary>
    /// Updates all of the sprites of the PuzzlePiece objects in the player's hand PuzzlePile
    /// </summary>
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

    /// <summary>
    /// Removes all of the sprites of the PuzzlePiece objects the the player's hand PuzzlePile
    /// </summary>
    public void UnloadHandSprites()
    {
        foreach (PuzzleRenderer puzzleRenderer in handPuzzlePieceRenderers)
        {
            puzzleRenderer.UnloadSprites();
        }
    }

    /// <summary>
    /// Updates the tooltip UI
    /// </summary>
    /// <param name="piece">The PuzzlePiece object to load data from</param>
    public void SetTooltipUIFromPuzzlePiece(PuzzlePiece piece)
    {
        tooltipManager.SetSprite(piece.GetImage());
        tooltipManager.SetText(piece.GetName(), piece.GetDescription());
    }

    /// <summary>
    /// Updates the tooltip UI
    /// </summary>
    /// <param name="uiid">The UIID of the object</param>
    /// <param name="index">The index of the UIID object</param>
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
                PuzzlePiece boardPiece = puzzleBoard.GetPuzzlePiece(index);
                if (boardPiece != null)
                {
                    SetTooltipUIFromPuzzlePiece(boardPiece);
                }
                break;
            case UIID.Enemy:
                if (enemies != null && index < enemies.Count && enemies[index] != null)
                {
                    tooltipManager.SetSprite(enemies[index].GetSpriteIdle());
                    tooltipManager.SetText(enemies[index].GetName(), string.Empty);
                }
                break;
            case UIID.EnemyAttack:
                if (enemies != null && index < enemies.Count && enemies[index] != null && enemies[index].GetNextAttack() != null)
                {
                    SetTooltipUIFromPuzzlePiece(new PuzzlePiece(enemies[index].GetNextAttack()));
                }
                break;
        }
    }

    /// <summary>
    /// Removes tooltip UI
    /// </summary>
    public void UnloadTooltipUI()
    {
        tooltipManager.UnloadSprites();
    }

    /// <summary>
    /// Updates the sprites of the PuzzlePiece objects on the puzzle board
    /// </summary>
    public void UpdateBoardPieceSprites()
    {
        for (int i = 0; i < puzzleBoard.GetSize(); i++)
        {
            UpdateBoardPieceSprite(i);
        }
    }

    /// <summary>
    /// Updates the sprite of a PuzzlePiece object
    /// </summary>
    /// <param name="index">The index of the PuzzlePiece object on the PuzzleBoard</param>
    public void UpdateBoardPieceSprite(int index)
    {
        PuzzlePiece boardPiece = puzzleBoard.GetPuzzlePiece(index);
        if (boardPiece != null)
        {
            boardPuzzlePieceRenderers[index].UpdateSprites(boardPiece);
        }
        else
        {
            boardPuzzlePieceRenderers[index].UnloadSprites();
        }
    }

    /// <summary>
    /// Makes a sprite follow the mouse
    /// </summary>
    /// <param name="sprite">The sprite that will follow the mouse</param>
    public void SetMouseSprite(Sprite sprite)
    {
        mousePuzzleRenderer.UnloadSprites();
        mouseSpriteManager.SetSprite(sprite);
    }

    /// <summary>
    /// Makes a sprite follow the mouse
    /// </summary>
    /// <param name="piece">The PuzzlePiece object that will follow the mouse</param>
    public void SetMousePuzzle(PuzzlePiece piece)
    {
        mouseSpriteManager.UnloadSprites();
        mousePuzzleRenderer.UpdateSprites(piece);
    }

    /// <summary>
    /// Removes all sprites that are following the mouse
    /// </summary>
    public void ClearMouseImage()
    {
        mousePuzzleRenderer.UnloadSprites();
        mouseSpriteManager.UnloadSprites();
    }

    /// <summary>
    /// Start player turn
    /// </summary>
    public void StartPlayerTurn()
    {
        SetButtonEnd();
        SetCombatState(CombatState.PlayerTurn);
        DrawHand(turnDrawAmount);
        puzzlePiecesPlayed = 0;
        playerTurn = true;
    }

    /// <summary>
    /// Adds PuzzlePiece objects ot the player's hand PuzzlePile, if possible
    /// </summary>
    /// <param name="drawAmount">The number of PuzzlePiece objects to add</param>
    public void DrawHand(int drawAmount)
    {
        if (drawPile.GetSize() >= drawAmount)
        {
            for (int i = 0; i < drawAmount; i++)
            {
                handPile.AddPuzzlePiece(drawPile.DrawPuzzlePiece());
            }
        }
        else
        {
            int drawnCount = 0;
            while (drawnCount < drawAmount && (drawPile.GetSize() > 0 || discardPile.GetSize() > 0))
            {
                if (drawPile.GetSize() > 0)
                {
                    handPile.AddPuzzlePiece(drawPile.DrawPuzzlePiece());
                    drawnCount++;
                }
                else
                {
                    DiscardToDraw();
                }
            }
        }
        UpdateHandSprites();
    }

    /// <summary>
    /// Puts all of the PuzzlePiece objects in the discard PuzzlePile into the draw PuzzlePile
    /// </summary>
    public void DiscardToDraw()
    {
        drawPile.AddPuzzlePieces(discardPile.DiscardPile());
        drawPile.ShufflePile();
    }

    /// <summary>
    /// Puts all of the PuzzlePiece objects in the hand PuzzlePile into the discard PuzzlePile
    /// </summary>
    public void DiscardHand()
    {
        discardPile.AddPuzzlePieces(handPile.DiscardPile());
        UpdateHandSprites();
    }

    /// <summary>
    /// End player turn
    /// </summary>
    public void EndPlayerTurn()
    {
        HideEndButton();
        SetCombatState(CombatState.DoingStuff);
        playerTurn = false;
        DiscardHand();
        StartEnemyTurn();
    }

    /// <summary>
    /// Start enemy turn
    /// </summary>
    public void StartEnemyTurn()
    {
        SetCombatState(CombatState.EnemyTurn);
        enemyTurn = true;
        DoEnemyAttacks();
    }

    /// <summary>
    /// End enemy turn
    /// </summary>
    public void EndEnemyTurn()
    {
        enemyTurn = false;
        ReloadEnemySprites();
        StartPlayerTurn();
    }

    /// <summary>
    /// Triggers click events
    /// </summary>
    /// <param name="uiid">The UIID of the object clicked</param>
    /// <param name="index">The index of the UIID object clicked</param>
    public void ObjectClicked(UIID uiid, int index)
    {
        switch (combatState)
        {
            case CombatState.PlayerTurn:
                if (uiid == UIID.End)
                {
                    EndPlayerTurn();
                }
                else if (uiid == UIID.Hand)
                {
                    AttemptHandSelection(index);
                }
                break;
            case CombatState.PieceSelected:
                if (uiid == UIID.End)
                {
                    ReturnHandSelection();
                }
                else if (uiid == UIID.Board)
                {
                    AttemptBoardPlacement(index);
                }
                break;
            case CombatState.PickingTarget:
                if (uiid == UIID.Enemy)
                {
                    AttemptPickTargetEnemy(index);
                }
                if (uiid == UIID.Board)
                {
                    AttemptPickTargetPuzzlePiece(index);
                }
                break;
        }
    }

    /// <summary>
    /// Selects a PuzzlePiece from the hand PuzzlePile if the index is in the current hand size
    /// </summary>
    /// <param name="index">The index of PuzzlePiece</param>
    public void AttemptHandSelection(int index)
    {
        if (index < handPile.GetSize() && selectedPuzzlePiece == null)
        {
            SetButtonCancel();
            SetCombatState(CombatState.PieceSelected);
            selectedPuzzlePiece = handPile.DrawPuzzlePiece(index);
            SetMousePuzzle(selectedPuzzlePiece);
            UpdateHandSprites();
        }
    }

    /// <summary>
    /// Returns the selected PuzzlePiece object to the hand PuzzlePile
    /// </summary>
    public void ReturnHandSelection()
    {
        SetButtonEnd();
        SetCombatState(CombatState.PlayerTurn);
        handPile.AddPuzzlePiece(selectedPuzzlePiece);
        selectedPuzzlePiece = null;
        ClearMouseImage();
        UpdateHandSprites();
    }

    /// <summary>
    /// Attempts to place the selected PuzzlePiece on the PuzzleBoard
    /// </summary>
    /// <param name="index">The index of the PuzzleBoard position</param>
    public void AttemptBoardPlacement(int index)
    {
        SetCombatState(CombatState.DoingStuff);
        SetButtonEnd();
        if (puzzlePiecesPlayed < energyCount && puzzleBoard.PieceCanFit(index, selectedPuzzlePiece))
        {
            ClearMouseImage();
            PlacePiece(selectedPuzzlePiece, index);
            puzzlePiecesPlayed++;
            selectedPuzzlePiece = null;
            UpdateBoardPieceSprites();
        }
        else
        {
            ReturnHandSelection();
            SetCombatState(CombatState.PlayerTurn);
        }
    }

    /// <summary>
    /// Attempts to use the selected action on a target Enemy
    /// </summary>
    /// <param name="index">The index of the target Enemy</param>
    public void AttemptPickTargetEnemy(int index)
    {
        ActiveEffect activeEffect = effectQueue[0];
        if (activeEffect.GetType() == typeof(EffectDamageEnemy))
        {
            EffectDamageEnemy effect = (EffectDamageEnemy)activeEffect;
            if (effect.GetTargetType() == TargetType.Single && enemies[index] != null && enemies[index].ValidTarget())
            {
                int times = 0;
                int damage = effect.GetDamage();
                int repetitions = effect.GetRepetitions();
                while (enemies[index].ValidTarget() && times++ < repetitions)
                {
                    DamageEnemy(enemies[index], damage);
                }
                effectQueue.RemoveAt(0);
                EndTargeting();
            }
        }
        else if (activeEffect.GetType() == typeof(EffectBuffEnemy))
        {
            EffectBuffEnemy effect = (EffectBuffEnemy)activeEffect;
            if (effect.GetTargetType() == TargetType.Single && enemies[index] != null && enemies[index].ValidTarget())
            {
                int times = 0;
                int damage = effect.GetAmount();
                int repetitions = effect.GetRepetitions();
                BuffID buff = effect.GetBuff();
                while (enemies[index].ValidTarget() && times++ < repetitions)
                {
                    BuffEnemy(enemies[index], buff, damage);
                }
                effectQueue.RemoveAt(0);
                EndTargeting();
            }
        }
    }

    /// <summary>
    /// Attempts to use the selected action on a target PuzzlePiece
    /// </summary>
    /// <param name="index">the index of the target PuzzlePiece on the PuzzleBoard</param>
    public void AttemptPickTargetPuzzlePiece(int index)
    {
        ActiveEffect activeEffect = effectQueue[0];
        if (activeEffect.GetType() == typeof(EffectDestroyPiece))
        {
            EffectDestroyPiece effect = (EffectDestroyPiece)activeEffect;
            if (effect.GetTargetType() == TargetType.Single && puzzleBoard.GetPuzzlePiece(index) != null && effect.GetColorCondition() == null ? true : effect.GetColorCondition() == puzzleBoard.GetPuzzlePiece(index).GetPuzzleColor())
            {
                DestroyPiece(index);
                activeEffect.ReduceRepetition();
                if (activeEffect.GetRepetitions() <= 0 || (effect.GetColorCondition() == null ? GetValidPuzzleTargets().Count == 0 : GetValidPuzzleTargets(effect.GetColorCondition()).Count == 0))
                {
                    effectQueue.RemoveAt(0);
                    EndTargeting();
                }
            }
        }
        else if (activeEffect.GetType() == typeof(EffectColorPiece))
        {
            EffectColorPiece effect = (EffectColorPiece)activeEffect;
            if (effect.GetTargetType() == TargetType.Single && puzzleBoard.GetPuzzlePiece(index) != null && effect.GetColorCondition() == null ? true : effect.GetColorCondition() == puzzleBoard.GetPuzzlePiece(index).GetPuzzleColor())
            {
                ChangePuzzleColor(index, effect.GetNewColor());
                activeEffect.ReduceRepetition();
                if (activeEffect.GetRepetitions() <= 0 || (effect.GetColorCondition() == null ? GetValidPuzzleTargets().Count == 0 : GetValidPuzzleTargets(effect.GetColorCondition()).Count == 0))
                {
                    effectQueue.RemoveAt(0);
                    EndTargeting();
                }
            }
        }
        else if (activeEffect.GetType() == typeof(EffectShapePiece))
        {
            EffectShapePiece effect = (EffectShapePiece)activeEffect;
            if (effect.GetTargetType() == TargetType.Single && puzzleBoard.GetPuzzlePiece(index) != null && effect.GetColorCondition() == null ? true : effect.GetColorCondition() == puzzleBoard.GetPuzzlePiece(index).GetPuzzleColor())
            {
                ChangePuzzleShape(index, effect.GetNewShape());
                activeEffect.ReduceRepetition();
                if (activeEffect.GetRepetitions() <= 0 || (effect.GetColorCondition() == null ? GetValidPuzzleTargets().Count == 0 : GetValidPuzzleTargets(effect.GetColorCondition()).Count == 0))
                {
                    effectQueue.RemoveAt(0);
                    EndTargeting();
                }
            }
        }
    }

    /// <summary>
    /// Changes the CombatState
    /// </summary>
    /// <param name="newCombatState">The CombatState to change to</param>
    public void SetCombatState(CombatState newCombatState)
    {
        combatState = newCombatState;
    }

    /// <summary>
    /// Shows the player health bar
    /// </summary>
    public void ShowPlayerHealth()
    {
        playerManager.ShowHealthBar();
    }

    /// <summary>
    /// Hides the player health bar
    /// </summary>
    public void HidePlayerHealth()
    {
        playerManager.HideHealthBar();
    }

    /// <summary>
    /// Places a PuzzlePiece at a location on the PuzzleBoard
    /// </summary>
    /// <param name="puzzlePiece">The PuzzlePiece to place</param>
    /// <param name="index">The index of the location</param>
    public void PlacePiece(PuzzlePiece puzzlePiece, int index)
    {
        puzzleBoard.PlacePiece(puzzlePiece, index);
        QueueEffects(CheckForTrigger(puzzlePiece, TriggerType.Place), index);
        QueueEffects(CheckForTrigger(puzzleBoard.GetAdjacent(index), TriggerType.Adjacent), index);
        QueueEffects(CheckForTrigger(puzzleBoard.GetConnected(index), TriggerType.Connected), index);
        QueueEffects(CheckForTrigger(puzzleBoard.GetChain(index), TriggerType.Chain), index);
        if (puzzleBoard.InCombo(index))
        {
            List<PuzzlePiece> pieces = puzzleBoard.GetChain(index);
            pieces.Insert(0, puzzlePiece);
            QueueEffects(CheckForTrigger(pieces, TriggerType.Combo), index);
            List<int> indexes = puzzleBoard.GetChainIndex(index);
            indexes.Insert(0, index);
            DestroyPieces(indexes);
        }
        UpdateBoardPieceSprites();
        ActivateNextEffect();
    }

    /// <summary>
    /// Attempts to rotate the selected piece
    /// </summary>
    public void AttemptRotate()
    {
        if (combatState == CombatState.PieceSelected && selectedPuzzlePiece != null)
        {
            selectedPuzzlePiece.RotatePiece();
            SetMousePuzzle(selectedPuzzlePiece);
        }
    }

    public List<PuzzleEffect> CheckForTrigger(PuzzlePiece piece, TriggerType triggerType)
    {
        List<PuzzlePiece> pieces = new List<PuzzlePiece>();
        pieces.Add(piece);
        return CheckForTrigger(pieces, triggerType);
    }

    public List<PuzzleEffect> CheckForTrigger(List<PuzzlePiece> pieces, TriggerType triggerType)
    {
        List<PuzzleEffect> puzzleEffects = new List<PuzzleEffect>();
        foreach (PuzzlePiece piece in pieces)
        {
            foreach (PuzzleEffect effect in piece.GetEffects())
            {
                if (effect.GetTriggerType() == triggerType)
                {
                    puzzleEffects.Add(effect);
                }
            }
        }
        return puzzleEffects;
    }

    public void QueueEffects(List<PuzzleEffect> effects, int index)
    {
        foreach (PuzzleEffect effect in effects)
        {
            switch (effect.GetEffectType())
            {
                case EffectType.Damage:
                    QueueEffect(new EffectDamageEnemy(effect.GetTargetType(), CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
                case EffectType.Heal:
                    QueueEffect(new EffectHealPlayer(CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
                case EffectType.Shield:
                    QueueEffect(new EffectBuffPlayer(BuffID.Shield, CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
                case EffectType.Buff:
                    QueueEffect(new EffectBuffPlayer(effect.GetBuffID(), CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
                case EffectType.Debuff:
                    QueueEffect(new EffectBuffEnemy(effect.GetBuffID(), effect.GetTargetType(), CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
                case EffectType.ModifyPiece:
                    switch (effect.GetModificationType())
                    {
                        case ModificationType.Destroy:
                            QueueEffect(new EffectDestroyPiece(index, effect.GetTargetType(), effect.GetLimitPieceSelection() && effect.GetLimitTargetColor() ? effect.GetTargetColor() : null, CalculateRepetitions(effect, index)));
                            break;
                        case ModificationType.Color:
                            QueueEffect(new EffectColorPiece(index, effect.GetTargetType(), effect.GetLimitPieceSelection() && effect.GetLimitTargetColor() ? effect.GetTargetColor() : null, effect.GetNewColor(), CalculateRepetitions(effect, index)));
                            break;
                        case ModificationType.Shape:
                            QueueEffect(new EffectShapePiece(index, effect.GetTargetType(), effect.GetLimitPieceSelection() && effect.GetLimitTargetColor() ? effect.GetTargetColor() : null, effect.GetShapeChange(), CalculateRepetitions(effect, index)));
                            break;
                    }
                    break;
                case EffectType.SelfDamage:
                    QueueEffect(new EffectDamagePlayer(CalculateAmount(effect, index), CalculateRepetitions(effect, index)));
                    break;
            }
        }
    }

    public void QueueEffect(ActiveEffect effect)
    {
        effectQueue.Add(effect);
    }

    public void DestroyPiece(int index)
    {
        QueueEffects(CheckForTrigger(puzzleBoard.GetPuzzlePiece(index), TriggerType.Destroy), index);
        discardPile.AddPuzzlePiece(puzzleBoard.RemovePiece(index));
        UpdateBoardPieceSprites();
    }

    public void DestroyPieces(List<int> indexes)
    {
        foreach (int index in indexes)
        {
            DestroyPiece(index);
        }
    }

    public void ActivateNextEffect()
    {
        if (CheckBattleOver())
        {
            EndEncounter();
            if (CheckLose())
            {
                //mapManager.GameOver();
            }
            else if (CheckWin())
            {
                //mapManager.BattleOver();
            }
        }
        else if (effectQueue.Count == 0)
        {
            if (combatState == CombatState.DoingStuff)
            {
                SetCombatState(CombatState.PlayerTurn);
            }
            else if (combatState == CombatState.EnemyTurn)
            {
                EndEnemyTurn();
            }
        }
        else
        {
            ActiveEffect currentEffect = effectQueue[0];
            if (currentEffect.GetType() == typeof(EffectDamageEnemy))
            {
                EffectDamageEnemy effect = (EffectDamageEnemy)currentEffect;
                switch (effect.GetTargetType())
                {
                    case TargetType.Single:
                        if (GetValidEnemyTargets().Count == 1)
                        {
                            int times = 0;
                            int damage = effect.GetDamage();
                            int repetitions = effect.GetRepetitions();
                            Enemy enemy = GetValidEnemyTargets()[0];
                            while (enemy.ValidTarget() && times++ < repetitions)
                            {
                                DamageEnemy(enemy, damage);
                            }
                        }
                        else if (GetValidEnemyTargets().Count != 0)
                        {
                            StartTargeting();
                            return;
                        }
                        break;
                    case TargetType.Random:
                        if (GetValidEnemyTargets().Count > 0)
                        {
                            List<Enemy> validTargets = GetValidEnemyTargets();
                            int times = 0;
                            int damage = effect.GetDamage();
                            int repetitions = effect.GetRepetitions();
                            while (validTargets.Count > 0 && times++ < repetitions)
                            {
                                Enemy target = validTargets[Random.Range(0, validTargets.Count)];
                                DamageEnemy(target, damage);
                                validTargets = GetValidEnemyTargets();
                            }
                        }
                        break;
                    case TargetType.All:
                        List<Enemy> targetsAll = GetValidEnemyTargets();
                        int damageAll = effect.GetDamage();
                        int repetitionsAll = effect.GetRepetitions();
                        foreach (Enemy target in targetsAll)
                        {
                            int times = 0;
                            while (target.ValidTarget() && times++ < repetitionsAll)
                            {
                                DamageEnemy(target, damageAll);
                            }
                        }
                        break;
                }
            }
            else if (currentEffect.GetType() == typeof(EffectHealPlayer))
            {
                EffectHealPlayer effect = (EffectHealPlayer)currentEffect;
                int amount = effect.GetAmount();
                for (int i = 0; i < effect.GetRepetitions(); i++)
                {
                    playerManager.Heal(amount);
                }
            }
            else if (currentEffect.GetType() == typeof(EffectBuffPlayer))
            {
                EffectBuffPlayer effect = (EffectBuffPlayer)currentEffect;
                BuffID buff = effect.GetBuff();
                int amount = effect.GetAmount();
                for (int i = 0; i < effect.GetRepetitons(); i++)
                {
                    playerManager.ApplyBuff(buff, amount);
                }
            }
            else if (currentEffect.GetType() == typeof(EffectBuffEnemy))
            {
                EffectBuffEnemy effect = (EffectBuffEnemy)currentEffect;
                BuffID buff = effect.GetBuff();
                switch (effect.GetTargetType())
                {
                    case TargetType.Single:
                        if (GetValidEnemyTargets().Count == 1)
                        {
                            int times = 0;
                            int amount = effect.GetAmount();
                            int repetitions = effect.GetRepetitions();
                            Enemy enemy = GetValidEnemyTargets()[0];
                            while (enemy.ValidTarget() && times++ < repetitions)
                            {
                                BuffEnemy(enemy, buff, amount);
                            }
                        }
                        else if (GetValidEnemyTargets().Count != 0)
                        {
                            StartTargeting();
                            return;
                        }
                        break;
                    case TargetType.Random:
                        if (GetValidEnemyTargets().Count > 0)
                        {
                            List<Enemy> validTargets = GetValidEnemyTargets();
                            int times = 0;
                            int amount = effect.GetAmount();
                            int repetitions = effect.GetRepetitions();
                            while (validTargets.Count > 0 && times++ < repetitions)
                            {
                                Enemy target = validTargets[Random.Range(0, validTargets.Count)];
                                BuffEnemy(target, buff, amount);
                                validTargets = GetValidEnemyTargets();
                            }
                        }
                        break;
                    case TargetType.All:
                        List<Enemy> targetsAll = GetValidEnemyTargets();
                        int amountAll = effect.GetAmount();
                        int repetitionsAll = effect.GetRepetitions();
                        foreach (Enemy target in targetsAll)
                        {
                            int times = 0;
                            while (target.ValidTarget() && times++ < repetitionsAll)
                            {
                                BuffEnemy(target, buff, amountAll);
                            }
                        }
                        break;
                }
            }
            else if (currentEffect.GetType() == typeof(EffectDestroyPiece))
            {
                EffectDestroyPiece effect = (EffectDestroyPiece)currentEffect;
                if (effect.GetColorCondition() == null)
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets().Count == 1)
                            {
                                DestroyPiece(GetValidPuzzleTargets()[0]);
                            }
                            else if (GetValidPuzzleTargets().Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets().Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets();
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    DestroyPiece(target);
                                    validTargets = GetValidPuzzleTargets();
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets();
                            foreach (int target in targetsAll)
                            {
                                DestroyPiece(target);
                            }
                            break;
                    }
                }
                else
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count == 1)
                            {
                                DestroyPiece(GetValidPuzzleTargets(effect.GetColorCondition())[0]);
                            }
                            else if (GetValidPuzzleTargets(effect.GetColorCondition()).Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    DestroyPiece(target);
                                    validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets(effect.GetColorCondition());
                            foreach (int target in targetsAll)
                            {
                                DestroyPiece(target);
                            }
                            break;
                    }
                }
            }
            else if (currentEffect.GetType() == typeof(EffectColorPiece))
            {
                EffectColorPiece effect = (EffectColorPiece)currentEffect;
                if (effect.GetColorCondition() == null)
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets().Count == 1)
                            {
                                ChangePuzzleColor(GetValidPuzzleTargets()[0], effect.GetNewColor());
                            }
                            else if (GetValidPuzzleTargets().Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets().Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets();
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    ChangePuzzleColor(target, effect.GetNewColor());
                                    validTargets = GetValidPuzzleTargets();
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets();
                            foreach (int target in targetsAll)
                            {
                                ChangePuzzleColor(target, effect.GetNewColor());
                            }
                            break;
                    }
                }
                else
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count == 1)
                            {
                                ChangePuzzleColor(GetValidPuzzleTargets(effect.GetColorCondition())[0], effect.GetNewColor());
                            }
                            else if (GetValidPuzzleTargets(effect.GetColorCondition()).Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    ChangePuzzleColor(target, effect.GetNewColor());
                                    validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets(effect.GetColorCondition());
                            foreach (int target in targetsAll)
                            {
                                ChangePuzzleColor(target, effect.GetNewColor());
                            }
                            break;
                    }
                }
            }
            else if (currentEffect.GetType() == typeof(EffectShapePiece))
            {
                EffectShapePiece effect = (EffectShapePiece)currentEffect;
                if (effect.GetColorCondition() == null)
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets().Count == 1)
                            {
                                ChangePuzzleShape(GetValidPuzzleTargets()[0], effect.GetNewShape());
                            }
                            else if (GetValidPuzzleTargets().Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets().Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets();
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    ChangePuzzleShape(target, effect.GetNewShape());
                                    validTargets = GetValidPuzzleTargets();
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets();
                            foreach (int target in targetsAll)
                            {
                                ChangePuzzleShape(target, effect.GetNewShape());
                            }
                            break;
                    }
                }
                else
                {
                    switch (effect.GetTargetType())
                    {

                        case TargetType.Single:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count == 1)
                            {
                                ChangePuzzleShape(GetValidPuzzleTargets(effect.GetColorCondition())[0], effect.GetNewShape());
                            }
                            else if (GetValidPuzzleTargets(effect.GetColorCondition()).Count != 0)
                            {
                                StartTargeting();
                                return;
                            }
                            break;
                        case TargetType.Random:
                            if (GetValidPuzzleTargets(effect.GetColorCondition()).Count > 0)
                            {
                                List<int> validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                int times = 0;
                                int repetitions = effect.GetRepetitions();
                                while (validTargets.Count > 0 && times++ < repetitions)
                                {
                                    int target = validTargets[Random.Range(0, validTargets.Count)];
                                    ChangePuzzleShape(target, effect.GetNewShape());
                                    validTargets = GetValidPuzzleTargets(effect.GetColorCondition());
                                }
                            }
                            break;
                        case TargetType.All:
                            List<int> targetsAll = GetValidPuzzleTargets(effect.GetColorCondition());
                            foreach (int target in targetsAll)
                            {
                                ChangePuzzleShape(target, effect.GetNewShape());
                            }
                            break;
                    }
                }
            }
            else if (currentEffect.GetType() == typeof(EffectDamagePlayer))
            {
                EffectDamagePlayer effect = (EffectDamagePlayer)currentEffect;
                int damage = effect.GetDamage();
                for (int i = 0; i < effect.GetRepetitions(); i++)
                {
                    playerManager.Damage(damage);
                }
            }
            effectQueue.RemoveAt(0);
            ReloadEnemySprites();
            ActivateNextEffect();
        }
    }

    public void ReloadEnemySprites()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemySpriteManagers[i].SetSprite(enemies[i].GetSpriteIdle(), enemies[i].GetCurrentHealth(), enemies[i].GetMaxHealth(), enemies[i].GetNextAttack());
        }
    }

    public int CalculateAmount(PuzzleEffect effect, int index)
    {
        ValueType valueType = effect.GetAmount();
        if (valueType == ValueType.Constant)
        {
            return effect.GetAmountConstant();
        }
        else if (valueType == ValueType.Random)
        {
            return Random.Range(effect.GetAmountMin(), effect.GetAmountMax() + 1);
        }
        else if (valueType == ValueType.Variable)
        {
            int variable = 0;
            switch (effect.GetAmountVariableSource())
            {
                case ConditionSource.PuzzleBoard:
                    List<PuzzlePiece> pieces = new List<PuzzlePiece>();
                    switch (effect.GetAmountPuzzlePieceRange())
                    {
                        case PuzzlePieceRange.Connected:
                            pieces = puzzleBoard.GetConnected(index);
                            break;
                        case PuzzlePieceRange.Adjacent:
                            pieces = puzzleBoard.GetAdjacent(index);
                            break;
                        case PuzzlePieceRange.Chain:
                            pieces = puzzleBoard.GetChain(index);
                            break;
                        case PuzzlePieceRange.All:
                            pieces = puzzleBoard.GetAll();
                            break;
                    }
                    if (effect.GetAmountLimitColor() && effect.GetAmountColor() != null)
                    {
                        List<PuzzlePiece> limitedPieces = new List<PuzzlePiece>();
                        PuzzleColor testColor = effect.GetAmountColor();
                        foreach (PuzzlePiece piece in pieces)
                        {
                            if (piece.GetPuzzleColor() == testColor)
                            {
                                limitedPieces.Add(piece);
                            }
                        }
                        pieces = limitedPieces;
                    }
                    variable = pieces.Count;
                    break;
            }
            return effect.GetAmountVariableCoefficient() * variable + effect.GetAmountVariableConstant();
        }
        else
        {
            return 0;
        }
    }

    public int CalculateRepetitions(PuzzleEffect effect, int index)
    {
        if (effect.GetRepeats())
        {
            ValueType valueType = effect.GetRepetitions();
            if (valueType == ValueType.Constant)
            {
                return effect.GetRepetitionsConstant();
            }
            else if (valueType == ValueType.Random)
            {
                return Random.Range(effect.GetRepetitionsMin(), effect.GetRepetitionsMax() + 1);
            }
            else if (valueType == ValueType.Variable)
            {
                int variable = 0;
                switch (effect.GetRepetitionsVariableSource())
                {
                    case ConditionSource.PuzzleBoard:
                        List<PuzzlePiece> pieces = new List<PuzzlePiece>();
                        switch (effect.GetRepetitionsPuzzlePieceRange())
                        {
                            case PuzzlePieceRange.Connected:
                                pieces = puzzleBoard.GetConnected(index);
                                break;
                            case PuzzlePieceRange.Adjacent:
                                pieces = puzzleBoard.GetAdjacent(index);
                                break;
                            case PuzzlePieceRange.Chain:
                                pieces = puzzleBoard.GetChain(index);
                                break;
                            case PuzzlePieceRange.All:
                                pieces = puzzleBoard.GetAll();
                                break;
                        }
                        if (effect.GetRepetitionsLimitColor() && effect.GetRepetitionsColor() != null)
                        {
                            List<PuzzlePiece> limitedPieces = new List<PuzzlePiece>();
                            PuzzleColor testColor = effect.GetRepetitionsColor();
                            foreach (PuzzlePiece piece in pieces)
                            {
                                if (piece.GetPuzzleColor() == testColor)
                                {
                                    limitedPieces.Add(piece);
                                }
                            }
                            pieces = limitedPieces;
                        }
                        variable = pieces.Count;
                        break;
                }
                return effect.GetRepetitionsVariableCoefficient() * variable + effect.GetRepetitionsVariableConstant();
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 1;
        }
    }

    public void StartTargeting()
    {
        SetCombatState(CombatState.PickingTarget);
        SetMouseSprite(targetingSprite);
    }

    public List<Enemy> GetValidEnemyTargets()
    {
        List<Enemy> validTargets = new List<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.ValidTarget())
            {
                validTargets.Add(enemy);
            }
        }
        return validTargets;
    }

    public List<int> GetValidPuzzleTargets()
    {
        List<int> pieces = new List<int>();
        for (int i = 0; i < puzzleBoard.GetSize(); i++)
        {
            if (puzzleBoard.GetPuzzlePiece(i) != null) pieces.Add(i);
        }
        return pieces;
    }

    public List<int> GetValidPuzzleTargets(PuzzleColor color)
    {
        List<int> allPieces = GetValidPuzzleTargets();
        List<int> pieces = new List<int>();
        foreach (int piece in allPieces)
        {
            if (puzzleBoard.GetPuzzlePiece(piece).GetPuzzleColor() == color) pieces.Add(piece);
        }
        return pieces;
    }

    public void DamageEnemy(Enemy enemy, int damage)
    {
        enemy.Damage(damage);
        ReloadEnemySprites();
    }

    public void BuffEnemy(Enemy enemy, BuffID buff, int amount)
    {
        enemy.ApplyBuff(buff, amount);
    }

    public void ChangePuzzleColor(int index, PuzzleColor color)
    {
        puzzleBoard.GetPuzzlePiece(index).SetPuzzleColor(color);
        UpdateBoardPieceSprites();
    }

    public void ChangePuzzleShape(int index, ShapeChange shape)
    {
        switch (shape)
        {
            case ShapeChange.ConnectSurrounding:
                if (puzzleBoard.GetY(index) < 5 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) + 1) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetTop(), puzzleBoard.GetTopEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))
                {
                    PuzzleEdge edge = puzzleBoard.GetTopEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index));
                    if (edge == PuzzleEdge.Key)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetTopEdge(PuzzleEdge.Socket);
                    }
                    else if (edge == PuzzleEdge.Socket)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetTopEdge(PuzzleEdge.Key);
                    }
                }
                if (puzzleBoard.GetX(index) > 0 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) - 1, puzzleBoard.GetY(index)) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetLeft(), puzzleBoard.GetLeftEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))
                {
                    PuzzleEdge edge = puzzleBoard.GetLeftEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index));
                    if (edge == PuzzleEdge.Key)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetLeftEdge(PuzzleEdge.Socket);
                    }
                    else if (edge == PuzzleEdge.Socket)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetLeftEdge(PuzzleEdge.Key);
                    }
                }
                if (puzzleBoard.GetX(index) < 5 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) + 1, puzzleBoard.GetY(index)) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetRight(), puzzleBoard.GetRightEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))
                {
                    PuzzleEdge edge = puzzleBoard.GetRightEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index));
                    if (edge == PuzzleEdge.Key)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetRightEdge(PuzzleEdge.Socket);
                    }
                    else if (edge == PuzzleEdge.Socket)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetRightEdge(PuzzleEdge.Key);
                    }
                }
                if (puzzleBoard.GetY(index) > 0 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) - 1) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetBottom(), puzzleBoard.GetBottomEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))
                {
                    PuzzleEdge edge = puzzleBoard.GetBottomEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index));
                    if (edge == PuzzleEdge.Key)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetBottomEdge(PuzzleEdge.Socket);
                    }
                    else if (edge == PuzzleEdge.Socket)
                    {
                        puzzleBoard.GetPuzzlePiece(index).SetBottomEdge(PuzzleEdge.Key);
                    }
                }
                break;
            case ShapeChange.SmoothEdges:
                if (puzzleBoard.GetY(index) == 5 || puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) + 1) || (puzzleBoard.GetY(index) < 5 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) + 1) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetTop(), puzzleBoard.GetTopEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index)))))puzzleBoard.GetPuzzlePiece(index).SetTopEdge(PuzzleEdge.Blank);
                if (puzzleBoard.GetX(index) == 0 || puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) - 1, puzzleBoard.GetY(index)) || (puzzleBoard.GetX(index) > 0 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) - 1, puzzleBoard.GetY(index)) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetLeft(), puzzleBoard.GetLeftEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))) puzzleBoard.GetPuzzlePiece(index).SetLeftEdge(PuzzleEdge.Blank);
                if (puzzleBoard.GetX(index) == 5 || puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) + 1, puzzleBoard.GetY(index)) || (puzzleBoard.GetX(index) < 5 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index) + 1, puzzleBoard.GetY(index)) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetRight(), puzzleBoard.GetRightEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))) puzzleBoard.GetPuzzlePiece(index).SetRightEdge(PuzzleEdge.Blank);
                if (puzzleBoard.GetY(index) == 0 || puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) - 1) || (puzzleBoard.GetY(index) > 0 && !puzzleBoard.LocationEmpty(puzzleBoard.GetX(index), puzzleBoard.GetY(index) - 1) && !puzzleBoard.EdgesAreConnected(puzzleBoard.GetPuzzlePiece(index).GetBottom(), puzzleBoard.GetBottomEdge(puzzleBoard.GetX(index), puzzleBoard.GetY(index))))) puzzleBoard.GetPuzzlePiece(index).SetBottomEdge(PuzzleEdge.Blank);

                if (puzzleBoard.InCombo(index))
                {
                    List<PuzzlePiece> pieces = puzzleBoard.GetChain(index);
                    pieces.Insert(0, puzzleBoard.GetPuzzlePiece(index));
                    QueueEffects(CheckForTrigger(pieces, TriggerType.Combo), index);
                    List<int> indexes = puzzleBoard.GetChainIndex(index);
                    indexes.Insert(0, index);
                    DestroyPieces(indexes);
                }
                break;
        }
        UpdateBoardPieceSprites();
    }

    public void EndTargeting()
    {
        ReloadEnemySprites();
        UpdateBoardPieceSprites();
        if (playerTurn) SetCombatState(CombatState.DoingStuff);
        else if (enemyTurn) SetCombatState(CombatState.EnemyTurn);
        ClearMouseImage();
        ActivateNextEffect();
    }

    public void DoEnemyAttacks()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.Alive())
            {
                DoEnemyAttack(enemy);
            }
        }
        ActivateNextEffect();
    }

    public void DoEnemyAttack(Enemy enemy)
    {
        PuzzlePiece enemyAttack = new PuzzlePiece(enemy.GetNextAttack());
        List<int> validLocations = new List<int>();
        for (int i = 0; i < puzzleBoard.GetSize(); i++)
        {
            if (puzzleBoard.PieceCanFit(i, enemyAttack))
            {
                validLocations.Add(i);
            }
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(i, enemyAttack) && !validLocations.Contains(i))
            {
                validLocations.Add(i);
            }
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(i, enemyAttack) && !validLocations.Contains(i))
            {
                validLocations.Add(i);
            }
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(i, enemyAttack) && !validLocations.Contains(i))
            {
                validLocations.Add(i);
            }
            enemyAttack.RotatePiece();
        }
        if (validLocations.Count != 0)
        {
            int index = validLocations[Random.Range(0, validLocations.Count)];
            List<int> validRotations = new List<int>();
            if (puzzleBoard.PieceCanFit(index, enemyAttack)) validRotations.Add(0);
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(index, enemyAttack)) validRotations.Add(1);
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(index, enemyAttack)) validRotations.Add(2);
            enemyAttack.RotatePiece();
            if (puzzleBoard.PieceCanFit(index, enemyAttack)) validRotations.Add(3);
            enemyAttack.RotatePiece();
            int rotations = validRotations[Random.Range(0, validRotations.Count)];
            for (int i = 0; i < rotations; i++)
            {
                enemyAttack.RotatePiece();
            }
            PlacePiece(enemyAttack, index);
        }
    }

    public void HideEndButton()
    {
        endButton.UnloadSprites();
    }

    public void SetButtonEnd()
    {
        endButton.SetSprite(buttonEndSprite);
    }

    public void SetButtonCancel()
    {
        endButton.SetSprite(buttonCancelSprite);
    }

    public bool CheckBattleOver()
    {
        return CheckLose() || CheckWin();
    }

    public bool CheckLose()
    {
        return playerManager.GetHealth() <= 0;
    }

    public bool CheckWin()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.Alive()) return false;
        }
        return true;
    }
}
