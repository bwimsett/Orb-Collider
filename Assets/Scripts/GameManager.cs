using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        
        public static LevelManager levelManager;
        public static GameManager gameManager;
        public static GUICover guiCover;
        public static Camera mainCamera;

        public float cameraShakeDuration, cameraShakeStrength, cameraShakeRandomness;
        public int cameraShakeVibrato;

        void Awake() {
            GameManager.levelManager = GetComponent<LevelManager>();
            GameManager.gameManager = this;
            GameManager.mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            GameManager.guiCover = GameObject.Find("GUI Cover").GetComponent<GUICover>();
        }

        public void ShakeCamera() {
            mainCamera.transform.DOShakePosition(cameraShakeDuration, cameraShakeStrength, cameraShakeVibrato, cameraShakeRandomness);
        }

        
        
    }
}