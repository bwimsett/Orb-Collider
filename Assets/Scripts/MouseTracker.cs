using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseTracker : MonoBehaviour {

    [SerializeField] private Camera camera;
    private Marble currentMarble;
    
    void Update() {
        RaycastForMarbles();
        OnMouseDown();
        OnMouseUp();
    }

    private void RaycastForMarbles() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider) {
            Marble newMarble= hit.collider.gameObject.GetComponent<Marble>();

            if (newMarble && newMarble != currentMarble) {
                if (currentMarble) {
                    currentMarble.MouseExit();
                }

                currentMarble = newMarble;
                currentMarble.MouseEnter();
            }
            
            return;
        }

        if (currentMarble) {
            currentMarble.MouseExit();
        }
        
        currentMarble = null;
    }

    private void OnMouseDown() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }
        
        if (currentMarble) {
            currentMarble.MouseDown();
        }
    }

    private void OnMouseUp() {
        if (!Input.GetMouseButtonUp(0)) {
            return;
        }

        if (currentMarble) {
            currentMarble.MouseUp();
        }
        
        GameManager.levelManager.CloseCurrentTether();
    }
    
}
