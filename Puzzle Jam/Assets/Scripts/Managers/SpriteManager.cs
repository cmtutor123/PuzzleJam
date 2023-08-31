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

    public virtual void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public virtual void UnloadSprites()
    {
        spriteRenderer.sprite = null;
    }
}
