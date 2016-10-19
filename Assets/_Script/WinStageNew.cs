using UnityEngine;
using System.Collections;

public class WinStageNew : MonoBehaviour {

	//public bool canWin = false;
	public static WinStageNew S;

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
		if (other.gameObject.tag == "Player") {
			DataControl.S.gameObject.SetActive (true);
			DisplayWhenPlaying.S.transform.FindChild ("Timer").gameObject.SetActive (false);
			DisplayWhenPlaying.S.transform.FindChild ("TimerIcon").gameObject.SetActive (false);
			DisplayWhenPlaying.S.transform.FindChild ("FruitNum").gameObject.SetActive (false);
			DisplayWhenPlaying.S.transform.FindChild ("FruitNumIcon").gameObject.SetActive (false);
		}
	}
}
