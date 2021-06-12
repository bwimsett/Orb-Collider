using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class LevelManager : MonoBehaviour {
        
        [SerializeField] private GameObject tetherPrefab;
        [SerializeField] private Transform levelArea;
        public LevelSO[] levels;
        private int currentLevel;
        private GameObject currentLevelGO;
        public Tether currentTether;
        private int tethersUsed;

        [SerializeField] private TextMeshProUGUI tetherCountText;

        void Awake() {
            currentLevel = 0;
            LoadLevel();
        }
        
        public Tether GetTether() {
            if (currentTether) {
                Destroy(currentTether.gameObject);
            }
            
            if (tethersUsed >= levels[currentLevel].tetherCount) {
                return null;
            }
            
            currentTether = Instantiate(tetherPrefab, levelArea).GetComponent<Tether>();
            return currentTether;
        }

        public void CloseCurrentTether() {
            if (!currentTether) {
                return;
            }

            bool tetherUsed = currentTether.Close();

            if (tetherUsed) {
                tethersUsed++;
            }
            
            RefreshTetherCountDisplay();
        }

        public void ResetLevel() {
            LoadLevel();
        }

        private void RefreshTetherCountDisplay() {
            int tethersRemaining = levels[currentLevel].tetherCount - tethersUsed;
            tetherCountText.text = "" + tethersRemaining;
        }

        public void CompleteLevel() {
            currentLevel++;

            if (currentLevel < levels.Length) {
                LoadLevel();
            } else {
                currentLevel = levels.Length - 1;
            }
        }

        private void LoadLevel() {
            if (currentLevelGO) {
                Destroy(currentLevelGO);
            }

            currentLevelGO = Instantiate(levels[currentLevel].levelPrefab, levelArea);
            tethersUsed = 0;
            RefreshTetherCountDisplay();
        }
        
    }
}