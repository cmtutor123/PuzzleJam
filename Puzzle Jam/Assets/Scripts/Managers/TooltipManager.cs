using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : SpriteManager
{
    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI tooltipName;
    [SerializeField] private TextMeshProUGUI tooltipDescription;
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [Header("Sprite")]
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
        base.SetSprite(sprite);
        backgroundRenderer.sprite = backgroundSprite;
    }

    // hides the text and images
    public override void UnloadSprites()
    {
        base.UnloadSprites();
        tooltipName.text = string.Empty;
        tooltipDescription.text = string.Empty;
        backgroundRenderer.sprite = null;
    }
}
