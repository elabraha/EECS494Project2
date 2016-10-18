﻿using UnityEngine;
using System.Collections;

public class PointDetector : MonoBehaviour {

	public int pos;
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			AppearingFinishing.S.positionToShow = (pos + 2) % 4;
			PlayerControl.S.enterWeakPowerUp ();
			AppearingFinishing.S.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerControl.S.exitWeakPowerUp ();
			PlayerControl.S.exitPowerUp ();
		}
	}
}