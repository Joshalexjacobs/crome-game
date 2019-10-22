using System.Collections;
using UnityEngine;

public class ChargeShotAOE : MonoBehaviour {

    private float radius = 0.5f;
    private float damage = 1f;
    private GameObject camera;

    void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        HandleSplashDamage();
        StartCoroutine("DeathTimer");
	}
	
    public void Init(float radius, float damage) {
        this.radius = radius;
        this.damage = damage;
    }

    IEnumerator DeathTimer() {
        camera.SendMessage("BeginMedVerticalShake", 0.0045f);
        yield return new WaitForSeconds(0.292f);

        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 1, true, -0.1f, 0.1f);

        Destroy(gameObject);
    }

    private void HandleSplashDamage() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.tag == "Enemy") {
                collider.gameObject.SendMessage("Damage", damage);
            } else if (collider.gameObject.tag == "Shield") {
                collider.GetComponentInParent<Shielder>().SendMessage("Damage", damage);
            }
        }
    }

}
