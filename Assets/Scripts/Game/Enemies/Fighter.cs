using System.Collections;
using UnityEngine;

public class Fighter : Enemy {

    public GameObject bullet;

    public float fallSpeed = 0.5f;
    public float speed = 0.5f;

    public Fighter() {
        health = 10;
    }

    private bool isReady = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        if (transform.position.y > 0.5f && !isReady) {
            rb.MovePosition(rb.position + new Vector2(0f, fallSpeed) * Time.fixedDeltaTime);
        } else if (!isReady) {
            isReady = true;
            StartCoroutine("Shoot");
            rb.AddForce(Vector2.down * speed);
        }

        if (transform.position.y < -1f) {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot() {
        while(!isDead) {
            for (int i = 0; i < 3 && !isDead; i++) {
                Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().SendMessage("Init", Vector3.down);
                audio.Play();
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    public override IEnumerator HandlePlayerCollision() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.2f);
        rb.isKinematic = false;
        rb.AddForce(Vector2.down * speed);
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 3, true, -0.02f, 0.02f);

        yield return new WaitForSeconds(0.25f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}
