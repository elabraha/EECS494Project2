using UnityEngine;
using System.Collections;

public class TargetBall : MonoBehaviour {

//	public bool is_AtTarget = false;
//	public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		if (is_AtTarget) {
//			WinnerStage.S.canWin = true;
//		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			PlayerControl.S.canWin = true;
		}
	}
}
