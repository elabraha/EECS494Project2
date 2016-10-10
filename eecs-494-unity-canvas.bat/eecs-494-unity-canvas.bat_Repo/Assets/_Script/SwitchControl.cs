using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchControl : MonoBehaviour {

	public List<GameObject> toBuild;
	public GameObject toDestroy; 
	public List<GameObject> toRebuild;  

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			showBridge ();
		}
	}

	public void showBridge(){
		foreach (GameObject item in toBuild) {
			item.gameObject.SetActive (true);
		}
		Destroy (toDestroy);
		foreach (GameObject item in toRebuild) {
			item.gameObject.SetActive (true);
		}
		//do something else
	}
}
