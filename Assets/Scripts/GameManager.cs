using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        
        public static LevelManager levelManager;
        public static Camera mainCamera;

        void Awake() {
            GameManager.levelManager = GetComponent<LevelManager>();
            GameManager.mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

    }
}