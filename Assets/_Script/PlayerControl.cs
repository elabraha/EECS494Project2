using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private Rigidbody rigid;
	public float speed = 10.0f;
	public static PlayerControl S;
	public float jumpspeed = 10000.0f;
	public bool IsGrounded = true;
	// Use this for initialization
	void Start () {
		S = this;
		rigid = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		//float jump = Input.GetAxis("Vertical");

		//what part is jump depends on the gravity lock

		//print (rigid); 


		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		//print (movement);

		rigid.AddForce(movement * speed * Time.deltaTime, ForceMode.Impulse);

		if (Input.GetKeyDown (KeyCode.UpArrow) && IsGrounded) {
			Vector3 jump = Vector3.up;
			rigid.AddForce (jump * jumpspeed * Time.deltaTime);
		}
		//jumping = false;
	}

	void OnCollisionStay (Collision collisionInfo)
	{
		if(collisionInfo.gameObject.name == "Floor"){
			IsGrounded = true;  
		}
	}

	void OnCollisionExit (Collision collisionInfo)
	{
		if(collisionInfo.gameObject.name == "Floor"){
			IsGrounded = false;
		}
	}
}
