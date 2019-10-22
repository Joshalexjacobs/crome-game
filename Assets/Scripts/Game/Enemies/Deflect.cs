using UnityEngine;

public class Deflect : MonoBehaviour {

    public GameObject bullet;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "PlayerBullet") {
            ChargeShot chargeShot = collision.GetComponent<ChargeShot>();

            if(chargeShot != null) {
                GetComponentInParent<Shielder>().Damage(4);
            }
        }
    }
}
