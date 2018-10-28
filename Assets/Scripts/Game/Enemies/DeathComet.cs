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

    public int phase = 1;

    public DeathComet() {
        health = 50;
        isDead = false;
    }

    private bool isReady = false;
    private Skull[] skulls;

    private SpriteRenderer baseSr;
    private Animator animator;
    private AudioSource[] audio;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        audio = GetComponents<AudioSource>();
        health = health * phase;

        if (isSpawnObject) {
            StartCoroutine("Spawn");
        } else {
            StartCoroutine("SpawnFire");
            SpawnSkulls();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead) {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(10f * (Time.time / 4)), 0f));
        } else if(isDead) {
            transform.Translate(new Vector3(0f, -0.0005f, 0f));
        }
    }

    public override IEnumerator Flash() {
        baseSr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 2; i++) {
            baseSr.material.SetFloat("_FlashAmount", 1);
            //sr.enabled = false;
            yield return new WaitForSeconds(0.035f);
            //sr.enabled = true;
            baseSr.material.SetFloat("_FlashAmount", 0);
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
        skulls = new Skull[numberOfSkulls];
        float angle = 0.1f * (float)numberOfSkulls;

        for (int i = 0; i < numberOfSkulls; i++) {
            Skull skullObj = Instantiate(skull, transform.position, Quaternion.identity).GetComponent<Skull>();
            skullObj.phase = phase;

            if(phase == 1) {
                skullObj.respawnTime = 25f;
                skullObj.health = 5;
            } else if(phase == 2) {
                skullObj.respawnTime = 20f;
                skullObj.health = 6;
            } else if (phase == 3) {
                skullObj.respawnTime = 15f;
                skullObj.health = 7;
            }

            skulls[i] = skullObj;
            skullObj.SetOriginAngle(i * angle, transform.position);
        }

        StartCoroutine("ShotRoutine");
    }

    private string GetNextAttack() {
        int nextAttack = Random.Range(0, phase);
        string results = "";

        switch(nextAttack) {
            case 0:
                results = "CircleShot";
                break;
            case 1:
                results = "SineShot";
                break;
            case 2:
                results = "DelayedShot";
                break;
        }

        return results;
    }

    IEnumerator ShotRoutine() {
        yield return new WaitForSeconds(4f);

        while(!isDead) {
            StartCoroutine("SkullAttack");

            yield return new WaitForSeconds(3f);

            StartCoroutine(GetNextAttack()); // generic death comet attack

            yield return new WaitForSeconds(4f);

            if(phase > 1 && !isDead) {
                HandleDeathCometWarp();
            }
        }
    }

    IEnumerator CircleShot() {
        yield return new WaitForSeconds(1f);

        animator.SetBool("isShooting", true);

        int numberOfShots = 15;
        float fullCircle = 360f;

        float degreesBetweenShots = fullCircle / (float)numberOfShots;

        for (int j = 0; j < 3 && !isDead; j++) {
            for (int i = 0; i < numberOfShots && !isDead; i++) {
                Vector3 dir = Quaternion.AngleAxis(degreesBetweenShots * i, Vector3.forward) * Vector3.right;

                Bullet bulletObj = Instantiate(bullet, transform.position + dir / 7, Quaternion.identity).GetComponent<Bullet>();
                bulletObj.Init(dir);
                bulletObj.speed = 0.01f;
                bulletObj.acceleration = 0.00025f;

                audio[1].Play();

                yield return new WaitForSeconds(0.025f);
            }
        }

        animator.SetBool("isShooting", false);
    }

    IEnumerator SineShot() {
        yield return new WaitForSeconds(1f);

        animator.SetBool("isShooting", true);

        int numberOfShots = 6;
        float fullCircle = 360f;

        float degreesBetweenShots = fullCircle / (float)numberOfShots;

        for (int j = 0; j < 3 && !isDead; j++) {
            for (int i = 0; i < numberOfShots && !isDead; i++) {
                Vector3 dir = Quaternion.AngleAxis(degreesBetweenShots * i, Vector3.forward) * Vector3.right;

                Bullet bulletObj = Instantiate(bullet, transform.position + dir / 7, Quaternion.identity).GetComponent<Bullet>();
                bulletObj.trailPlayer = true;
                bulletObj.sineWave = true;
                bulletObj.speed = 0.015f;
                bulletObj.acceleration = 0.00005f;

                audio[2].Play();

                yield return new WaitForSeconds(0.025f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        animator.SetBool("isShooting", false);
    }

    IEnumerator DelayedShot() {
        yield return new WaitForSeconds(1f);

        animator.SetBool("isShooting", true);

        int numberOfShots = 4;
        float fullCircle = 360f;

        float degreesBetweenShots = fullCircle / (float)numberOfShots;

        for (int j = 0; j < 3 && !isDead; j++) {
            for (int i = 0; i < numberOfShots && !isDead; i++) {
                Vector3 dir = Quaternion.AngleAxis(degreesBetweenShots * i, Vector3.forward) * new Vector3(0.25f, 0f, 0f);

                Bullet bulletObj = Instantiate(bullet, transform.position + dir / 2, Quaternion.identity).GetComponent<Bullet>();
                bulletObj.delayShot = true;
                bulletObj.Init(dir);
                bulletObj.speed = 0.0225f;
                bulletObj.acceleration = 0.00005f;
            }

            audio[3].Play();
            yield return new WaitForSeconds(0.75f);
        }

        animator.SetBool("isShooting", false);
    }

    IEnumerator SkullAttack() {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < phase; i++) {
            List<Skull> activeSkulls = GetListOfActiveNonShootingSkulls();

            if (activeSkulls.Count > 0) {
                int skullToGrab = Random.Range(0, activeSkulls.Count);
                activeSkulls[skullToGrab].InitBasicAttack(3);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private List<Skull> GetListOfActiveNonShootingSkulls() {
        List<Skull> activeSkulls = new List<Skull>();

        foreach (Skull obj in GetListOfActiveSkulls()) {
            if(!obj.GetIsAttacking() && obj.GetIsReady()) {
                activeSkulls.Add(obj);
            }
        }

        return activeSkulls;
    }

    private List<Skull> GetListOfActiveSkulls() {
        List<Skull> activeSkulls = new List<Skull>();

        foreach (Skull obj in skulls) {
            if (obj && !obj.isDead) {
                activeSkulls.Add(obj);
            }
        }

        return activeSkulls;
    }

    private List<Skull> GetListOfAllSkulls() {
        List<Skull> activeSkulls = new List<Skull>();

        foreach (Skull obj in skulls) {
            if (obj) {
                activeSkulls.Add(obj);
            }
        }

        return activeSkulls;
    }

    private void HandleDeathCometWarp() {
        List<Skull> activeSkulls = GetListOfAllSkulls();
        Vector3 newPosition;
        if (transform.position.x < 0.3f && transform.position.x > -0.3f) {
            if (Random.Range(-1, 1) == -1) {
                newPosition = new Vector3(-0.4f, 0.2f, 0f);
            } else {
                newPosition = new Vector3(0.4f, 0.2f, 0f);
            }
        } else {
            newPosition = new Vector3(0f, 0.3f, 0f);
        }

        transform.position = newPosition;

        foreach (Skull obj in activeSkulls) {
            if(obj) {
                obj.SetCenter(newPosition);
            }
        }
    }

    public void DestroyAllActiveSkulls() {
        Skull[] allSkulls = GameObject.FindObjectsOfType<Skull>();

        foreach (Skull skullObj in allSkulls) {
            if (skullObj) {
                if (!skullObj.isDead) {
                    skullObj.respawnable = false;
                    skullObj.Damage(100f);
                } else {
                    Destroy(skullObj.gameObject);
                }
            }
            
        }
    }

    public override IEnumerator Death() {
        DestroyAllActiveSkulls();
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 25, true, -0.1f, 0.1f);
        yield return new WaitForSeconds(0.15f);
        explosionManager.AddExplosions(gameObject.transform.position, 10, true, -0.15f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        explosionManager.AddExplosions(gameObject.transform.position, 10, true, -0.15f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        explosionManager.AddExplosions(gameObject.transform.position, 10, true, -0.15f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        explosionManager.AddExplosions(gameObject.transform.position, 10, true, -0.15f, 0.15f);
        yield return new WaitForSeconds(0.15f);

        for (int i = 4; i > 0; i--) {
            baseSr.material.color = new Color(1f, 1f, 1f, 0.25f * i);
            explosionManager.AddExplosions(gameObject.transform.position, 2, true, -0.15f, 0.15f);
            yield return new WaitForSeconds(0.25f);
        }

        Destroy(gameObject);
    }
}