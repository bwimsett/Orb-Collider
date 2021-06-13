using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        
        public static LevelManager levelManager;
        public static GameManager gameManager;
        public static Camera mainCamera;

        public float cameraShakeDuration, cameraShakeStrength, cameraShakeRandomness;
        public int cameraShakeVibrato;

        void Awake() {
            GameManager.levelManager = GetComponent<LevelManager>();
            GameManager.gameManager = this;
            GameManager.mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        public void ShakeCamera() {
            mainCamera.transform.DOShakePosition(cameraShakeDuration, cameraShakeStrength, cameraShakeVibrato, cameraShakeRandomness);
        }

        
        
    }
}