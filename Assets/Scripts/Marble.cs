using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace {
    public class Marble : MonoBehaviour {

        [SerializeField] private Collider2D collider;
        private Tether tether;
        public float moveSpeed;
        private Tween currentTween, growShrinkTween;
        protected bool moving;
        public Level level;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] public Color defaultColor;
        private bool mouseOver;
        
        void Awake() {
            collider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = defaultColor;
        }

        public void MouseEnter() {
            tether = GameManager.levelManager.currentTether;
            mouseOver = true;
            
            // Animate grow and shrink on mouse enter
            if (!tether) {
                growShrinkTween?.Kill();
                growShrinkTween = transform.DOScale(new Vector3(1.15f, 1.15f), 0.1f);
            }

            if (tether && tether.rootMarble) {
                tether.SetEndMarble(this);
            }
        }

        public void MouseExit() {
            mouseOver = false;
            
            if (!tether) {
                growShrinkTween?.Kill();
                growShrinkTween = transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            }
            
            if (tether && tether.endMarble == this) {
                tether.SetEndMarble(null);
            }
        }

        public void RemoveTether() {
            if (!mouseOver) {
                growShrinkTween?.Kill();
                growShrinkTween = transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            }

            tether = null;
        }

        public virtual void MouseDown() {
            tether = GameManager.levelManager.GetTether();

            if (!tether) {
                GameManager.levelManager.tetherCounter.Flash();
                return;
            }
            
            tether.SetRootMarble(this);
        }

        public virtual void MouseUp() {
            
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag.Equals("Level")) {
                Stop();
                return;
            }
            
            Marble marble = other.GetComponent<Marble>();

            if (marble) {
                OnCollisionWithMarble(marble);
            }
        }

        protected virtual void OnCollisionWithMarble(Marble marble) {
            if (moving) {
                marble.Destroy();
            }
        }

        protected virtual void OnDestroy() {
            
        }
        
        public void Destroy() {
            Stop();
            OnDestroy();
            Destroy(gameObject);
            _spriteRenderer.DOFade(0, 0.3f).OnComplete(()=>Destroy(gameObject));
            GameManager.gameManager.ShakeCamera();
            GameManager.levelManager.CreateExplosion(this);
            level.RemoveMarble(this);
        }

        public void Stop() {
            currentTween?.onComplete?.Invoke();
            currentTween?.Kill();
            currentTween = null;
        }

        public void PullTowards(Marble marble, TweenCallback onComplete) {
            float moveDistance = (marble.transform.position - transform.position).magnitude;
            moving = true;
            currentTween = transform.DOMove(marble.transform.position, moveDistance * (1 / moveSpeed)).OnComplete(()=> {
                moving = false;
                onComplete.Invoke();
            });
            
            RemoveTether();
        }
    }
}