using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : Enemy {

    public GameObject bullet;

    public float fallSpeed = 0.01f;
    public float speed = 0.5f;

    public Vulture() {
        health = 25;
        isDead = false;
    }

    private bool isReady = false;
    private Transform player;
    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();

        if (player == null) {
            player = GameObject.Find("Player").gameObject.transform;
        }
    }

    private void FixedUpdate(){
        if (transform.position.y > 0.5f && !isReady) {
            rb.MovePosition(rb.position + new Vector2(0f, fallSpeed) * Time.fixedDeltaTime);
        } else if (!isReady) {
            isReady = true;
            StartCoroutine("Shoot");
            rb.AddForce(Vector2.down * speed);
        }

        if(transform.position.y < -1f) {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot() {
        while (!isDead) {
            for (int i = 0; i < 3 && !isDead; i++) {
                Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity).GetComponent<Bullet>().SendMessage("Init", new Vector3(0.5f, -1f, 0f));
                Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity).GetComponent<Bullet>().SendMessage("Init", new Vector3(-0.5f, -1f, 0f));
                Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity).GetComponent<Bullet>().SendMessage("Init", new Vector3(0.5f, -0.25f, 0f));
                Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity).GetComponent<Bullet>().SendMessage("Init", new Vector3(-0.5f, -0.25f, 0f));
                yield return new WaitForSeconds(0.4f);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    public override IEnumerator Death()
    {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 16, true, -0.07f, 0.07f);

        yield return new WaitForSeconds(0.75f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}
