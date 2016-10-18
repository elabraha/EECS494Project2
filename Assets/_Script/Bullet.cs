using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	Rigidbody rb;
	public float speed;
	//public bool shoot = false;

	void Start () {
		this.rb = GetComponent <Rigidbody>();
	}
		

	// Update is called once per frame
	void Update () {
		rb.velocity = transform.forward * speed * Time.deltaTime;
	}

	void OnBecameInvisible () {
		print ("destroy");
		Destroy (this.gameObject);
	}
}
