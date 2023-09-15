using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages a health bar for the player or an enemy
/// </summary>
public class HealthBarManager : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private Image background;
    [Header("Sprites")]
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite backgroundSprite;
    [Header("Colors")]
    [SerializeField] private Color healthColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private Color healColor;
    [Header("Animation Values")]
    [SerializeField] private float chipSpeed;

    private float lerpTimer = 0;

    private int maxHealth;
    private int currentHealth;

    void Update()
    {
        UpdateHealthBarUI();
    }

    /// <summary>
    /// Sets the current health
    /// </summary>
    /// <param name="currentHealth">The value for the current health</param>
    public void SetHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        lerpTimer = 0;
    }

    /// <summary>
    /// Sets the current health and the max health
    /// </summary>
    /// <param name="currentHealth">The value for the current health</param>
    /// <param name="maxHealth">The value for the max health</param>
    public void SetHealth(int currentHealth, int maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }

    /// <summary>
    /// Sets the max health
    /// </summary>
    /// <param name="maxHealth">The value for the max health</param>
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    /// <summary>
    /// Updates the ui of the health bar
    /// </summary>
    public void UpdateHealthBarUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction;
        if (maxHealth <= 0 || currentHealth <= 0) hFraction = 0;
        else hFraction = currentHealth / maxHealth;
        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = damageColor;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if (fillFront < hFraction)
        {
            backHealthBar.color = healColor;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    /// <summary>
    /// Hides the health bar ui
    /// </summary>
    public void HideHealthBar()
    {
        background.sprite = emptySprite;
        backHealthBar.sprite = emptySprite;
        frontHealthBar.sprite = emptySprite;
    }

    /// <summary>
    /// Shows the health bar ui
    /// </summary>
    public void ShowHealthBar()
    {
        background.sprite = backgroundSprite;
        background.color = backgroundColor;
        backHealthBar.sprite = fullSprite;
        backHealthBar.color = healthColor;
        frontHealthBar.sprite = fullSprite;
        frontHealthBar.color = healthColor;
    }
}
