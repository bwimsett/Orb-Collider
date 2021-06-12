using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Level : MonoBehaviour {
    
    public Marble[] marbles;
    private int marbleCount;

    void Awake() {
        marbleCount = marbles.Length;
        foreach (Marble marble in marbles) {
            marble.level = this;
        }
    }

    public void RemoveMarble(Marble marble) {
        for (int i = 0; i < marbles.Length; i++) {
            if (marbles[i] == marble) {
                marbles[i] = null;
                marbleCount--;
                break;
            }
        }

        if (marbleCount == 0) {
            CompleteLevel();    
        }
    }

    private void CompleteLevel() {
        GameManager.levelManager.CompleteLevel();
    }

}
