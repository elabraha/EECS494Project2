using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataControl : MonoBehaviour {

	public bool isTransLevel;
	public static DataControl S;
	public Text FruitNumberDataText;
	public Text TimeDataText;
	public bool isGetTime;
	public bool isGetFruit;

	public Text HistFruitDataText;
	public Text HistTimeDataText;

	// Use this for initialization
	void Awake () {
		S = this;
	}

	void Start(){
		isGetTime = false;
		isGetFruit = false;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int levelNumber = 0;
		string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		if (sceneName == "_Scene_1_Begin") {
			levelNumber = 0;
		} else if (sceneName == "_Scene_Custom") {
			levelNumber = 1;
		}
		FruitNumberDataText = transform.FindChild ("NumFruitsData").GetComponent<Text> ();
		TimeDataText = transform.FindChild ("TimeData").GetComponent<Text> (); 

		HistFruitDataText = transform.FindChild ("HistHighFruitData").GetComponent<Text> ();
		HistTimeDataText = transform.FindChild ("HistHighTimeData").GetComponent<Text> ();
		if (!isGetFruit) {
			FruitNumberDataText.text = PlayerControl.S.FruitNum.ToString ();
			isGetFruit = true;
			if (PlayerControl.S.FruitNum > DataStore.S.FruitData) {
				DataStore.S.FruitData = PlayerControl.S.FruitNum;
				DataStore.S.histFruit = FruitNumberDataText.text;
			}
		}
		if (!isGetTime) {
			int hours;
			int minutes;
			int seconds;
			float Timer = PlayerControl.S.CurrentTime;
			hours = (int)Timer / 3600;
			minutes = (int)Timer / 60 - hours * 3600;
			seconds = (int)Timer - hours * 3600 - minutes * 60;
			TimeDataText.text = hours.ToString ("D2") + " : "
			+ minutes.ToString ("D2") + " : "
			+ seconds.ToString ("D2");
			isGetTime = true;
			if (Timer < DataStore.S.TimeData) {
				DataStore.S.TimeData = (int)Timer;
				DataStore.S.histTime = TimeDataText.text;
			}
			HistFruitDataText.text = DataStore.S.histFruit;
			HistTimeDataText.text = DataStore.S.histTime;
		}
	}
}
