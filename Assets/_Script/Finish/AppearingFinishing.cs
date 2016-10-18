using UnityEngine;
using System.Collections;

public class AppearingFinishing : MonoBehaviour {

	public static AppearingFinishing S;
	public int positionToShow = -1;
	public static float[] pos = new float[]{-45f, 45f, 135f, -135f};
	public bool isShown = false;

	void Awake(){
		S = this;
		this.gameObject.SetActive (false);
	}

	void FixedUpdate(){
		if (!isShown && positionToShow > -1 && positionToShow < 4) {
			this.transform.rotation = Quaternion.Euler (0f, pos [positionToShow], 0f);
			isShown = true;
		}
	}
}
