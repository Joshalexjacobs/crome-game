using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy {

    public float force = 0.01f;

    public Drone() {
        health = 1;
        isDead = false;
    }

    private Transform player;
    private Rigidbody2D rb;

	void Start () {
        if(player == null) {
            player = GameObject.Find("Player").gameObject.transform;
        }

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        rb.AddForce(Vector3.down * 0.05f);
        yield return new WaitForSeconds(2f);

        if(player == null) {
            player = GameObject.Find("Player").gameObject.transform;
        }

        while (!isDead && player) {
            rb.AddForce((player.position - transform.position).normalized * force / 3f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, player.position - transform.position);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");

            GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
            StartCoroutine("Death");
        }
    }
}
