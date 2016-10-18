using UnityEngine;
using System.Collections;

public class EvilControlOnBoard: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			PlayerControl.S.evil.SetActive (true);
		}
	}
}
