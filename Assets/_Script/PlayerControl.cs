using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private Rigidbody rigid;
	public float speed = 200.0f;
	public static PlayerControl S;
	public float jumpspeed = 15.0f;
	public bool IsGrounded = true;
	public bool canWin;

	//EVIL:
	public GameObject evil;
	private float evilRadius;

	//POWER_UP : These are variables for powerUp
	public bool isPowerUpMovingJumping = false;
	public float powerUpStartTime;
	public float powerUpDuration = 1000f; // this may be more complicated later, but just assume a fixed duration first
	public float speedFactor;
	public float jumpForceFactor;
	public int numPowerUpMovingJumping;

	//EVIL:
	private bool isBrokenByEvil;
	private float brokenBegin;
	private float brokenDuration = 2f;

	bool jumWasPressed = false;
	private const float BONUS_GRAV = 9.8f;
	// Use this for initialization

	void Awake(){
		S = this;
		rigid = GetComponent<Rigidbody> ();
		// POWER_UP : Set the Glowing object false
		this.transform.FindChild ("Glow").gameObject.SetActive (false);
		this.transform.FindChild ("Glow_weak").gameObject.SetActive (false);

		numPowerUpMovingJumping = 0;
	}

	void Start () {
		//TEST:
		evil.SetActive(false);
		evilRadius = evil.GetComponent<SphereCollider> ().radius;
		this.transform.parent.FindChild ("brokenPlayer").gameObject.SetActive (false);
		isBrokenByEvil = false;
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
			} else if (sceneName == "_Scene_Custom") {
				transform.position = new Vector3 (0.0f, 2.2f, -18.7f);
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

		if (!IsGrounded) {
			//print (rigid.velocity);
			Vector3 vel = rigid.velocity;
			vel.y-=BONUS_GRAV * Time.deltaTime;
			rigid.velocity=vel;

			//TO DO: figure out how to make this decrease forward force over time
		}

		if(Input.GetKey (KeyCode.Space) && !IsGrounded) {
			jumWasPressed = true;
		}
		//FIX ME: make jumps faster
		if ((Input.GetKeyDown (KeyCode.Space) || jumWasPressed) && IsGrounded) {
			print ("bang bang");
			Vector3 jump = Vector3.up;
			rigid.velocity += jump * jumpspeed;
		}
		//end jump code

		// TEST_CHEAT 1
		if (Input.GetKey (KeyCode.F4)) {
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

		// POWER_UP : Add some additional Gravity when power up
//		if(isPowerUpMovingJumping){
//			rigid.AddForce(Vector3.down * rigid.mass * 9.8f, ForceMode.Force);
//		}
		if(Input.GetKey(KeyCode.E) && numPowerUpMovingJumping > 0){
			numPowerUpMovingJumping--;
			enterPowerUp ();
		}
		// POWER_UP : Exit if time is up
		if(Time.time - powerUpStartTime > powerUpDuration && isPowerUpMovingJumping && isPowerUpMovingJumping){
			exitPowerUp ();	
		}

		//EVIL: 
		this.transform.parent.FindChild("brokenPlayer").transform.position 
			= this.gameObject.transform.position;

		//TEST: this is just code for test
		if(Input.GetKey(KeyCode.F3)){
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

		//TEST_CHEAT 2

		if (Input.GetKey (KeyCode.F1)) {
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_Custom") {
				this.transform.parent.gameObject.transform.position = new Vector3 (75.53f, -38.74f, 79.8f);
			}
		} 
		if (Input.GetKey (KeyCode.F2)) {
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_Custom") {
				this.transform.parent.gameObject.transform.position = new Vector3 (216.3f, -29.9f, 79.8f);
			}
		}

		//Test_CHEAT 3
		if (Input.GetKey (KeyCode.F5)) {
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_Custom") {
				this.transform.parent.gameObject.transform.position = new Vector3 (451.07f, 62f, 89.8f);
			}
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.gameObject.tag == "Fruit") {
			Destroy (collisionInfo.gameObject);
		}

		if (collisionInfo.gameObject.tag == "Evil" 
			&& this.transform.position.y < collisionInfo.gameObject.transform.position.y) {
			brokenBegin = Time.time;
			isBrokenByEvil = true;
			BrokenByEvil (collisionInfo);
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

	//Here is some public/private functions 
	public Rigidbody getRigidBody(){
		return rigid;
	}

	//POWER_UP : When power up exits
	public void exitPowerUp(){
		isPowerUpMovingJumping = false;
		PlayerControl.S.jumpspeed /= jumpForceFactor;
		PlayerControl.S.speed /= speedFactor;
		// POWER_UP : Set the Glowing object false
		this.transform.FindChild ("Glow").gameObject.SetActive (false);
	}

	//POWER_UP : When player chooses to use it
	public void enterPowerUp(){
		//rigid.AddForce(Vector3.down * rigid.mass * 9.8f, ForceMode.Force);
		this.transform.FindChild ("Glow").gameObject.SetActive (true);
		powerUpStartTime = Time.time; 
		isPowerUpMovingJumping = true;
		PlayerControl.S.jumpspeed *= jumpForceFactor;
		PlayerControl.S.speed *= speedFactor;

	}

	//POWER_UP : When power up exits
	public void exitWeakPowerUp(){
		PlayerControl.S.jumpspeed /= 2f;
		// POWER_UP : Set the Glowing object false
		this.transform.FindChild ("Glow_weak").gameObject.SetActive (false);
	}

	//POWER_UP : When player chooses to use it
	public void enterWeakPowerUp(){
		//rigid.AddForce(Vector3.down * rigid.mass * 9.8f, ForceMode.Force);
		this.transform.FindChild ("Glow_weak").gameObject.SetActive (true);
		PlayerControl.S.jumpspeed *= 2f;
	}



	public void BrokenByEvil(Collision other){
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

// 75.53, -38.74, 79.8
// 216.3, -29.9, 79.8
