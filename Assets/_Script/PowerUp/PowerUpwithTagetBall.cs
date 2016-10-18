using UnityEngine;
using System.Collections;

public class PowerUpwithTagetBall : MonoBehaviour {

	public bool isleft = true;
	public GameObject fruits;

	void OnCollisionEnter(Collision other){
		
		if (other.gameObject.tag == "Floor") {
			bool temp = this.transform.parent.parent.gameObject.GetComponent<PowerUpDecider>().isleftPowerUp;
			Debug.Log (isleft && temp);
			if (isleft && temp || !isleft && !temp) {
				this.transform.parent.FindChild ("StarPowerUp").gameObject.SetActive (true);
				this.transform.parent.FindChild ("StarPowerUp").gameObject.transform.position = this.transform.position;
				fruits.SetActive (true);
				//Finally set inactivate
				this.gameObject.SetActive (false);
			}
		}
	}
}
	