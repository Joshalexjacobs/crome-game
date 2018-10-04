using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStars : MonoBehaviour {

    public BackgroundStar[] stars;
    public GameObject starsParent;

    public int numberOfStars = 10;

	void Start () {
        SpawnStars();
    }
	
    private void SpawnStars() {
        for (int i = 0; i < numberOfStars; i++) {
            float x = Random.Range(-0.8f, 0.8f);
            float y = Random.Range(-0.7f, 0.7f);

            GameObject starObj = Instantiate(stars[Random.Range(0, stars.Length)], new Vector3(x, y, 0f), Quaternion.identity).gameObject;
            starObj.transform.parent = starsParent.transform;
        }
    }
}
