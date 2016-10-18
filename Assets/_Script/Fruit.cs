using UnityEngine;
using System.Collections;


//private static class IsComponentExist {
//	public static bool isComponentExist<T>(this GameObject flag) where T : Component{
//		return flag.GetComponent<T> () != null;
//	}
//}

public class Fruit : MonoBehaviour {

	public bool isFall = false;
	public const float SPEED = 9.8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isFall) {
			//Debug.Log("Always true?");
			this.transform.position -= SPEED * Time.fixedDeltaTime * Vector3.up;
		}
		if (this.transform.position.y <= PlayerControl.S.transform.position.y) {
			//Debug.Log (this.transform.position.y);
			//Debug.Log (PlayerControl.S.transform.position.y);
			isFall = false;
			Debug.Log (isFall);
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Entre");
		if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
//
//	void OnTriggerStay(Collider other){
//		if (other.gameObject.tag == "Floor") {
//			isFall = false;
//		}
//	}
}
 