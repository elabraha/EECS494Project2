using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Vector3 		relativePosition;

	void Start (){
		relativePosition = transform.position - PlayerControl.S.gameObject.transform.position;
	}

	void LateUpdate (){
		gameObject.transform.position = PlayerControl.S.gameObject.transform.position + relativePosition;
	}
}