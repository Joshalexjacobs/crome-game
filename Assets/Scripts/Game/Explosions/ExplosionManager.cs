using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

    public GameObject[] explosions;

    private Vector2 position;
	
    public void AddExplosions(Vector2 position) {
        AddExplosions(position, 1, false);
    }

    public void AddExplosions(Vector2 positon, int numberOfExplosions, bool randomization) {
        if(!randomization) {
            int i = Random.Range(0, explosions.Length);
            Instantiate(explosions[i], positon, Quaternion.identity);
        } else {
            this.position = positon;
            StartCoroutine("SpawnExplosions", numberOfExplosions);
        }
    }

    IEnumerator SpawnExplosions(int numberOfExplosionss) {
        yield return new WaitForSeconds(1f);

        // loop through explosions

        this.position = Vector2.zero;
    }
}
