using UnityEngine;

namespace DefaultNamespace {
    [CreateAssetMenu(fileName = "Level", menuName = "Level")]
    public class LevelSO : ScriptableObject {

        public GameObject levelPrefab;
        public int tetherCount;

    }
}