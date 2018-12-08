using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingObject : MonoBehaviour {

    public GameObject chargeShot;

    public float stage2WaitTime = 2f;
    public float stage3WaitTime = 3f;

    private int stage = 1;
    private bool isFiring = false;
    private Animator animator;

	// Use this for initialization
	void Start () {
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
            animator.SetBool("isStage" + stageNumber.ToString(), true);
            stage = stageNumber;
        }
    }

    public void FireChargeShot() {
        ChargeShot chargeShotObj = Instantiate(chargeShot, transform.position, Quaternion.identity).GetComponent<ChargeShot>();
        chargeShotObj.Init(stage);
        Destroy(gameObject);
    }

}
