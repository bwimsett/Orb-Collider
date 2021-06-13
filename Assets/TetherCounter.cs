using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TetherCounter : MonoBehaviour {

    public Sprite[] numberSprites;
    public SpriteRenderer iconSpriteRenderer, timesSpriteRenderer, numberSpriteRenderer;
    public Color redColor;
    public Color defaultColor;
    public float colorFlashDuration;
    public float colorFlashScale;
    private int currentCount = 0;
    
    
    public void SetCount(int count) {
        if (count == currentCount) {
            return;
        }

        currentCount = count;
        numberSpriteRenderer.sprite = numberSprites[currentCount];
        FlashSpriteRenderer(numberSpriteRenderer, true);
    }

    public void Flash() {
        FlashSpriteRenderer(iconSpriteRenderer, true);
        FlashSpriteRenderer(timesSpriteRenderer, true);
        FlashSpriteRenderer(numberSpriteRenderer, true);
    }

    private void FlashSpriteRenderer(SpriteRenderer spriteRenderer, bool scale) {
        spriteRenderer.color = redColor;
        spriteRenderer.DOColor(defaultColor, colorFlashDuration);
        if (scale) {
            spriteRenderer.transform.localScale = new Vector3(colorFlashScale, colorFlashScale, 1f);
            spriteRenderer.transform.DOScale(new Vector3(1f, 1f, 1f), colorFlashDuration);
        }
    }

}
