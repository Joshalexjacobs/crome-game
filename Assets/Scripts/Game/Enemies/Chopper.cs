using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : Enemy {

    public GameObject drone;

    public float fallSpeed = 0f;

    public Chopper() {
        health = 25;
    }

    private bool isReady = false;

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	

    private void FixedUpdate() {
        if(transform.position.y > 0.5f && !isReady) {
            rb.MovePosition(rb.position + new Vector2(0f, fallSpeed) * Time.fixedDeltaTime);
        } else if (!isReady) {
            isReady = true;
            StartCoroutine("ShootDrones");
        } else {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(15f * (Time.time / 4)), 0f));
        }
    }

    IEnumerator ShootDrones() {
        while(!isDead) {
            for (int i = 0; i < 3 && !isDead; i++) {
                Instantiate(drone, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 12, true, -0.07f, 0.07f);

        yield return new WaitForSeconds(0.75f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }

}
