using UnityEngine;
using System.Collections;

/*
 * This is the very first version of Power up;
 * Rather basically, it just speeds up the player (* factor > 1);
 * When jumping it will make the player jump with stronger force (* some factor > 1);
 * It may be served as a base class if there are more variance needed.
 * I assume this is a script control the object to power up the player scatter around the map
 * 
 */

public class PowerUp : MonoBehaviour {

	public float speedFactor;
	public float jumpForceFactor;
	public bool isActivate = true;

	public bool _________________;

	//protected float powerUpStartTime;

	void Start(){
		this.gameObject.SetActive (isActivate);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player" && PlayerControl.S.isPowerUpMovingJumping == false) {
			getPowerUp ();
		}
	}

	public void getPowerUp(){
//		PlayerControl.S.transform.FindChild ("Glow").gameObject.SetActive (true);
//		PlayerControl.S.powerUpStartTime = Time.time; 
//		PlayerControl.S.isPowerUpMovingJumping = true;
//		PlayerControl.S.jumpspeed *= jumpForceFactor;
//		PlayerControl.S.speed *= speedFactor;
		PlayerControl.S.numPowerUpMovingJumping += 1;
		PlayerControl.S.speedFactor = speedFactor;
		PlayerControl.S.jumpForceFactor = jumpForceFactor;
		Destroy (this.gameObject);
	}

}
