using System.Collections;
using UnityEngine;

public class ChargingObject : MonoBehaviour {

    public GameObject chargeShot;

    public float stage2WaitTime = 2f;
    public float stage3WaitTime = 3f;

    private int stage = 1;
    private bool isFiring = false;
    private Animator animator;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        StartCoroutine("ChangeStage");
	}

    IEnumerator ChangeStage() {
        yield return new WaitForSeconds(stage2WaitTime);
        TransitionToNextStage(2);

        yield return new WaitForSeconds(stage3WaitTime);
        TransitionToNextStage(3);
    }

    private void TransitionToNextStage(int stageNumber) {
        if (!isFiring) {
            audio.pitch += 0.1f;
            audio.Play();
            animator.SetBool("isStage" + stageNumber.ToString(), true);
            transform.position = transform.position + new Vector3(0f, 0.025f, 0f);
            stage = stageNumber;
        }
    }

    public void FireChargeShot(int playerLevel) {
        ChargeShot chargeShotObj = Instantiate(chargeShot, transform.position, Quaternion.identity).GetComponent<ChargeShot>();
        chargeShotObj.Init(stage, playerLevel);

        if(stage == 3) {
            SteamAchievements steamAchievements = GameObject.FindObjectOfType<SteamAchievements>();
            if(steamAchievements != null) {
                steamAchievements.SetSuperCharged();
            }
        }

        Destroy(gameObject);
    }

}
