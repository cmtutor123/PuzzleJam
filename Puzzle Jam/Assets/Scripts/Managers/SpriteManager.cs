using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager : MonoBehaviour
{
    protected float transitionTimer, transitionTime;
    protected Vector2 startPosition, endPosition;
    protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = new Vector2();
        endPosition = new Vector2();
    }

    private void Update()
    {
        transitionTimer += Time.deltaTime;
        transform.position = new Vector2(Mathf.Lerp(startPosition.x, endPosition.y, transitionTimer), Mathf.Lerp(startPosition.y, endPosition.y, transitionTimer));
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void LerpPositions(Vector2 startPosition, Vector2 endPosition, float transitionTime = 0.5f)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.transitionTime = transitionTime;
        transitionTimer = 0;
    }
}
