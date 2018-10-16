using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public float deathTimeMin = 0.3f;
    public float deathTimeMax = 0.9f;
    public float forceMin = 0.05f;
    public float forceMax = 0.09f;
    public GameObject bulletTrail;

    private bool isDead = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.up * Random.Range(forceMin, forceMax));

        StartCoroutine("SpawnFireTrail");
        StartCoroutine("Die");
	}
	
    IEnumerator SpawnFireTrail() {
        while (!isDead) {
            yield return new WaitForSeconds(0.1f);
            GameObject fireTrailObj = Instantiate(bulletTrail, transform.position - new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0f), Quaternion.identity);
            //fireTrailObj.transform.parent = transform;
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(Random.Range(deathTimeMin, deathTimeMax));
        Destroy(gameObject);
    }
}
