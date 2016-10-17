using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private Rigidbody rigid;
	public float speed = 500.0f;
	public static PlayerControl S;
	public float jumpspeed = 350000.0f;
	public bool IsGrounded = true;
	public bool canWin;
	bool jumWasPressed = false;
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
		if (transform.position.y <= -100) {
			
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_1_Begin") {
				transform.position = new Vector3 (0f, 21f, 0f);
			} else if (sceneName == "_Scene_1st_Level") {
				transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
			}
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero; 
		}

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

		//this is so you can hold don't down space and still jump

		if(Input.GetKeyUp(KeyCode.Space)) {
			jumWasPressed = false;
		}

		if(Input.GetKey (KeyCode.Space) && !IsGrounded) {
			jumWasPressed = true;
		}

		if ((Input.GetKeyDown (KeyCode.Space) || jumWasPressed) && IsGrounded) {
			Vector3 jump = Vector3.up;
			rigid.AddForce (jump * jumpspeed * Time.deltaTime);
			jumWasPressed = false;
		}
		//end jump code

		if (Input.GetKey (KeyCode.C)) {
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_1_Begin") {
				transform.position = new Vector3 (0, 21, 0);
			} else if (sceneName == "_Scene_1st_Level") {
				transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
			}
		}

		if (Input.GetKey (KeyCode.R)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1st_Level");
		}
		if (Input.GetKey (KeyCode.F)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1_Begin");
		}

		//jumping = false;
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.gameObject.tag == "Fruit") {
			Destroy (collisionInfo.gameObject);
		}
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

	public Rigidbody getRigidBody(){
		return rigid;
	}
}
