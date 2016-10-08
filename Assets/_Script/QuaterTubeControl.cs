using UnityEngine;
using System.Collections;

public class QuaterTubeControl : MonoBehaviour {

	public bool isActivate;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (isActivate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
