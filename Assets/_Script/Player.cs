using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody rigid;

	// Use this for initialization
	void Start () {
		rigid = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			
		}
	}
}
