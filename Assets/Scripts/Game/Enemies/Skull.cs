using System.Collections;
using UnityEngine;

public class Skull : Enemy {

    public float rotateSpeed = 0.5f;
    public float distance = 0.5f;
    public int fluctuationRange = 5;
    public int phase = 0;
    public int maxHealth = 10;
    public float respawnTime = 10f;
    public GameObject fire;
    public GameObject bullet;
    public bool respawnable = true;

    public Skull() {
        health = 25;
        isDead = false;
    }

    private bool isReady = false;
    private bool isAttacking = false;
    private float angle = 0f;
    private Vector3 center = Vector3.zero;

    private BoxCollider2D baseBox;
    private Animator animator;
    private SpriteRenderer baseSr;
    private AudioSource[] audio;

    // Use this for initialization
    void Start () {
        if(!isDead) {
            baseBox = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            baseSr = GetComponent<SpriteRenderer>();
            audio = GetComponents<AudioSource>();
        }

        isDead = false;
        health = maxHealth;
        baseSr.enabled = true;

        baseBox.enabled = false;

        StartCoroutine("Expand");
    }

	void FixedUpdate () {
        Spin();
	}

    public void SetOriginAngle(float angle, Vector3 center) {
        this.angle = angle;
        this.center = center;
        isReady = true;
        StartCoroutine("FluctuateDistance");
    }

    public void SetCenter(Vector3 center) {
        this.center = center;
    }

    private void Spin() {
        angle += rotateSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * distance;
        transform.position = center + offset;
    }

    IEnumerator Expand() {
        transform.localScale = new Vector3(0f, 0f, 0f);

        audio[0].Play();

        for (int i = 0; i <= 10; i++) {
            transform.localScale = new Vector3(i * 0.1f, i * 0.1f, i * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        baseBox.enabled = true;

        StartCoroutine("SpawnFire");
    }

    IEnumerator SpawnFire() {
        while (!isDead) {
            int max = Random.Range(2, 4);

            for (int i = 0; i < max; i++) {
                GameObject fireObj = Instantiate(fire, transform.position + new Vector3(Random.Range(-0.055f, 0.055f), Random.Range(0.045f, 0.075f), 0f), Quaternion.identity);
                fireObj.transform.parent = transform;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    // at some point make this a death comet function instead
    IEnumerator FluctuateDistance() {
        while(true) {
            for(int i = 0; i < fluctuationRange; i++) {
                distance += 0.01f;
                yield return new WaitForSeconds(0.15f);
            }

            for (int i = 0; i < fluctuationRange; i++) {
                distance -= 0.01f;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }

    public bool GetIsAttacking() {
        return isAttacking;
    }

    public bool GetIsReady() {
        return isReady;
    }

    public void InitBasicAttack(int shots) {
        if(!isAttacking) {
            StartCoroutine("BasicAttack", shots);
        }
    }

    IEnumerator BasicAttack(int shots) {
        isAttacking = true;
        animator.SetBool("isShooting", true);

        for(int i = 0; i < shots; i++) {
            Instantiate(bullet, transform.position, Quaternion.identity).SendMessage("Init", new Vector3(Random.Range(-0.25f, 0.25f), -1f, 0f));
            audio[1].Play();
            yield return new WaitForSeconds(0.3f);
        }

        animator.SetBool("isShooting", false);
        isAttacking = false;
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 8, true, -0.09f, 0.09f);
        yield return new WaitForSeconds(0.15f);
        explosionManager.AddExplosions(gameObject.transform.position, 8, true, -0.07f, 0.07f);
        yield return new WaitForSeconds(0.20f);

        GetComponent<SpriteRenderer>().enabled = false;

        if (respawnable) {
            StartCoroutine("Respawn");
        } else if (!respawnable) {
            Destroy(gameObject);
        }
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(respawnTime);
        Start();
    }
}
