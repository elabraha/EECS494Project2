using UnityEngine;
using System.Collections;

public class DataStore : MonoBehaviour {

	public static DataStore S;
	public string histFruit = "0";
	public string histTime = "99 : 59 : 59";
	public int TimeData = 35999;
	public int FruitData = 0;

	void Awake(){
		if (S == null) {
			S = this;
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyImmediate (gameObject);
		}
	}
}
