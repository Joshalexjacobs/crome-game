using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {
	
    public void MoveAllStarsDown() {
        BackgroundStar[] backgroundStars = GetComponentsInChildren<BackgroundStar>();
        foreach(BackgroundStar backgroundStar in backgroundStars) {
            backgroundStar.StartMoveDown();
        }

        Planet planet = GetComponentInChildren<Planet>();
        planet.StartMoveDown();
    }

}
