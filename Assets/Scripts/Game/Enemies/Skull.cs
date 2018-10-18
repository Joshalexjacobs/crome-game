using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Enemy {

    public float rotateSpeed = 0.5f;
    public float distance = 0.5f;
    public int fluctuationRange = 5;
    public GameObject fire;

    public Skull() {
        health = 25;
        isDead = false;
    }

    private bool isReady = false;
    private float angle = 0f;
    private Vector3 center = Vector3.zero;

    private BoxCollider2D box;

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;

        StartCoroutine("Expand");
    }

	void FixedUpdate () {
		if(isReady) {
            Spin();
        }
	}

    public void SetOriginAngle(float angle, Vector3 center) {
        this.angle = angle;
        this.center = center;
        isReady = true;
        StartCoroutine("FluctuateDistance");
    }

    private void Spin() {
        angle += rotateSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * distance;
        transform.position = center + offset;
    }

    IEnumerator Expand() {
        transform.localScale = new Vector3(0f, 0f, 0f);

        for (int i = 0; i <= 10; i++) {
            transform.localScale = new Vector3(i * 0.1f, i * 0.1f, i * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        box.enabled = true;

        StartCoroutine("SpawnFire");
    }

    IEnumerator SpawnFire() {
        while (!isDead) {
            int max = Random.Range(2, 4);

            for (int i = 0; i < max; i++) {
                GameObject fireObj = Instantiate(fire, transform.position + new Vector3(Random.Range(-0.055f, 0.055f), Random.Range(0.045f, 0.075f), 0f), Quaternion.identity);
                fireObj.transform.parent = transform;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    // at some point make this a death comet function instead
    IEnumerator FluctuateDistance() {
        while(true) {
            for(int i = 0; i < fluctuationRange; i++) {
                distance += 0.01f;
                yield return new WaitForSeconds(0.15f);
            }

            for (int i = 0; i < fluctuationRange; i++) {
                distance -= 0.01f;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 8, true, -0.09f, 0.09f);
        explosionManager.AddExplosions(gameObject.transform.position, 8, true, -0.07f, 0.07f);

        yield return new WaitForSeconds(0.35f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}
