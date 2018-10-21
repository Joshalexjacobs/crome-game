using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1.0f;
    public float deathTime = 10f;
    public float acceleration = 0f;
    public bool trailPlayer = false;
    public bool isHeatSeeking = false;
    public bool sineWave = false;
    public bool delayShot = false;
    public Vector3 direction;
    public GameObject bulletTrail;

    private Rigidbody2D rb;
    private Transform player;
    private bool isDead = false;

    public void Init(Vector3 direction) {
        this.direction = direction;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerGameObj = GameObject.Find("Player");

        if (playerGameObj && playerGameObj.transform) {
            player = playerGameObj.transform;
        }

        if (player && trailPlayer) {
            rb.AddForce((player.position - transform.position).normalized * speed);
        } else if(!isHeatSeeking){
            rb.AddForce(direction * speed);
        }

        if (bulletTrail) {
            StartCoroutine("SpawnBulletTrail");
        }

        if(isHeatSeeking) {
            StartCoroutine("Seek");
        }

        if (delayShot) {
            StartCoroutine("DelayShot");
        }

        StartCoroutine("Die");
    }

    IEnumerator DelayShot() {
        yield return new WaitForSeconds(1f);

        GameObject playerGameObj = GameObject.Find("Player");

        if (playerGameObj && playerGameObj.transform) {
            player = playerGameObj.transform;
        }

        rb.velocity = Vector3.zero;
        rb.AddForce((player.position - transform.position).normalized * speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");
        }

        Destroy(gameObject);
    }

    public virtual void Damage(float damage) {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position);
        Destroy(gameObject);
    }

    IEnumerator Seek() {
        rb.AddForce(direction * speed);
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;

        while (!isDead && player) {
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, speed));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, player.position - transform.position);
            yield return new WaitForSeconds(0.035f);
        }
    }

    IEnumerator SpawnBulletTrail() {
        while (!isDead) {
            yield return new WaitForSeconds(0.1f);
            Instantiate(bulletTrail, transform.position - new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0f), Quaternion.identity);
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(deathTime);
        isDead = true;

        if(isHeatSeeking) {
            ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
            explosionManager.AddExplosions(gameObject.transform.position);
        }

        Destroy(gameObject);
    }

    public void FixedUpdate() {
        if(acceleration != 0f) {
            rb.AddForce(direction * acceleration);
        }

        if(sineWave) {
            transform.Translate(0.002f * Mathf.Sin(12 * (transform.position.y)), 0, 0);
        }
    }
}
