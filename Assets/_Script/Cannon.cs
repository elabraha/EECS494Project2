using UnityEngine;
using System.Collections;
using System.Text;

public class Cannon : MonoBehaviour {

	// Use this for initialization
	public GameObject bulletPrefab;
	public float shootTime = 3f; 
	bool visible = false;

	void OnBecameInvisible () {
		print ("invisible"); 
		visible = false;

	}

	void Go () {
		InvokeRepeating ("Shoot", shootTime, shootTime);
	}
	//cancel all invoke cals
	void Update() {
		if(!visible)
			CancelInvoke();
	}

	// Update is called once per frame
	void Shoot () {
		Bullet bullet = Instantiate (bulletPrefab, transform.position, transform.rotation) as Bullet;
		//bullet.transform.TransformDirection (transform.forward);
	}

	void OnBecameVisible() {
		print ("visible");
		visible = true;
		Go ();
	}
		

}
