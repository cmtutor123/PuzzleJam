using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the sprite of an object
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager : MonoBehaviour
{
    protected Image spriteRenderer;
    [SerializeField] protected Sprite empty;

    private void Start()
    {
        //if (this.GetType() != typeof(TooltipManager)) spriteRenderer = GetComponent<Image>();
        spriteRenderer = GetComponent<Image>();
        UnloadSprites();
    }

    /// <summary>
    /// Sets the SpriteRenderer's Sprite
    /// </summary>
    /// <param name="sprite">The Sprite to change to</param>
    public virtual void SetSprite(Sprite sprite)
    {
        if (sprite != null) spriteRenderer.sprite = sprite;
        else spriteRenderer.sprite = empty;
    }

    /// <summary>
    /// Sets the SpriteRenderer's Sprite to empty
    /// </summary>
    public virtual void UnloadSprites()
    {
        spriteRenderer.sprite = empty;
    }
}
