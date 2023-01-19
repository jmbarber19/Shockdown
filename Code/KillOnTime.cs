using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTime : MonoBehaviour {
	public float killTime = 5f;
	// Update is called once per frame
	void Update () {
		killTime -= Time.deltaTime;
		if (killTime < 0f) {
			Destroy(this.gameObject);
		}
	}
}
