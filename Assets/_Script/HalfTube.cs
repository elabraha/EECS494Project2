using UnityEngine;
using System.Collections;

public class HalfTube : MonoBehaviour {
	public bool isActive;
	// Use this for initialization
	void Awake () {
		gameObject.SetActive (isActive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
