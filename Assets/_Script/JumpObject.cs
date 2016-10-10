using UnityEngine;
using System.Collections;

public class JumpObject : MonoBehaviour {

	public float jumpspeed = 1000000.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		
		if (other.gameObject.tag == "Player") {
			PlayerControl.S.getRigidBody ().velocity = 10f * Vector3.up;
		}
	}
}
