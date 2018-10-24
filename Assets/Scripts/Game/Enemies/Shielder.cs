using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : Enemy {

    public GameObject bullet;

    public Shielder() {
        health = 6;
        isDead = false;
    }

    private BoxCollider2D ogBox;
    private BoxCollider2D shieldBox;

    private Animator animator;
    private AudioSource audio;

    void Start () {
        BoxCollider2D[] boxes = GetComponentsInChildren<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        ogBox = boxes[0];
        shieldBox = boxes[1];

        ogBox.enabled = false;
        shieldBox.enabled = false;

        animator = GetComponent<Animator>();

        StartCoroutine("Expand");
	}

    void Update() {
        if(!isDead) {
            transform.Translate(new Vector3(0f, 0.001f * Mathf.Sin(10f * (Time.time / 4)), 0f));
        }
    }

    IEnumerator Expand() {
        transform.localScale = new Vector3(0f, 0f, 0f);

        for (int i = 0; i <= 10; i++) {
            transform.localScale = new Vector3(i * 0.1f, i * 0.1f, i * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        ogBox.enabled = true;

        StartCoroutine("Shield");
    }

    IEnumerator Shield() {
        while(!isDead) {
            animator.SetBool("isShielded", true);

            yield return new WaitForSeconds(0.750f);

            shieldBox.enabled = true;
            ogBox.enabled = false;

            yield return new WaitForSeconds(1f);

            StartCoroutine("Shoot");

            yield return new WaitForSeconds(3f);

            animator.SetBool("isShielded", false);
            shieldBox.enabled = false;
            ogBox.enabled = true;

            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator Shoot() {
        for (int i = 0; i < 2 && !isDead; i++) {
            Bullet bulletObj = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bulletObj.Init(new Vector2(Random.Range(-0.5f, 0.5f), 1f));

            audio.pitch = Random.Range(0.9f, 1.1f);
            audio.Play();

            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 6, true, -0.05f, 0.05f);

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}