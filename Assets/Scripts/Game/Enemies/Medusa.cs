using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Enemy {

    public float stopPoint = 0.5f;
    public float flyInSpeed = 0.2f;
    public bool onRight = true;
    public GameObject bullet;

    public Medusa() {
        health = 6f;
    }

    private bool isReady = false;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audio;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        StartCoroutine("Shoot");
	}

    private void FixedUpdate() {
        if(!isReady) {
            if(onRight && gameObject.transform.position.x > stopPoint) {
                rb.MovePosition(rb.position + new Vector2(flyInSpeed, 0.05f) * Time.fixedDeltaTime);
            } else if(!onRight && gameObject.transform.position.x < stopPoint) {
                rb.MovePosition(rb.position + new Vector2(flyInSpeed, 0.05f) * Time.fixedDeltaTime);
            } else {
                isReady = true;
            }
        } else {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(10f * (Time.time / 4)), 0f));
        }
    }

    IEnumerator Shoot() {
        while(!isDead) {
            yield return new WaitForSeconds(0.2f);

            if (isReady) {
                animator.SetBool("isShooting", true);

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 5; i++) {
                    Bullet bulletObj = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
                    bulletObj.Init(new Vector3(Random.Range(-0.5f, 0.5f), -1f, 0f));

                    audio.pitch = Random.Range(0.9f, 1.1f);
                    audio.Play();

                    yield return new WaitForSeconds(0.2f);
                }

                animator.SetBool("isShooting", false);

                yield return new WaitForSeconds(2.5f);
            }
        }
    }
}