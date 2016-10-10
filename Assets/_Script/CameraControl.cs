using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Vector3 		relativePosition;

	void Start (){
		relativePosition = transform.position - PlayerControl.S.gameObject.transform.position;
	}

//	void Update() {
//		Vector3 turn_radius = PlayerControl.S.gameObject.transform.forward;
//		Vector3 relative_turn = (turn_radius - transform.position);
//		Quaternion rotation = Quaternion.LookRotation (relative_turn);
//		this.gameObject.transform.rotation = rotation;
//	}

	void LateUpdate (){
		this.gameObject.transform.position = PlayerControl.S.gameObject.transform.position + relativePosition;
	}
}