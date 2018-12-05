using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    /* PUBLIC VARS */

    public bool debugMode = false;

    public float speed = 5f;
    public float damage = 1f;

    public int experience = 0;
    public int currentLevel = 0;

    public int lives = 3;

    public Vector2 respawnPoint;

    /* PUBLIC OBJECTS */

    public GameObject playerTrailParticle;
    public PlayerBullet playerBullet;
    public PlayerBullet sideBulletLeft;
    public PlayerBullet sideBulletRight;
    public ExperienceBar experienceBar;
    public LevelUpText levelUpText;

    /* PRIVATE VARS */

    private bool isDead = false;
    private bool isMoving = false;

    private float myFireTime = 0.0f;
    private float mySideFireTime = 0.0f;
    private float nextFire = 0.2f;
    private float nextSideFire = 1f;

    private bool canFireDouble = false;

    private bool canSideFire = false;
    private int sideFireBullets = 2;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sr;
    private TrailRenderer tr;
    private AudioSource[] audio;
    private PlayerLivesUI playerLivesUI;
    private CromeController cromeController;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
        audio = GetComponents<AudioSource>();
        playerLivesUI = FindObjectOfType<PlayerLivesUI>();
        cromeController = GetComponent<CromeController>();

        playerLivesUI.SetLives(lives);
        StartCoroutine("SpawnPlayerTrailParticles");
    }

    void Update() {
        if(!isDead) {
            UpdateMisc();

            /* SHOOTING - SPACE */
            HandleShooting(cromeController.CromeIsFiring() && myFireTime > nextFire);

            /* MOVEMENT - WASD/ANALOG */
            HandleMovement();
        }
    }

    private void UpdateMisc() {
        myFireTime = myFireTime + Time.deltaTime;
        mySideFireTime = mySideFireTime + Time.deltaTime;
    }

    private void HandleShooting(bool isShooting) {
        if (isShooting) {
            //camera.SendMessage("BeginShortVerticalShake", 0.0025f);

            StartCoroutine("Shoot");
            if(canSideFire && mySideFireTime > nextSideFire) {
                StartCoroutine("ShootSideBullet");
                mySideFireTime = 0.0f;
            }
            
            myFireTime = 0.0f;
        }
    }

    IEnumerator Shoot() {
        if(canFireDouble) {
            PlayerBullet playerBulletObj = Instantiate(playerBullet, transform.position + new Vector3(0.025f, 0.035f, 0f), Quaternion.identity);
            playerBulletObj.Init(damage);

            playerBulletObj = Instantiate(playerBullet, transform.position + new Vector3(-0.012f, 0.035f, 0f), Quaternion.identity);
            playerBulletObj.Init(damage);
        } else {
            PlayerBullet playerBulletObj = Instantiate(playerBullet, transform.position + new Vector3(Random.Range(-0.001f, 0.003f), 0.035f, 0f), Quaternion.identity);
            playerBulletObj.Init(damage);
        }

        audio[0].pitch = Random.Range(0.975f, 1.025f);
        audio[0].Play();

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator ShootSideBullet() {
        PlayerBullet playerBulletObj = Instantiate(sideBulletLeft, transform.position + new Vector3(-0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, 25f));
        playerBulletObj.speed = 1f;
        playerBulletObj.Init(damage);

        playerBulletObj = Instantiate(sideBulletRight, transform.position + new Vector3(0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, -25f));
        playerBulletObj.speed = 1f;
        playerBulletObj.Init(damage);

        if (sideFireBullets >= 4) {
            playerBulletObj = Instantiate(sideBulletLeft, transform.position + new Vector3(-0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, 5f));
            playerBulletObj.sideFloat = -0.2f;
            playerBulletObj.speed = 1f;
            playerBulletObj.Init(damage);

            playerBulletObj = Instantiate(sideBulletRight, transform.position + new Vector3(0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, -5f));
            playerBulletObj.sideFloat = 0.2f;
            playerBulletObj.speed = 1f;
            playerBulletObj.Init(damage);
        }

        if (sideFireBullets >= 6) {
            playerBulletObj = Instantiate(sideBulletLeft, transform.position + new Vector3(-0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, 40f));
            playerBulletObj.sideFloat = -1.7f;
            playerBulletObj.speed = 1f;
            playerBulletObj.Init(damage);

            playerBulletObj = Instantiate(sideBulletRight, transform.position + new Vector3(0.003f, -0.001f, 0f), Quaternion.Euler(0f, 0f, -40f));
            playerBulletObj.sideFloat = 1.7f;
            playerBulletObj.speed = 1f;
            playerBulletObj.Init(damage);
            
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void HandleMovement() {
        /* HORIZONTAL MOVEMENT */
        //float dx = HandleHorizontalMovement(Input.GetAxisRaw("Horizontal") != 0);
        float dx = HandleHorizontalMovement(cromeController.CromeHorizontal() != 0f);

        /* VERTICAL MOVEMENT */
        //float dy = HandleVerticalMovement(Input.GetAxis("Vertical") != 0);
        float dy = HandleVerticalMovement(cromeController.CromeVertical() != 0f);

        /* UPDATE RB */
        rb.velocity = new Vector2(dx, dy);
    }

    private float HandleHorizontalMovement(bool isHorizontal) {
        float dx = rb.velocity.x;

        if (isHorizontal) {
            dx = speed * cromeController.CromeHorizontal();
        }

        return dx;
    }

    private float HandleVerticalMovement(bool isVertical) {
        float dy = rb.velocity.y;

        if (isVertical) {
            dy = speed * cromeController.CromeVertical();
        }

        return dy;
    }

    /* OTHER */

    public void GainExperience(int xp) {
        if (currentLevel <= 14) {
            experience += xp;

            if (experience >= 10) {
                experience = experience - 10;
                HandleLevelUp();
            }

            experienceBar.UpdateExperience(experience);
        }
    }

    private void HandleLevelUp() {
        currentLevel++;
        levelUpText.StartBlinking(currentLevel);

        switch(currentLevel) {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                nextFire -= 0.005f;
                break;
            case 14:
                nextFire -= 0.015f;
                break;
            case 15:
                canFireDouble = true;
                experienceBar.WipeExperience();
                break;
            default:
                break;
        }
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

            audio[1].Play();            

            if (!debugMode) {
                playerLivesUI.DecrementLives();
                lives--;
            }

            box.enabled = false;
            isDead = true;

            GameObject.FindObjectOfType<ScoreKeeper>().ResetMultiplier();

            ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
            explosionManager.AddExplosions(gameObject.transform.position);

            if (lives > 0) {
                sr.enabled = false;
                tr.enabled = false;
                transform.position = respawnPoint;
                StartCoroutine("Respawn");
            } else {
                HandlePlayerOutOfLives();
                //StartCoroutine("Restart");
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

    private void HandlePlayerOutOfLives() {
        ScoreKeeper scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
        scoreKeeper.PostScore();

        GameObject.FindObjectOfType<ArcadeMode>().HandleGameOver(scoreKeeper.GetTextScore());

        sr.enabled = false;
    }

    IEnumerator Restart() {
        sr.enabled = false;

        if(!debugMode) {
            GameObject.FindObjectOfType<ScoreKeeper>().PostScore();
        }
        
        Destroy(cromeController);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("main");
    }
}