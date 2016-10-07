﻿using UnityEngine;
using System.Collections;

public class GravityObj : MonoBehaviour {

	public float 			GravityCoefficient = 10f;
//	public float			massForGravity = 10f;
//	public Quaternion		rot = new Quaternion(0,0,0);
	public Vector3 			size = new Vector3 (20, 2, 2);
	public float 			gravityBoundary = 5f;
	public Vector3			gravityDirection; 
	public bool 			____________;
	private float 			distToPlayer;

	// Use this for initialization
	void Start () {
		this.transform.localScale = size;
		gravityDirection = gameObject.transform.rotation * Vector3.left;
	}
	
	// FixedUpdate is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Player.S.rigid.AddForce (GravityCoefficient * gravityDirection, ForceMode.Force);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			Player.S.rigid.AddForce (GravityCoefficient * gravityDirection, ForceMode.Force);
		}
	}

	void OnTriggerExit(Collider other){
		// nothing for now	
	}
}
