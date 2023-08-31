using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // sets the SpriteRenderer's Sprite to the specified Sprite
    public virtual void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // sets the SpriteRenderer's Sprite to null
    public virtual void UnloadSprites()
    {
        spriteRenderer.sprite = null;
    }
}
