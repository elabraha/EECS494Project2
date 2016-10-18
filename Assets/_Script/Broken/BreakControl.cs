using UnityEngine;
using System.Collections;

public class BreakControl : MonoBehaviour {

	public bool isBreak = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Evil") {
			isBreak = true;
			this.transform.FindChild ("Cube").gameObject.SetActive (false);
			Destroy (gameObject.GetComponent<BoxCollider> ());
			foreach (Rigidbody rgd in gameObject.GetComponentsInChildren<Rigidbody>()) {
				rgd.constraints = RigidbodyConstraints.None;
				rgd.velocity = new Vector3 (Random.value * 10f - 5f, Random.value, Random.value* 10f - 5f);
			}
		}
	}
}
