using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStars : MonoBehaviour {

    public BackgroundStar[] stars;
    public GameObject starsParent;

    public int numberOfStars = 10;
    public int minNumberOfStars = 12;
    public int maxNumberOfStars = 15;

	void Start () {
        SpawnStars();
    }
	
    private void SpawnStars() {
        if(minNumberOfStars > 0 && maxNumberOfStars > 0) {
            numberOfStars = Random.Range(minNumberOfStars, maxNumberOfStars);
        }

        for (int i = 0; i < numberOfStars; i++) {
            float x = Random.Range(-0.8f, 0.8f);
            float y = Random.Range(-0.7f, 0.7f);

            GameObject starObj = Instantiate(stars[Random.Range(0, stars.Length)], new Vector3(x, y, 0f), Quaternion.identity).gameObject;
            starObj.transform.parent = starsParent.transform;
        }
    }
}
