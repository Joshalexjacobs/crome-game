using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour {

    public GameObject bullet;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "PlayerBullet") {
            //Bullet bulletObj = Instantiate(bullet, collision.transform.position, Quaternion.identity).GetComponent<Bullet>();
            //bulletObj.Init(new Vector2(Random.Range(-0.5f, 0.5f), -1.0f));
        }
    }
}
