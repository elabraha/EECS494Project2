﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

/*
 * This is the very first version of Power up;
 * Rather basically, it just speeds up the player (* factor > 1);
 * When jumping it will make the player jump with stronger force (* some factor > 1);
 * It may be served as a base class if there are more variance needed.
 * I assume this is a script control the object to power up the player scatter around the map
 * 
 */

public enum PowerType {JUMP, BUBBLE}


public class PowerUp : MonoBehaviour {
	public Material mat;

	public float speedFactor;
	public float jumpForceFactor;
	public float bubbleJump;

	public PowerType type; 

	//public GameObject renderMode;

	public bool _________________;


	public void SetType(PowerType absorb) {
		this.type = absorb;
	}

//	void Start() {
//		//set power up numbers?
//	}


	public void enterPowerUp(){
		//or set power up numbers in here
		//print ("why?"); 
		if (type == PowerType.JUMP) {
			PlayerControl.S.transform.FindChild ("Glow").gameObject.SetActive (true);
			PlayerControl.S.powerUpStartTime = Time.time;
			PlayerControl.S.powerUpDuration = 100.0f; 
			PlayerControl.S.isPowerUp = true;
			PlayerControl.S.jumpspeed *= jumpForceFactor;
			PlayerControl.S.speed *= speedFactor;
			PlayerControl.S.speedFactor = speedFactor;
			PlayerControl.S.jumpForceFactor = jumpForceFactor;
		} else{
			//else it's bubble for now
//			Material mat = PlayerControl.S.GetComponent<Renderer> ().material;
//			mat.SetFloat ("_Mode", 3.0f);
			PlayerControl.S.isPowerUp = true;
			PlayerControl.S.powerUpStartTime = Time.time;
			PlayerControl.S.powerUpDuration = 500.0f; 
			PlayerControl.S.GetComponent<Renderer> ().material = mat;
			PlayerControl.S.bubbleJump = bubbleJump;
			PlayerControl.S.rigid.drag = 1.0f;
			float mass = PlayerControl.S.GetComponent <Rigidbody> ().mass;
			mass = 0.5f;
			PlayerControl.S.GetComponent <Rigidbody> ().mass = mass;
			PlayerControl.S.rigid.velocity = Vector3.zero;
			PlayerControl.S.rigid.angularVelocity = Vector3.zero;
			PlayerControl.S.speed /= 20.0f;
		}
		Destroy (this.gameObject);
	}
		
	void OnCollisionEnter(Collision other){
		//print ("collide please");
		if (other.gameObject.tag == "Player" && PlayerControl.S.isPowerUp == false) {
			enterPowerUp ();
		}
		PlayerControl.S.powerup = type;
	}

}
