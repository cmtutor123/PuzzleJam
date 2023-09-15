using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the Tooltip
/// </summary>
public class TooltipManager : SpriteManager
{
    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI tooltipName;
    [SerializeField] private TextMeshProUGUI tooltipDescription;
    [Header("Images")]
    [SerializeField] private Image image;
    [SerializeField] private Image backgroundRenderer;
    [Header("Sprite")]
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite backgroundSprite;

    private void Start()
    {
        UnloadSprites();
    }

    /// <summary>
    /// Changes the tooltip text
    /// </summary>
    /// <param name="nameText">What to change the name to</param>
    /// <param name="descriptionText">What to change the description to</param>
    public void SetText(string nameText, string descriptionText)
    {
        tooltipName.text = nameText;
        tooltipDescription.text = descriptionText;
    }

    /// <summary>
    /// Changes the tooltip Sprites
    /// </summary>
    /// <param name="sprite">Sprite to change to</param>
    public override void SetSprite(Sprite sprite)
    {
        if (sprite != null) image.sprite = sprite;
        else image.sprite = emptySprite;
        backgroundRenderer.sprite = backgroundSprite;
    }

    /// <summary>
    /// Hides the text and images of the tooltip
    /// </summary>
    public override void UnloadSprites()
    {
        tooltipName.text = string.Empty;
        tooltipDescription.text = string.Empty;
        image.sprite = emptySprite;
        backgroundRenderer.sprite = emptySprite;
    }
}
