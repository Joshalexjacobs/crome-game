using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEater : Enemy {

    public float fallSpeed = 0.5f;

    public AntEater() {
        health = 4;
        isDead = false;
    }

    private bool isReady = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate() {
        if (transform.position.y > 0.5f && !isReady) {
            rb.MovePosition(rb.position + new Vector2(0f, fallSpeed) * Time.fixedDeltaTime);
        } else if (!isReady) {
            isReady = true;
            //StartCoroutine("Shoot");
        }
    }
}
