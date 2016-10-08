using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LivesControl : MonoBehaviour {

	public List<GameObject> listObjects;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name == "Lives" && this.gameObject.GetComponentsInChildren<Collider> ().Length == 0) {
			foreach(GameObject item in listObjects){
				item.gameObject.SetActive(true);
			}
		}
	}
}
