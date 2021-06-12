using System.Security.Cryptography;
using UnityEngine;

namespace DefaultNamespace {
    public class LevelManager : MonoBehaviour {
        
        [SerializeField] private GameObject tetherPrefab;
        [SerializeField] private Transform levelArea;
        public Tether currentTether;
        
        public Tether GetTether() {
            if (currentTether) {
                Destroy(currentTether.gameObject);
            }
            
            currentTether = Instantiate(tetherPrefab, levelArea).GetComponent<Tether>();
            return currentTether;
        }

        public void CloseCurrentTether() {
            if (!currentTether) {
                return;
            }

            currentTether.Close();
        }
        
    }
}