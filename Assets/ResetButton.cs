using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : MonoBehaviour {

    private bool mouseDown;
    public SpriteRenderer SpriteRenderer;
    public Color mouseOverColor, mouseOutColor, mouseDownColor;
    public UnityEvent onMouseUp;
    
    
    private void OnMouseEnter() {
        SpriteRenderer.DOColor(mouseOverColor, 0.05f);
    }

    private void OnMouseExit() {
        SpriteRenderer.DOColor(mouseOutColor, 0.05f);
        mouseDown = false;
    }

    private void OnMouseDown() {
        mouseDown = true;
        SpriteRenderer.color = mouseDownColor;
    }

    private void OnMouseUp() {
        SpriteRenderer.color = mouseOverColor;
        
        if (!mouseDown) {
            return;
        }
        
        onMouseUp.Invoke();
    }
}
