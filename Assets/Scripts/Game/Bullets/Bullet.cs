using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1.0f;
    public float deathTime = 10f;
    public bool trailPlayer = false;
    public GameObject bulletTrail;

    private Rigidbody2D rb;
    private Transform player;
    private bool isDead = false;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerGameObj = null;

        if (trailPlayer) {
            playerGameObj = GameObject.Find("Player");
        }

        if (playerGameObj && playerGameObj.transform) {
            player = playerGameObj.transform;
        }

        if (player) {
            rb.AddForce((player.position - transform.position).normalized * speed);
        }

        if (bulletTrail) {
            StartCoroutine("SpawnBulletTrail");
        }

        StartCoroutine("Die");
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");
        }

        Destroy(gameObject);
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
        Destroy(gameObject);
    }
}
