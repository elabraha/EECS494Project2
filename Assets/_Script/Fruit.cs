using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Entre");
		if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
