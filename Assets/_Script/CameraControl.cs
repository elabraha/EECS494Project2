using UnityEngine;
using System.Collections;
using UnityEditor;

public class CameraControl : MonoBehaviour {

	private Vector3 		relativePosition;
	private float			currentX = 0.0f;
	private float			currentY = 0.0f;
	private float			sensitivityX = 4.0f;
	private float			sensitivityY = 1.0f;
	public static 			CameraControl C;
	private const float 	Y_ANGLE_MIN = -25.0f;
	private const float 	Y_ANGLE_MAX = 25.0f;


	void Start (){
		relativePosition = transform.position - PlayerControl.S.gameObject.transform.position;
	}
	//source: 
	private void Update() {
		currentX += Input.GetAxis ("Mouse X") * sensitivityX;
		currentY += Input.GetAxis ("Mouse Y") * sensitivityY;

		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}
		

	void LateUpdate (){

		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		transform.position = PlayerControl.S.gameObject.transform.position + rotation * relativePosition;
		transform.LookAt (PlayerControl.S.gameObject.transform.position);
	}
}