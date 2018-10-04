using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /* GLOBALS */
    //public const string CHARGESHOT = "grey";
    //public const string HONING_MISSILE = "red";

    /* PUBLIC VARS */

    //public int lives = 3;
    //public float speed = 5f;
    //public float maxSpeed = 5f;
    //public float warpSpeed = 5f;
    //public float nextWarp = 0.2f;
    //public Laser laserOne;
    //public Laser laserTwo;
    //public ChargeShot chargeShotScript;
    //public HoningBeam honingBeamScript;
    //public GameObject honingStream;
    //public float nextHoningStream = 2f;
    //public float nextHoningBeam = 0.3f;
    //public float nextFire = 0.2f; // 0.2f
    //public float nextFireMin = 0.1f;
    //public float camShake = 0.005f;
    //public float chargeMax = 10f;
    //public float energy = 100f;
    //public float honingEnergy = 100f;
    //public float energyMax = 100f;
    //public float analogStickDeadZone = 0.2f;
    //public GameObject playerFade;
    //public GameObject blank;

    //public float nextPause = 0.1f;
    //public float lastDeathMAX = 2.5f;
    //public float lastDeath = 0f;

    //// player will have a primary and secondary passive upgrade
    //// primary: upgraded at 10/30/50 points (pea shooter++)
    //// secondary: upgraded at 15/35 points (honingStream)

    //private int[] levels = new int[] { 10, 20, 30, 40, 60 };
    //private int[] maxUpgradePoints = new int[] { 0, 10, 20, 30, 40 };

    //public int currentLevel = 0;
    //public int currentUpgradePoints = 0;

    //public float damage = 1.0f;
    //public float honingStreamDamage = 0.5f;

    ///* PRIVATE VARS */

    //private Animator animator;
    //private BoxCollider2D box;
    //private CircleCollider2D circle;
    //private SpriteRenderer sr;

    //private float charge = 0.0f;
    //private float myFireTime = 0.0f;
    //private float myWarpTime = 0.0f;

    //private float myPause = 0.0f;

    //private bool firstWarp = true;
    //private bool safeWarp = false;

    //private float warpStartTime;
    //private Vector3 warpHere;
    //private Vector3 warpStartPosition;
    //private float warpDistance;

    //private bool fireRight = false;
    //private bool isReady = false;
    //private bool isCharging = false;
    //private bool isWarping = false;
    //private bool canFireHoningStream = true;
    //private bool canFireHoningBeam = true;
    //private bool canWarpDash = true;
    //private bool canSwitchSpecial = true;
    //private bool firstSpawn = true;
    //private bool isPaused = false;
    //private int bulletCount = 1;
    //private string currentSpecial = "grey";
    //private EnergyBarUI energyBarUI;
    //private GameObject camera;
    //private AudioSource[] sounds;

    //private LivesTextMesh livesTextMesh;

    /* PUBLIC VARS */

    public float speed = 5f;
    public float damage = 1f;

    /* PUBLIC OBJECTS */

    public GameObject playerTrailParticle;
    public PlayerBullet playerBullet;

    /* PRIVATE VARS */

    private bool isDead = false;
    private bool isMoving = false;

    private float myFireTime = 0.0f;
    private float nextFire = 0.2f;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

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
        PlayerBullet playerBulletObj = Instantiate(playerBullet, transform.position + new Vector3(Random.Range(-0.001f, 0.001f), 0.035f, 0f), Quaternion.identity);
        playerBulletObj.Init(damage);

        yield return new WaitForSeconds(0.1f);
    }

    private void HandleMovement()
    {
        /* HORIZONTAL MOVEMENT */
        float dx = HandleHorizontalMovement(Input.GetAxisRaw("Horizontal") != 0);

        /* VERTICAL MOVEMENT */
        float dy = HandleVerticalMovement(Input.GetAxis("Vertical") != 0);

        /* UPDATE RB */
        rb.velocity = new Vector2(dx, dy);

        //if(dx != 0f || dy != 0f) {
        //    isMoving = true;
        //} else {
        //    isMoving = false;
        //}
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

    IEnumerator SpawnPlayerTrailParticles() {
        while(!isDead) {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

            //if(isMoving) {
                Instantiate(playerTrailParticle, transform.position + new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.005f, 0.005f), 0f), Quaternion.identity);
            //}
        }
    }
}
