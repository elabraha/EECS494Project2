using UnityEngine;
using System.Collections;

public class HideFirstFruits : MonoBehaviour {

	public bool isActive;
	// Use this for initialization
	void Awake () {
		gameObject.SetActive (isActive);
	}
}
