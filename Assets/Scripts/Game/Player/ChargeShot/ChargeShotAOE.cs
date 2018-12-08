using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotAOE : MonoBehaviour {

    private float radius = 0.5f;
    private float damage = 5f;

	void Start () {
        HandleSplashDamage();
        StartCoroutine("DeathTimer");
	}
	
    public void Init(float radius, float damage) {
        this.radius = radius;
        this.damage = damage;
    }

    IEnumerator DeathTimer() {
        yield return new WaitForSeconds(0.292f);
        Destroy(gameObject);
    }

    private void HandleSplashDamage() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.tag == "Enemy") {
                collider.gameObject.SendMessage("Damage", damage / 3);
            }
        }
    }

}
