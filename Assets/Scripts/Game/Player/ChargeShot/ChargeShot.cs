using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour {

    public float speed = 1f;
    public float deathTime = 1.5f;

    public ChargeShotAOE chargeShotAOE;

    private float damage = 1f;
    private float radius = 0.4f;
    private float playerLevel = 1f;

    private BoxCollider2D box;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource[] audio;

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine("ChargeShotBulletDeath");
    }

    public void Init(int stage, int playerLevel) {
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.AddForce(new Vector2(0.0f, speed));

        this.playerLevel = playerLevel;

        if (stage == 1) {
            this.damage = 5f;
        } else if (stage == 2) {
            this.damage = 10f;
            this.radius = 0.5f;
        } else {
            this.damage = 15f;
            this.radius = 0.6f;
        }
    }

    IEnumerator ChargeShotBulletDeath() {
        yield return new WaitForSeconds(deathTime);
        rb.isKinematic = true;
        box.enabled = false;
        rb.velocity = new Vector2(0f, 0f);
        HandleSplashDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
            rb.isKinematic = true;
            box.enabled = false;
            rb.velocity = new Vector2(0f, 0f);
            HandleSplashDamage();
        } else if (collision.tag == "Shield") {
            rb.isKinematic = true;
            box.enabled = false;
            rb.velocity = new Vector2(0f, 0f);

            HandleSplashDamage();
        }
    }

    private void HandleSplashDamage() {
        ChargeShotAOE chargeShotAOEObj = Instantiate(chargeShotAOE, transform.position, Quaternion.identity).GetComponent<ChargeShotAOE>();
        chargeShotAOEObj.Init(radius, (damage / 5f) + ((float)playerLevel * 0.1f));
        Destroy(gameObject);
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(0.3f);
    }

}
