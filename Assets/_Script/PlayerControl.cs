using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private Rigidbody rigid;
	public float speed = 20.0f;
	public static PlayerControl S;
	public float jumpspeed = 10000.0f;
	public bool IsGrounded = true;
	// Use this for initialization

	void Awake(){
		S = this;
		rigid = GetComponent<Rigidbody> ();
	}

	void Start () {
		
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//float jump = Input.GetAxis("Vertical");

		//what part is jump depends on the gravity lock

		//print (rigid); 


		Vector3 movementHorizontal = new Vector3(moveHorizontal, 0.0f, 0.0f);
		Vector3 movementVertical = new Vector3(0.0f, 0.0f, moveVertical);

		//print (movement);
		if(Input.GetKey(KeyCode.LeftShift)){
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
		else{
			rigid.AddForce(movementHorizontal * speed * Time.deltaTime, ForceMode.Impulse);
			rigid.AddForce(movementVertical * speed * Time.deltaTime, ForceMode.Impulse);
		}

		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded) {
			Vector3 jump = Vector3.up;
			rigid.AddForce (jump * jumpspeed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.C)) {
			transform.position = new Vector3 (0, 21, 0);
		}

		//jumping = false;
	}

	void OnCollisionStay (Collision collisionInfo)
	{
		if(collisionInfo.gameObject.tag == "Floor"){
			IsGrounded = true;  
		}
	}

	void OnCollisionExit (Collision collisionInfo)
	{
		if(collisionInfo.gameObject.tag == "Floor"){
			IsGrounded = false;
		}
	}
}
