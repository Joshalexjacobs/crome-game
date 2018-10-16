using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEater : Enemy {

    public GameObject bullet;

    public float fallSpeed = 0.5f;

    public AntEater() {
        health = 4;
        isDead = false;
    }

    private bool isReady = false;

    private Rigidbody2D rb;
    private Animator animator;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (transform.position.y > 0.5f && !isReady) {
            rb.MovePosition(rb.position + new Vector2(0f, fallSpeed) * Time.fixedDeltaTime);
        } else if (!isReady) {
            isReady = true;
            animator.SetBool("isCloaked", false);
            StartCoroutine("Shoot");
        } else {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(15f * (Time.time / 4)), 0f));
        }
    }

    IEnumerator Shoot() {
        while(!isDead) {
            GameObject[] bullets = new GameObject[3];

            bullets[0] = Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity);
            bullets[1] = Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity);
            bullets[2] = Instantiate(bullet, transform.position + new Vector3(0f, -0.05f, 0f), Quaternion.identity);

            bullets[0].SendMessage("Init", new Vector3(-0.5f, -1f, 0f));
            bullets[1].SendMessage("Init", new Vector3(0.5f, -1f, 0f));
            bullets[2].SendMessage("Init", Vector3.down);

            yield return new WaitForSeconds(3f);
        }
    }
}
