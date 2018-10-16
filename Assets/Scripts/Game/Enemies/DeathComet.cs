using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComet : Enemy {

    public GameObject fire;
    public bool isSpawnObject = false;
    public GameObject deathCometActual;

    public DeathComet() {
        health = 50;
        isDead = false;
    }

    private bool isReady = false;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        if(isSpawnObject) {
            StartCoroutine("Spawn");
        } else {
            StartCoroutine("SpawnFire");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(!isDead) { // doesnt work because of the movement animation
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(10f * (Time.time / 4)), 0f));
        }
    }

    public override IEnumerator Flash() {
        sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 2; i++) {
            sr.material.SetFloat("_FlashAmount", 1);
            //sr.enabled = false;
            yield return new WaitForSeconds(0.035f);
            //sr.enabled = true;
            sr.material.SetFloat("_FlashAmount", 0);
            yield return new WaitForSeconds(0.035f);
        }
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(5f);
        Instantiate(deathCometActual, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator SpawnFire() {
        while (!isDead) {
            int max = Random.Range(2, 4);

            for (int i = 0; i < max; i++) {
                GameObject fireObj = Instantiate(fire, transform.position + new Vector3(Random.Range(-0.075f, 0.075f), Random.Range(0.13f, 0.15f), 0f), Quaternion.identity);
                fireObj.transform.parent = transform;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 15, true, -0.1f, 0.1f);
        explosionManager.AddExplosions(gameObject.transform.position, 10, true, -0.1f, 0.1f);

        yield return new WaitForSeconds(0.75f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}