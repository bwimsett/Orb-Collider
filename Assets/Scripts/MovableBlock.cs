using System;
using UnityEngine;

namespace DefaultNamespace {
    public class MovableBlock : Marble {
        private bool dragging;
        private Vector2 startPos, mouseStartPos;
        private GameObject collidingGameObject;

        void Update() {
            if (dragging) {
                Vector2 mousePos = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouseDifference = mousePos - mouseStartPos;
                transform.position = startPos + mouseDifference;
            }
        }
        
        public override void MouseDown() {
            dragging = true;
            mouseStartPos = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            startPos = transform.position;
        }

        public override void MouseUp() {
            dragging = false;
            if (collidingGameObject) {
                transform.position = startPos;
                collidingGameObject = null;
                _spriteRenderer.color = defaultColor;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (dragging) {
                collidingGameObject = other.gameObject;
                Color newColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.5f);
                _spriteRenderer.color = newColor;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (collidingGameObject == other.gameObject) {
                collidingGameObject = null;
                _spriteRenderer.color = defaultColor;
            }
        }
    }
}