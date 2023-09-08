using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    // changes the tooltip text
    public void SetText(string nameText, string descriptionText)
    {
        tooltipName.text = nameText;
        tooltipDescription.text = descriptionText;
    }

    // changes the tooltip Sprites
    public override void SetSprite(Sprite sprite)
    {
        if (sprite != null) image.sprite = sprite;
        else image.sprite = emptySprite;
        backgroundRenderer.sprite = backgroundSprite;
    }

    // hides the text and images
    public override void UnloadSprites()
    {
        tooltipName.text = string.Empty;
        tooltipDescription.text = string.Empty;
        image.sprite = emptySprite;
        backgroundRenderer.sprite = emptySprite;
    }
}
