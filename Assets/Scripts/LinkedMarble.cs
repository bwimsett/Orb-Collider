using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LinkedMarble : Marble {
    
    [SerializeField] private LinkedMarble sibling;
    private bool destroyed;

    protected override void OnDestroy() {
        if (destroyed) {
            return;
        }

        destroyed = true;
        sibling.Destroy();
    }
    
}
