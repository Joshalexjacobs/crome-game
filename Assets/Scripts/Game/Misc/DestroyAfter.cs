using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

    public float deathTime;

	// Use this for initialization
	void Start () {
        StartCoroutine("Death");
	}
	
    IEnumerator Death() {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
