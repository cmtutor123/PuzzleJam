using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the Tooltip
/// </summary>
public class EnemySpriteManager : SpriteManager
{
    [Header("Sprite")]
    [SerializeField] private Sprite emptySprite;
    [Header("Puzzle Piece")]
    [SerializeField] private PuzzleRenderer puzzleRenderer;
    [Header("Health Bar")]
    [SerializeField] private HealthBarManager healthBarManager;

    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
        UnloadSprites();
    }

    /// <summary>
    /// Sets the sprites of the enemy, enemy health, and enemy attack
    /// </summary>
    /// <param name="sprite">The Sprite to change to</param>
    /// <param name="currentHealth">The enemy's current health</param>
    /// <param name="maxHealth">The enemy's max health</param>
    /// <param name="attack">The PuzzlePiece that is the enemy's next attack</param>
    public void SetSprite(Sprite sprite, int currentHealth, int maxHealth, PuzzlePiece attack)
    {
        base.SetSprite(sprite);
        if (attack != null)
        {
            puzzleRenderer.UpdateSprites(attack);
        }
        else
        {
            puzzleRenderer.UnloadSprites();
        }
        if (maxHealth <= 0)
        {
            healthBarManager.HideHealthBar();
        }
        else
        {
            healthBarManager.SetHealth(currentHealth, maxHealth);
            healthBarManager.ShowHealthBar();
        }
    }

    /// <summary>
    /// Sets the sprites of the enemy, enemy health, and enemy attack
    /// </summary>
    /// <param name="sprite">The Sprite to change to</param>
    /// <param name="currentHealth">The enemy's current health</param>
    /// <param name="maxHealth">The enemy's max health</param>
    /// <param name="attack">The PuzzleData that is the enemy's next attack</param>
    public void SetSprite(Sprite sprite, int currentHealth, int maxHealth, PuzzleData attack)
    {
        PuzzlePiece newPuzzlePiece = null;
        if (attack != null) newPuzzlePiece = new PuzzlePiece(attack);
        SetSprite(sprite, currentHealth, maxHealth, newPuzzlePiece);
    }

    /// <summary>
    /// Hides the sprites association with the enemy
    /// </summary>
    public override void UnloadSprites()
    {
        spriteRenderer.sprite = emptySprite;
        healthBarManager.HideHealthBar();
        puzzleRenderer.UnloadSprites();
    }
}
