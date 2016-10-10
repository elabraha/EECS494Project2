using UnityEngine;
using System.Collections;

public class WinnerStage : MonoBehaviour {
	//public bool canWin = false;
	public static WinnerStage S;
	// Use this for initialization
	void Start () {
		S = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void OnCollisionEnter(Collision other){
		// Do something to show the player passes the instruction level
		//Debug.Log("Player Win the instruction level!");
//		if (canWin && other.gameObject.tag == "Player") {
//			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1st_Level");
//		}
		if (other.gameObject.tag == "Player" && PlayerControl.S.canWin) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_1st_Level");
		}
	}
}
