using UnityEngine;
using System.Collections;
//using System;

public class PlayerControl : MonoBehaviour {

	public Rigidbody rigid;
	public float speed = 200.0f;
	public static PlayerControl S;
	public float jumpspeed = 15.0f;
	public bool IsGrounded = true;
	public bool canWin;

	//EVIL:
	public GameObject evil;
	private float evilRadius;

	//POWER_UP : These are variables for powerUp
	public bool isPowerUp = false;
	public float powerUpStartTime;
	public float powerUpDuration = 100f; // this may be more complicated later, but just assume a fixed duration first
	public float speedFactor;
	public float jumpForceFactor;
	public float bubbleJump;

	//EVIL:
	private bool isBrokenByEvil;
	private float brokenBegin;
	private float brokenDuration = 2f;

	bool jumWasPressed = false;
	private const float BONUS_GRAV = 9.8f;

	public PowerType powerup;

	Vector3 restartPos;
	Material mat;
	// Use this for initialization

	void Awake(){
		S = this;
		rigid = GetComponent<Rigidbody> ();
		// POWER_UP : Set the Glowing object false
		this.transform.FindChild ("Glow").gameObject.SetActive (false);
	}

	void Start () {
		//TEST:
		evil.SetActive(false);
		evilRadius = evil.GetComponent<SphereCollider> ().radius;
		this.transform.parent.FindChild ("brokenPlayer").gameObject.SetActive (false);
		isBrokenByEvil = false;
		restartPos = transform.position;
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
		restartPos = transform.position;
		mat = PlayerControl.S.GetComponent<Renderer> ().material;
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
		//float jump = Input.GetAxis("Vertical");

		//what part is jump depends on the gravity lock

		//print (rigid); 
		if (transform.position.y <= -100) { //TODO: Make this restart to the right start point by collision
			
//			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
//			if (sceneName == "_Scene_1_Begin") {
//				transform.position = new Vector3 (0f, 21f, 0f);
//			} else if (sceneName == "_Scene_1st_Level") {
//				transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
//			}
			transform.position = restartPos;
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero; 
		}

		Vector3 movementHorizontal = new Vector3(moveHorizontal, 0.0f, 0.0f);
		Vector3 movementVertical = new Vector3(0.0f, 0.0f, moveVertical);

		//print (movement);
		//TO DO: figure out how to use velocity instead. remember if I use velocity then must change
		//firction of material because it migth weird
		if(Input.GetKey(KeyCode.LeftShift)){
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
		else{
			rigid.AddForce(movementHorizontal * speed * Time.deltaTime, ForceMode.Impulse);
			rigid.AddForce(movementVertical * speed * Time.deltaTime, ForceMode.Impulse);
		}

		//this is so you can hold don't down space and still jump
		if(!isPowerUp || (powerup == PowerType.JUMP && powerup != PowerType.BUBBLE )) {
//			print("you should not be here if bubble");
//			print (powerup); 
			if(Input.GetKeyUp(KeyCode.Space)) {
				jumWasPressed = false;
			}
				

			if (!IsGrounded) {
				Vector3 vel = rigid.velocity;
				vel.y-=BONUS_GRAV*Time.deltaTime;
				rigid.velocity=vel;

				//TO DO: figure out how to make this decrease forward force over time
			}
				

			if(Input.GetKey (KeyCode.Space) && !IsGrounded) {
				jumWasPressed = true;
			}
			//FIX ME: make jumps faster
			if ((Input.GetKeyDown (KeyCode.Space) || jumWasPressed) && IsGrounded) {
//				print ("bang bang");
				Vector3 jump = Vector3.up;
				rigid.velocity += jump * jumpspeed;
			}
			//end jump code
		}
		if (powerup == PowerType.BUBBLE) {
			if (!IsGrounded) {
				Vector3 vel = rigid.velocity;
				vel.y-=BONUS_GRAV*Time.deltaTime;
				rigid.velocity=vel;
			}
			if(Input.GetKeyUp(KeyCode.Space)) {
				Vector3 jump = Vector3.up;
				rigid.AddForce(jump * bubbleJump);
			}
		}
		//TO DO: Change key codes to numbers
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

		//TO DO: add a way to do this without doing it directly in here. Also just fix gravity and velocity increases in general
		// POWER_UP : Add some additional Gravity when power up
//		if(isPowerUp){
//			rigid.AddForce(Vector3.down * rigid.mass * 9.8f, ForceMode.Force);
//		}


		// POWER_UP : Exit if time is up
		if(Time.time - powerUpStartTime > powerUpDuration && isPowerUp){
			exitPowerUp ();	
		}
		//EVIL: 
		this.transform.parent.FindChild("brokenPlayer").transform.position 
			= this.gameObject.transform.position;
		//TEST: this is just code for test
		if(Input.GetKey(KeyCode.E)){
			evil.SetActive(true);
			float _y = evil.gameObject.transform.position.y;
			evil.transform.position = new Vector3 (this.gameObject.transform.position.x, 
													_y, this.transform.position.z);
		}
		//EVIL:
		if (isBrokenByEvil) {
			this.transform.position = evil.transform.position - new Vector3(0f, evilRadius * 20f, 0f);
			if (Time.time - brokenBegin > brokenDuration) {
				string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
			}
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (isPowerUp && powerup == PowerType.BUBBLE) {
			if (collisionInfo.gameObject.tag == "Fruit") {
				Destroy (collisionInfo.gameObject);
			} 
			if (collisionInfo.gameObject.tag == "Floor") {
				//just do nothing
			} else {
				//everything destroys bubble for now except wind if I add it.
				Start();
			}
			return;
		}
		if (collisionInfo.gameObject.tag == "Fruit") {
			Destroy (collisionInfo.gameObject);
		}

		if (collisionInfo.gameObject.tag == "Evil") {
			brokenBegin = Time.time;
			isBrokenByEvil = true;
			BrokenByEnemy (collisionInfo);
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

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "CheckPoint") {
			restartPos = coll.gameObject.transform.GetChild(0).position + Vector3.up;
		}
		if (coll.gameObject.tag == "Fan") {
			print ("fan zone"); 
			float addon = 1.0f;
			if (rigid.mass < 1.0f) {
				addon = 0.2f;
			}
			rigid.AddForce(17000000.0f * addon * coll.gameObject.transform.forward, ForceMode.Force);
		}
	}

//	void OnTriggerStay(Collider coll) {
//		
//
//	}
	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Fan") {
			print ("out the zone");
		}
	}

	//Here is some public/private functions 
	public Rigidbody getRigidBody(){
		return rigid;
	}

	//POWER_UP : When power up exits
	public void exitPowerUp(){
		if (powerup == PowerType.JUMP) {
			PlayerControl.S.isPowerUp = false;
			PlayerControl.S.jumpspeed /= jumpForceFactor;
			PlayerControl.S.speed /= speedFactor;
			// POWER_UP : Set the Glowing object false
			PlayerControl.S.transform.FindChild ("Glow").gameObject.SetActive (false);
		} else {
			//do bubble shit
			//return mode
			rigid.mass = 10.0f;
			this.gameObject.GetComponent<Rigidbody> ().mass = 10.0f;
			PlayerControl.S.GetComponent<Renderer> ().material = mat;
			rigid.drag = 0.0f;
		}

	}

	public void BrokenByEnemy(Collision other){
		if (other.gameObject.tag == "Evil" 
			&& other.gameObject.transform.position.y > this.transform.position.y) {
			float radius = evil.GetComponent<SphereCollider> ().radius;
			this.transform.parent.FindChild ("brokenPlayer").gameObject.SetActive (true);
			this.transform.parent.FindChild ("brokenPlayer").gameObject.transform.position 
			= this.gameObject.transform.position;
			this.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			//this.gameObject.SetActive (false);
			Destroy (this.gameObject.GetComponent<SphereCollider> ());
			foreach (Rigidbody rgd in this.transform.parent.FindChild("brokenPlayer").gameObject.GetComponentsInChildren<Rigidbody>()) {
				rgd.constraints = RigidbodyConstraints.None;
				rgd.velocity = new Vector3 (Random.value * 36f - 18f, Random.value, Random.value* 36f - 18f);
			}
		}
	}
}
