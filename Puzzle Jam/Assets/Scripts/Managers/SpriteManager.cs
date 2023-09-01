using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager : MonoBehaviour
{
    protected Image spriteRenderer;
    [SerializeField] protected Sprite empty;

    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    // sets the SpriteRenderer's Sprite to the specified Sprite
    public virtual void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // sets the SpriteRenderer's Sprite to null
    public virtual void UnloadSprites()
    {
        spriteRenderer.sprite = empty;
    }
}
