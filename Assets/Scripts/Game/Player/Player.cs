﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    /* PUBLIC VARS */

    public float speed = 5f;
    public float damage = 1f;

    public int experience = 0;
    public int currentLevel = 0;

    public int lives = 3;

    public Vector2 respawnPoint;

    /* PUBLIC OBJECTS */

    public GameObject playerTrailParticle;
    public PlayerBullet playerBullet;

    /* PRIVATE VARS */

    private bool isDead = false;
    private bool isMoving = false;

    private float myFireTime = 0.0f;
    private float nextFire = 0.2f;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sr;
    private TrailRenderer tr;
    private PlayerLivesUI playerLivesUI;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
        playerLivesUI = FindObjectOfType<PlayerLivesUI>();

        playerLivesUI.SetLives(lives);
        StartCoroutine("SpawnPlayerTrailParticles");
    }

    void Update() {
        if(!isDead) {
            UpdateMisc();

            /* SHOOTING - SPACE */
            HandleShooting(Input.GetButton("Fire1") && myFireTime > nextFire);

            /* MOVEMENT - WASD/ANALOG */
            HandleMovement();
        }
    }

    private void UpdateMisc() {
        myFireTime = myFireTime + Time.deltaTime;
    }

    private void HandleShooting(bool isShooting) {
        if (isShooting) {
            //camera.SendMessage("BeginShortVerticalShake", 0.0025f);

            StartCoroutine("Shoot");
            myFireTime = 0.0f;
        }
    }

    IEnumerator Shoot() {
        PlayerBullet playerBulletObj = Instantiate(playerBullet, transform.position + new Vector3(Random.Range(-0.001f, 0.003f), 0.035f, 0f), Quaternion.identity);
        playerBulletObj.Init(damage);

        yield return new WaitForSeconds(0.1f);
    }

    private void HandleMovement() {
        /* HORIZONTAL MOVEMENT */
        float dx = HandleHorizontalMovement(Input.GetAxisRaw("Horizontal") != 0);

        /* VERTICAL MOVEMENT */
        float dy = HandleVerticalMovement(Input.GetAxis("Vertical") != 0);

        /* UPDATE RB */
        rb.velocity = new Vector2(dx, dy);
    }

    private float HandleHorizontalMovement(bool isHorizontal) {
        float dx = rb.velocity.x;

        if (isHorizontal) {
            dx = speed * Input.GetAxis("Horizontal");
        }

        return dx;
    }

    private float HandleVerticalMovement(bool isVertical) {
        float dy = rb.velocity.y;

        if (isVertical) {
            dy = speed * Input.GetAxis("Vertical");
        }

        return dy;
    }

    /* OTHER */

    public void GainExperience(int xp) {
        experience += xp;
        HandleLevelUp();
    }

    private void HandleLevelUp() {
        
    }

    IEnumerator SpawnPlayerTrailParticles() {
        while(!isDead) {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
            GameObject particleObj = Instantiate(playerTrailParticle, transform.position + new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.005f, 0.005f), 0f), Quaternion.identity);
            particleObj.transform.parent = transform;
        }
    }

    public void Damage() {
        if (!isDead) {
            if(playerLivesUI == null) {
                playerLivesUI = FindObjectOfType<PlayerLivesUI>();
            }

            playerLivesUI.DecrementLives();
            lives--;

            box.enabled = false;
            isDead = true;

            ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
            explosionManager.AddExplosions(gameObject.transform.position);

            if (lives > 0) {
                sr.enabled = false;
                tr.enabled = false;
                transform.position = respawnPoint;
                StartCoroutine("Respawn");
            } else {
                StartCoroutine("Restart");
            }
        }
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(0.6f);

        isDead = false;
        box.enabled = true;
        Physics2D.IgnoreLayerCollision(11, 8, true);
        Physics2D.IgnoreLayerCollision(11, 9, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);

        for (int i = 0; i < 7; i++) {
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

        sr.enabled = true;
        tr.enabled = true;
        StartCoroutine("SpawnPlayerTrailParticles");
        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreLayerCollision(11, 8, false);
        Physics2D.IgnoreLayerCollision(11, 9, false);
        Physics2D.IgnoreLayerCollision(11, 12, false);
    }

    IEnumerator Restart() {
        sr.enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("main");
    }
}