using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TooltipManager : SpriteManager
{
    private TextMeshProUGUI tooltip;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tooltip = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string tooltipText)
    {
        tooltip.text = tooltipText;
    }
}
