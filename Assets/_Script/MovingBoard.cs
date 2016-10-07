using UnityEngine;
using System.Collections;

// This is just a really basic moving boards
public class MovingBoard : MonoBehaviour {

	public Quaternion		movingDirection = Quaternion.Euler(0,0,0);
	public float 			movingSpeed = 2f;
	public float 			movingRadius = 5f;

	public bool 			____________________;

	private Rigidbody 		rigid;
	private Vector3 		startPoint;
	private bool 			isGoBack;
	private Vector3			directionCheck;

	void Start () {
		rigid = gameObject.GetComponent<Rigidbody> ();
		startPoint = gameObject.transform.position;
		isGoBack = false;
		directionCheck = movingDirection * Vector3.right;
	}
	

	void FixedUpdate () {
		Vector3 distanceToStartVec = gameObject.transform.position - startPoint;

		rigid.velocity = movingDirection * Vector3.right * movingSpeed;
		if (distanceToStartVec.magnitude > movingRadius) {
			if (!isGoBack && directionCheck.x * distanceToStartVec.x > 0f
				|| isGoBack && directionCheck.x * distanceToStartVec.x < 0f) {
				movingSpeed *= -1;
				isGoBack = !isGoBack;
			}
		}
	}
}
