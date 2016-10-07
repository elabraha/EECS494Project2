using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Rigidbody rigid;
	public float speed = 10f;
	public static Player S;

	// Use this for initialization
	void Start () {
		S = this;
		rigid = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
