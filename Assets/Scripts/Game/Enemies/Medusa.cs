using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Enemy {

    public float stopPoint = 0.5f;
    public float flyInSpeed = 0.2f;

    public Medusa() {
        health = 6f;
    }

    private bool isReady = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate() {
        if(!isReady && gameObject.transform.position.x > stopPoint) {
            rb.MovePosition(rb.position + new Vector2(flyInSpeed, 0.05f) * Time.fixedDeltaTime);
        }
        else if (!isReady) {
            isReady = true;
        } else {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(10f * (Time.time / 4)), 0f));
        }
    }

}
