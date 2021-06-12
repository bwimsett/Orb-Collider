using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace {
    public class Marble : MonoBehaviour {

        [SerializeField] private CircleCollider2D collider;
        private Tether tether;
        public float moveSpeed;
        private Tween currentTween;
        protected bool moving;

        void Awake() {
            collider = GetComponent<CircleCollider2D>();
        }

        public void OnMouseEnter() {
            tether = GameManager.levelManager.currentTether;

            if (tether && tether.rootMarble) {
                tether.SetEndMarble(this);
            }
        }

        public void OnMouseExit() {
            if (tether && tether.endMarble == this) {
                tether.SetEndMarble(null);
            }
        }

        public void OnMouseDown() {
            Tether tether = GameManager.levelManager.GetTether();
            tether.SetRootMarble(this);
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

        public void Destroy() {
            Stop();
            Destroy(gameObject);
        }

        public void Stop() {
            currentTween.onComplete.Invoke();
            currentTween.Kill();
            currentTween = null;
        }

        public void PullTowards(Marble marble, TweenCallback onComplete) {
            float moveDistance = (marble.transform.position - transform.position).magnitude;
            moving = true;
            currentTween = transform.DOMove(marble.transform.position, moveDistance * (1 / moveSpeed)).OnComplete(()=> {
                moving = false;
                onComplete.Invoke();
            });
        }
    }
}