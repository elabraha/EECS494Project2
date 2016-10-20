using UnityEngine;
using System.Collections;
//using System;
using System.Collections.Specialized;

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
	public bool isPowerUpMovingJumping = false;
	public float powerUpStartTime;
	public float powerUpDuration = 10.0f; // this may be more complicated later, but just assume a fixed duration first
	public float speedFactor;
	public float jumpForceFactor;
	public int numPowerUpMovingJumping;
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

	// Fruit Counter and Timer
	public int FruitNum;
	public float CurrentTime;
	public float LastStopTimePoint;

	void Awake(){
//		if (S == null)
//		{
//			DontDestroyOnLoad(this);
//			S = this;
////			PlayerPrefs.SetFloat("PlayerX", transform.position.x);
////			PlayerPrefs.SetFloat("PlayerY", transform.position.y);
////			PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
//		}
//		else if (S != this)
//		{
//			Destroy (this);
//			return;
//		}
		if (S == null) {
			S = this;
		} else {
			Debug.Log ("two players");
			return;
		}
		rigid = GetComponent<Rigidbody> ();
		// POWER_UP : Set the Glowing object false
		this.transform.FindChild ("Glow").gameObject.SetActive (false);
		this.transform.FindChild ("Glow_weak").gameObject.SetActive (false);
		numPowerUpMovingJumping = 0;
		restartPos = transform.position;
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
		print ("position" + restartPos);
		// Fruit Counter and Timer
		FruitNum = 0;
		LastStopTimePoint = 0f;
	}

	void Start () {
		//TEST:
		evil.SetActive(false);
		evilRadius = evil.GetComponent<SphereCollider> ().radius;
		this.transform.parent.FindChild ("brokenPlayer").gameObject.SetActive (false);
		isBrokenByEvil = false;
		isPowerUpMovingJumping = false;
		//restartPos = transform.position;
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;

		if (PlayerPrefs.HasKey ("PlayerX")) {

			Vector3 newPosition = new Vector3 (0, 0, 0);
			newPosition.x = PlayerPrefs.GetFloat ("PlayerX");
			newPosition.y = PlayerPrefs.GetFloat ("PlayerY");
			newPosition.z = PlayerPrefs.GetFloat ("PlayerZ");
			restartPos = newPosition;
		}

		transform.position = restartPos;
		PlayerPrefs.SetFloat("PlayerX", transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
		print ("position" + restartPos);

		mat = GetComponent<Renderer> ().material;
		if (PlayerPrefs.HasKey ("Timer")) {
			LastStopTimePoint = PlayerPrefs.GetFloat ("Timer");
			CurrentTime = PlayerPrefs.GetFloat ("Timer");
		} else {
			LastStopTimePoint = Time.time;
			CurrentTime = Time.time;
		}
		FruitNum = 0;
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
//				if (isPowerUpMovingJumping) {
//					exitPowerUp ();
//					++numPowerUpMovingJumping;
//				}
//				//transform.position = new Vector3 (0f, 21f, 0f);
//			} else if (sceneName == "_Scene_1st_Level") {
//				//transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
//				if (isPowerUpMovingJumping) {
//					exitPowerUp ();
//					++numPowerUpMovingJumping;
//				}
//			} else if (sceneName == "_Scene_Custom") {
//				//transform.position = new Vector3 (0.0f, 2.2f, -18.7f);
//				if (isPowerUpMovingJumping) {
//					exitPowerUp ();
//					++numPowerUpMovingJumping;
//				}
//			}

			if (isPowerUpMovingJumping) {
				exitPowerUp ();
				++numPowerUpMovingJumping;
			}
//			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
//			if (sceneName == "_Scene_1_Begin") {
//				transform.position = new Vector3 (0f, 21f, 0f);
//			} else if (sceneName == "_Scene_1st_Level") {
//				transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
//			}
			transform.position = restartPos;
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			//DontDestroyOnLoad (this);
			PlayerPrefs.SetFloat("Timer", CurrentTime);
			PlayerPrefs.Save ();
			UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
		}

//		Vector3 movementHorizontal = new Vector3(moveHorizontal, 0.0f, 0.0f);
//		Vector3 movementVertical = new Vector3(0.0f, 0.0f, moveVertical);
		Vector3 movement = new Vector3(moveHorizontal, 0.0f,  moveVertical);
		movement = Camera.main.transform.TransformDirection(movement);
		if (movement.y > 0.0f || movement.y < 0.0f ) {
			movement.y = 0.0f;
			movement.z = moveVertical;
			print ("in here"); 
		}

		//print (movement);
		//TO DO: figure out how to use velocity instead. remember if I use velocity then must change
		//firction of material because it migth weird
		if(Input.GetKey(KeyCode.LeftShift)){
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		} if (!IsGrounded) {
			//rigid.velocity = movement * speed * Time.deltaTime;
			print ("z formation" + movement.z); 
			rigid.AddForce (movement * speed * Time.deltaTime, ForceMode.Impulse);

		} else{
//			rigid.AddForce(movementHorizontal * speed * Time.deltaTime, ForceMode.Impulse);
//			rigid.AddForce(movementVertical * speed * Time.deltaTime, ForceMode.Impulse);
			rigid.AddForce (movement * speed);
		}

		//this is so you can hold don't down space and still jump
		if(!isPowerUpMovingJumping || (powerup == PowerType.JUMP && powerup != PowerType.BUBBLE )) {
//			print("you should not be here if bubble");
//			print (powerup); 
			if(Input.GetKeyUp(KeyCode.Space)) {
				jumWasPressed = false;
			}
				
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

		// TEST_CHEAT 1
		if (Input.GetKey (KeyCode.F4)) {
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_1_Begin") {
				transform.position = new Vector3 (0, 21, 0);
			} else if (sceneName == "_Scene_1st_Level") {
				transform.position = new Vector3 (0.0f, 1.2f, -20.9f);
			}
		}

//		if (Input.GetKey (KeyCode.R)) {
//			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1st_Level");
//		}
//		if (Input.GetKey (KeyCode.F)) {
//			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1_Begin");
//		}

		//TO DO: add a way to do this without doing it directly in here. Also just fix gravity and velocity increases in general
		// POWER_UP : Add some additional Gravity when power up

		if(Input.GetKey(KeyCode.E) && numPowerUpMovingJumping > 0){
			numPowerUpMovingJumping--;
			enterPowerUp ();
		}
			
		// POWER_UP : Exit if time is up
		if(Time.time - powerUpStartTime > powerUpDuration && isPowerUpMovingJumping){
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
				//DontDestroyOnLoad (this);
				PlayerPrefs.SetFloat("Timer", CurrentTime);
				PlayerPrefs.Save ();
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

		if(Input.GetKey(KeyCode.F6)){
			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
			if (sceneName == "_Scene_Custom") {
				//GameObject.Find ("AppearingFinish").gameObject.SetActive (true);
				this.transform.parent.gameObject.transform.position = new Vector3 (538f, 62f, 171.92f);
			}
		}

		if (Input.GetKey (KeyCode.F12)) {
			transform.position = new Vector3 (511.7f, 99.4f, 139.8f);
		}

		if (Input.GetKey (KeyCode.F7)) {
			transform.position = new Vector3 (146.45f, -52.9f, 77.65f);
		}

		//Timer 
		Time_update();

		//LEVEL CONTROL
		if(Input.GetKey(KeyCode.Alpha1)){
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1_Begin");
		}else if(Input.GetKey(KeyCode.Alpha2)){
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_Custom");
		} else if(Input.GetKey(KeyCode.Alpha3)) {
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_Custom_Level_2");
		} else if(Input.GetKey(KeyCode.Alpha4)){
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1st_Level");
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if (isPowerUpMovingJumping && powerup == PowerType.BUBBLE) {
			if (collisionInfo.gameObject.tag == "Fruit") {
				Destroy (collisionInfo.gameObject);
			} 
			if (collisionInfo.gameObject.tag == "Floor") {
				//just do nothing
			} else if (collisionInfo.gameObject.tag == "GravityTube") {
				//nothing
			} else if (collisionInfo.gameObject.tag == "CheckPoint") {
				//nothing
			} else if (collisionInfo.gameObject.tag == "Finish") {
				//nothing
			} else {
				//everything destroys bubble for now except wind if I add it.
				print ("restartbubble"); 
				//Start();
				string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
				//DontDestroyOnLoad (this);
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
			}
			return;
		}
		if (collisionInfo.gameObject.tag == "Fruit") {
			Destroy (collisionInfo.gameObject);
		}

		if (collisionInfo.gameObject.tag == "Evil" 
			&& this.transform.position.y < collisionInfo.gameObject.transform.position.y) {
			brokenBegin = Time.time;
			isBrokenByEvil = true;
			BrokenByEvil (collisionInfo);
		}
		if (collisionInfo.gameObject.tag == "Projectile") {
			float magnitude = 5000f;
			Vector3 back_force_vec = transform.position = collisionInfo.gameObject.transform.position;
			back_force_vec.Normalize ();
			rigid.AddForce (back_force_vec * magnitude);
			collisionInfo.gameObject.GetComponent<Rigidbody> ().AddForce (back_force_vec * magnitude);

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
			PlayerPrefs.SetFloat("PlayerX", restartPos.x);
			PlayerPrefs.SetFloat("PlayerY", restartPos.y);
			PlayerPrefs.SetFloat("PlayerZ", restartPos.z);
			print ("checkpoint" + restartPos); 
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
			PlayerControl.S.isPowerUpMovingJumping = false;
			PlayerControl.S.jumpspeed /= jumpForceFactor;
			PlayerControl.S.speed /= speedFactor;
			// POWER_UP : Set the Glowing object false
			this.transform.FindChild ("Glow").gameObject.SetActive (false);
		} else {
			//do bubble shit
			//return mode
			print ("hey there"); 
			PlayerControl.S.isPowerUpMovingJumping = false;
			rigid.mass = 10.0f;
			this.gameObject.GetComponent<Rigidbody> ().mass = 10.0f;
			PlayerControl.S.GetComponent<Renderer> ().material = mat;
			rigid.drag = 0.0f;
		}
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

	public void Time_update(){
		CurrentTime = Time.time - LastStopTimePoint;
	}
}

// 75.53, -38.74, 79.8
// 216.3, -29.9, 79.8
