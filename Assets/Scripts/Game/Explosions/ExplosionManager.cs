using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

    public GameObject[] explosions;
	
    public void AddExplosions(Vector2 position) {
        AddExplosions(position, 1, false, 0f, 0f);
    }

    private AudioSource[] audio;

    private void Start() {
        audio = GetComponents<AudioSource>();
    }

    public void AddExplosions(Vector2 position, int numberOfExplosions, bool randomization, float min, float max) {
        int track = Random.Range(0, audio.Length);
        audio[track].pitch = Random.Range(0.80f, 1.20f);
        audio[track].Play();

        if (!randomization) {
            int i = Random.Range(0, explosions.Length);
            GameObject explosionObj = Instantiate(explosions[i], position, Quaternion.identity).gameObject;
            explosionObj.transform.parent = transform;
        } else {
            ExplosionParams explosionParams = new ExplosionParams(position, numberOfExplosions, min, max);
            StartCoroutine("SpawnExplosions", explosionParams);
        }
    }

    IEnumerator SpawnExplosions(ExplosionParams explosionParams) {
        Vector2 position = explosionParams.position;
        float min = explosionParams.min;
        float max = explosionParams.max;

        for (int i = 0; i < explosionParams.numberOfExplosions; i++) {
            int range = Random.Range(0, explosions.Length);
            Vector2 mod = new Vector2(Random.Range(min, max), Random.Range(min, max));
            GameObject explosionObj = Instantiate(explosions[range], position + mod, Quaternion.identity).gameObject;
            explosionObj.transform.parent = transform;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
