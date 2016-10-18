using UnityEngine;
using System.Collections;

//public static class IsComponentExist {
//	public static bool isComponentExist<T>(this GameObject flag) where T : Component{
//		return flag.GetComponent<T> () != null;
//	}
//}

public class RewardComtrolSimple : MonoBehaviour {


	public bool isHitByPlayer = false;
	public bool isupdate = false;
	
	// Update is called once per frame
	void FixedUpdate () {
		isHitByPlayer = this.transform.FindChild ("BrokenPost_0").gameObject.GetComponent<BreakControl> ().isBreak;
		if (isHitByPlayer && !isupdate) {
			//this.transform.FindChild ("Fruit").gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			this.transform.FindChild ("Fruit").gameObject.GetComponent<Fruit>().isFall = true;
			isupdate = true;
		}
	}
}
