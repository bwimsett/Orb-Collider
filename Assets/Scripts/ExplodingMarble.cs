using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ExplodingMarble : Marble
{
    protected override void OnCollisionWithMarble(Marble marble) {
        if (marble is MovableBlock && !moving) {
            return;
        }
        
        marble.Destroy();
        Destroy();
    }
}
