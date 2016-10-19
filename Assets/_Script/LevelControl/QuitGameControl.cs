using UnityEngine;
using System.Collections;

public class QuitGameControl : MonoBehaviour {

	public void ClickTest(){
		// your code goes here
		Application.Quit();
	}

	public void ClickTest1(){
		string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		if (sceneName == "_Scene_Custom") {
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_Custom_Level_2");

		} else if (sceneName == "_Scene_1_Begin") {
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("_Scene_Custom"); 
		} else {
			PlayerPrefs.DeleteAll ();
			Application.Quit();
		}
	}
}
