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

    [Header("Health Bar")]
    [SerializeField] private HealthBarManager playerHealthBar;

    [Header("Audio")]
    [SerializeField] private AudioClip playerDamaged;

    private PuzzlePile puzzleDeck;

    private Dictionary<BuffID, int> buffs;

    private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        puzzleDeck = new PuzzlePile();
        buffs = new Dictionary<BuffID, int>();
        puzzleDeck.AddPuzzlePieces(characterData.GetStartingPuzzlePieces());
        maxHealth = characterData.GetHealth();
        currentHealth = maxHealth;
        UpdateHealthBar();
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
        UpdateHealthBar();
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <param name="amount">The amount of damage to take</param>
    public void Damage(int amount)
    {
        if (HasBuff(BuffID.Shield))
        {
            if (GetBuffAmount(BuffID.Shield) > amount)
            {
                RemoveBuff(BuffID.Shield, amount);
                amount = 0;
            }
            else
            {
                amount -= GetBuffAmount(BuffID.Shield);
                RemoveAllBuff(BuffID.Shield);
            }
        }
        amount = Mathf.Clamp(amount, 0, currentHealth);
        currentHealth -= amount;
        AudioSource.PlayClipAtPoint(playerDamaged,Camera.main.transform.position);
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the health bar ui
    /// </summary>
    public void UpdateHealthBar()
    {
        playerHealthBar.SetHealth(currentHealth, maxHealth);
    }

    /// <summary>
    /// Shows the health bar ui
    /// </summary>
    public void ShowHealthBar()
    {
        playerHealthBar.ShowHealthBar();
    }

    /// <summary>
    /// Hides the health bar ui
    /// </summary>
    public void HideHealthBar()
    {
        playerHealthBar.HideHealthBar();
    }

    public void ApplyBuff(BuffID buff, int amount)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] += amount;
        }
        else
        {
            buffs.Add(buff, amount);
        }
    }

    public void RemoveBuff(BuffID buff, int amount)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] -= amount;
            if (buffs[buff] < 0) buffs[buff] = 0;
        }
        else
        {
            buffs.Add(buff, 0);
        }
    }

    public void RemoveAllBuff(BuffID buff)
    {
        if (buffs.ContainsKey(buff))
        {
            buffs[buff] = 0;
        }
        else
        {
            buffs.Add(buff, 0);
        }
    }

    public bool HasBuff(BuffID buff)
    {
        return buffs.ContainsKey(buff) && buffs[buff] > 0;
    }

    public int GetBuffAmount(BuffID buff)
    {
        if (HasBuff(buff)) return buffs[buff];
        else return 0;
    }

    public void AddToDeck(PuzzlePiece piece)
    {
        puzzleDeck.AddPuzzlePiece(piece);
    }
}
