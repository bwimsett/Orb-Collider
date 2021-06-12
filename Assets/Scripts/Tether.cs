using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Tether : MonoBehaviour {

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float fadeDuration;
    public Marble rootMarble, endMarble;
    private Vector3 endPosition;
    private bool closed, blockedByLevel;

    private void Awake() {
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.positionCount = 2;
    }

    void Update() {
        if (!rootMarble){
            return;
        }
        
        if (endMarble || closed) {
            RaycastToEndPosition();
            return;
        } 
        
        // Track mouse position with other end
        endPosition = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastToEndPosition();
    }
    
    public void SetRootMarble(Marble target) {
        rootMarble = target;

        Vector3 targetPosition = rootMarble.transform.position;

        _lineRenderer.SetPosition(0, targetPosition);
        
        Update();
    }

    public void SetEndMarble(Marble target) {
        endMarble = target;

        if (endMarble == null || endMarble == rootMarble) {
            endMarble = null;
            return;
        }

        endPosition = endMarble.transform.position;
        _lineRenderer.SetPosition(1, endPosition);
    }

    public bool Close() {
        RaycastToEndPosition();

        closed = true;

        GameManager.levelManager.currentTether = null;

        if (!endMarble || blockedByLevel) {
            Destroy();
            return false;
        }
        
        rootMarble.PullTowards(endMarble, Destroy);
        return true;
    }

    private void RaycastToEndPosition() {
        LayerMask levelLayer = LayerMask.GetMask("Level");
        RaycastHit2D hit = Physics2D.Linecast(rootMarble.transform.position, endPosition, levelLayer.value);

        if (hit) {
            if (hit.collider.CompareTag("Level")) {
                endPosition = hit.point;
                blockedByLevel = true;
                RefreshLineEnd();
                return;
            }
        }

        blockedByLevel = false;
        RefreshLineEnd();
    }

    private void RefreshLineEnd() {
        _lineRenderer.SetPosition(1, endPosition);
    }

    private void Destroy() {
        Color original = _lineRenderer.startColor;
        Color faded = new Color(original.r, original.g, original.b, 0);

        DOTween.To(() => _lineRenderer.endColor, (value) => _lineRenderer.endColor = value, faded, fadeDuration);
        DOTween.To(()=>_lineRenderer.startColor, (value)=>_lineRenderer.startColor = value, faded, fadeDuration).OnComplete(() => {
            Destroy(gameObject);
            if (GameManager.levelManager.currentTether == this) {
                GameManager.levelManager.currentTether = null;
            }
        });
        
    }
    
}
