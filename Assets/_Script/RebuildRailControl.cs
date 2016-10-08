using UnityEngine;
using System.Collections;

public class RebuildRailControl : MonoBehaviour {

	public bool isActive;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (isActive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
