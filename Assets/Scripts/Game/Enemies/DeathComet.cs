using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComet : Enemy {

    public GameObject fire;
    public bool isSpawnObject = false;
    public GameObject deathCometActual;

    public GameObject bullet;

    public int numberOfSkulls = 8;
    public GameObject skull;

    public GameObject[] skulls;

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
            SpawnSkulls();
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

    private void SpawnSkulls() {
        skulls = new GameObject[numberOfSkulls];
        float angle = 0.1f * (float)numberOfSkulls;

        for (int i = 0; i < numberOfSkulls; i++) {
            Skull skullObj = Instantiate(skull, transform.position, Quaternion.identity).GetComponent<Skull>();
            skulls[i] = skullObj.gameObject;
            skullObj.SetOriginAngle(i * angle, transform.position);
        }

        StartCoroutine("ShotRoutine");
    }

    IEnumerator ShotRoutine() {
        yield return new WaitForSeconds(4f);

        while(!isDead) {
            yield return new WaitForSeconds(3f);

            StartCoroutine("SkullAttack");

            yield return new WaitForSeconds(3f);

            StartCoroutine("CircleShot");
        }
    }

    IEnumerator SkullAttack() {
        yield return new WaitForSeconds(1f);

        List<GameObject> activeSkulls = new List<GameObject>();

        foreach(GameObject obj in skulls) {
            if(obj) {
                activeSkulls.Add(obj);
            }
        }

        if(activeSkulls.Count > 0) {
            activeSkulls[Random.Range(0, activeSkulls.Count)].SendMessage("InitBasicAttack", 4);
        }
    }

    IEnumerator CircleShot() {
        yield return new WaitForSeconds(1f);

        int numberOfShots = 15;
        float fullCircle = 360f;

        float degreesBetweenShots = fullCircle / (float)numberOfShots;

        for(int i = 0; i < numberOfShots; i++) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            // set bullet to have correct vector
            
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