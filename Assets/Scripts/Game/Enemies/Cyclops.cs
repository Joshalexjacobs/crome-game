using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : Enemy {

    public float stopPoint = 0.5f;
    public float fallSpeed = 0.1f;
    public float direction = 1.0f;
    public float speed = 0.5f;
    public float xModifier = 0.1f;
    public bool flyDown = false;

    public GameObject bullet;

    public Cyclops() {
        health = 3f;
    }

    private GameObject player;

    private bool isReady = false;

    private Rigidbody2D rb;
    private Animator animator;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isSpawning", flyDown);

        if (flyDown) {
            StartCoroutine("Die");
        } else {
            StartCoroutine("SpawnBullets");
            StartCoroutine("Blink");
        }
	}

    public void FlyDown() {
        animator = GetComponent<Animator>();
        animator.SetBool("isSpawning", true);
    }

    private void FixedUpdate() {
        if (gameObject.transform.position.y > stopPoint && !isReady) {
            rb.MovePosition(rb.position + new Vector2(xModifier, fallSpeed) * Time.fixedDeltaTime);
        } else if(!isReady) {
            isReady = true;
        } else {
            Vector3 movement = Vector3.right * direction * speed * Time.deltaTime;
            transform.Translate(new Vector3(movement.x, 0.001f * Mathf.Cos(10 * (transform.position.x)), 0f));
        }

        if (transform.position.x <= -0.7f) {
            direction = 1.0f;
        }
        else if (transform.position.x >= 0.7f) { 
            direction = -1.0f;
        }
    }

    IEnumerator SpawnBullets() {
        while(!isDead) {
            yield return new WaitForSeconds(Random.Range(1f, 4.5f));

            if(player == null) {
                player = GameObject.Find("Player");
            }

            if (isReady && player) {
                Instantiate(bullet, gameObject.transform.position + new Vector3 (0f, -0.035f, 0f), Quaternion.identity);
            }
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    IEnumerator Blink() {
        while(!isDead) {
            yield return new WaitForSeconds(Random.Range(0.55f, 4.55f));
            animator.SetTrigger("isBlinking");
        }
    }
}