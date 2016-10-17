using UnityEngine;
using System.Collections;

public class BreakPlayer : MonoBehaviour {

	//public GameObject evil;

	void Start(){
//		this.transform.FindChild ("brokenPlayer").gameObject.SetActive (false);
//		//TEST: this is just code for test
//		GameObject.Find ("Evil").SetActive(false);
	}

	void FixedUpdate(){
//		this.transform.position = this.transform.FindChild ("Player").gameObject.transform.position;
//		//TEST: this is just code for test
//		if(Input.GetKey(KeyCode.E)){
//			float _y = evil.gameObject.transform.position.y;
//			evil.SetActive(true);
//			evil.transform.position = new Vector3 (this.gameObject.transform.position.x, _y, this.transform.position.z);
//		}
	}

	void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "Evil" 
//			&& other.gameObject.transform.position.y > this.transform.position.y) {
//			this.transform.FindChild ("brokenPlayer").gameObject.SetActive (true);
//			this.transform.FindChild ("brokenPlayer").gameObject.transform.position 
//				= this.transform.FindChild ("Player").gameObject.transform.position;
//			Destroy (this.transform.FindChild ("Player").gameObject.GetComponent<SphereCollider>());
//			Destroy (this.gameObject.GetComponent<SphereCollider>());
//			this.transform.FindChild ("Player").gameObject.SetActive (false);
//			foreach (Rigidbody rgd in this.transform.FindChild("brokenPlayer").gameObject.GetComponentsInChildren<Rigidbody>()) {
//				rgd.constraints = RigidbodyConstraints.None;
//				rgd.velocity = new Vector3 (Random.value * 24f - 12f, Random.value, Random.value* 24f - 12f);
//			}
//		}
	}
}
