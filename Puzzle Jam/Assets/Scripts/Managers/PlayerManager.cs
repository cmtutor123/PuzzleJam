using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages character data
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] private CharacterData characterData;

    private PuzzlePile puzzleDeck;

    private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        puzzleDeck = new PuzzlePile();
        puzzleDeck.AddPuzzlePieces(characterData.GetStartingPuzzlePieces());
        maxHealth = characterData.GetHealth();
        currentHealth = maxHealth;
    }

    /// <returns>A list of PuzzlePiece objects generated from the player's current deck PuzzlePile</returns>
    public List<PuzzlePiece> GetPuzzleDeck()
    {
        return puzzleDeck.GetPuzzlePieces();
    }

    /// <returns>The puzzle board sprite from CharacterData</returns>
    public Sprite GetPuzzleBoardSprite()
    {
        return characterData.GetSpritePuzzleBoard();
    }
    
    /// <returns>The player's current health</returns>
    public int GetHealth()
    {
        return currentHealth;
    }

    /// <returns>The player's max health</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Heals the player
    /// </summary>
    /// <param name="amount">The amount of health to heal</param>
    public void Heal(int amount)
    {
        amount = Mathf.Clamp(amount, 0, maxHealth - currentHealth);
        currentHealth += amount;
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <param name="amount">The amount of damage to take</param>
    public void Damage(int amount)
    {
        amount = Mathf.Clamp(amount, 0, currentHealth);
        currentHealth -= amount;
    }
}
