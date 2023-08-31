using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TooltipManager : SpriteManager
{
    private TextMeshProUGUI tooltip;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite backgroundSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tooltip = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string tooltipText)
    {
        tooltip.text = tooltipText;
    }

    public override void SetSprite(Sprite sprite)
    {
        base.SetSprite(sprite);
        backgroundRenderer.sprite = backgroundSprite;
    }

    public override void UnloadSprites()
    {
        base.UnloadSprites();
        tooltip.text = string.Empty;
        backgroundRenderer.sprite = null;
    }
}
