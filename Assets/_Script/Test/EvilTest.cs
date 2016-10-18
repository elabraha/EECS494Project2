using UnityEngine;
using System.Collections;

public class EvilTest : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		print (other.gameObject.name);
	}
}
