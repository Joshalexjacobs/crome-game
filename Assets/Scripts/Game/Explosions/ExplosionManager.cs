using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

    public GameObject[] explosions;

    private Vector2 position;
    private float min = 0f;
    private float max = 0f;
	
    public void AddExplosions(Vector2 position) {
        AddExplosions(position, 1, false, 0f, 0f);
    }

    public void AddExplosions(Vector2 positon, int numberOfExplosions, bool randomization, float min, float max) {
        if(!randomization) {
            int i = Random.Range(0, explosions.Length);
            GameObject explosionObj = Instantiate(explosions[i], positon, Quaternion.identity).gameObject;
            explosionObj.transform.parent = transform;
        } else {
            this.min = min;
            this.max = max;
            this.position = positon;
            StartCoroutine("SpawnExplosions", numberOfExplosions);
        }
    }

    IEnumerator SpawnExplosions(int numberOfExplosions) {
        for (int i = 0; i < numberOfExplosions; i++)
        {
            int range = Random.Range(0, explosions.Length);
            Vector2 mod = new Vector2(Random.Range(min, max), Random.Range(min, max));
            GameObject explosionObj = Instantiate(explosions[range], this.position + mod, Quaternion.identity).gameObject;
            explosionObj.transform.parent = transform;
            yield return new WaitForSeconds(0.05f);
        }

        this.position = Vector2.zero;
    }
}
