using UnityEngine;
using System.Collections;

public class WinnerStage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		// Do something to show the player passes the instruction level
		Debug.Log("Player Win the instruction level!");
	}
}
