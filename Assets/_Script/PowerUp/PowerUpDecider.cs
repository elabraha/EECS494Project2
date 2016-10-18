using UnityEngine;
using System.Collections;

public class PowerUpDecider : MonoBehaviour {

	public bool isleftPowerUp;
	// Use this for initialization
	void Start () {
		float rnd_v = 0; //Random.value;
		if (rnd_v > 0.5f) {
			isleftPowerUp = true;
		} else {
			isleftPowerUp = false;
		}
	}
}
