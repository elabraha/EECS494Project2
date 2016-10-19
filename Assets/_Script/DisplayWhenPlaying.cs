using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayWhenPlaying : MonoBehaviour {

	public Text FruitNumText;
	public Text TimerText;
	public int FruitNum;
	public float Timer;
	public static DisplayWhenPlaying S;

	public Sprite OtherSpriteforNone;
	public Sprite OtherSpriteforPowerUpJump;
	public Sprite OtherSpriteforPowerUpBubble;


	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		FruitNumText = transform.FindChild ("FruitNum").GetComponent<Text> ();
		TimerText = transform.FindChild ("Timer").GetComponent<Text> ();
		FruitNum = PlayerControl.S.FruitNum;
		Timer = PlayerControl.S.CurrentTime;
	}
	
	// Update is called once per fixed frame
	void FixedUpdate () {
		int hours;
		int minutes;
		int seconds;

		Image images = gameObject.transform.FindChild("PowerUpIcon").gameObject.GetComponent<Image>();
		if (PlayerControl.S.numPowerUpMovingJumping <= 0) {
			images.sprite = OtherSpriteforNone;
			images.rectTransform.sizeDelta = new Vector2(1, 1);
		}
		else if (PlayerControl.S.powerup == PowerType.JUMP  && PlayerControl.S.numPowerUpMovingJumping > 0) {
			images.sprite = OtherSpriteforPowerUpJump;
			images.rectTransform.sizeDelta = new Vector2(80, 80);
		} else if (PlayerControl.S.powerup == PowerType.BUBBLE && PlayerControl.S.numPowerUpMovingJumping > 0) {
			images.sprite = OtherSpriteforPowerUpBubble;
			images.rectTransform.sizeDelta = new Vector2(80, 80);
		}
		// keep updating
		FruitNum = PlayerControl.S.FruitNum;
		Timer = PlayerControl.S.CurrentTime;
		// get the time in a HH:MM:SS format
		hours = (int)Timer / 3600;
		minutes = (int)Timer / 60 - hours * 3600;
		seconds = (int)Timer - hours * 3600 - minutes * 60;
		//Display both data
		FruitNumText.text = FruitNum.ToString();
		TimerText.text = hours.ToString ("D2") + " : "
						+ minutes.ToString ("D2") + " : "
						+ seconds.ToString ("D2");
	}

	public void AddFruit(){
		++PlayerControl.S.FruitNum;
		++FruitNum;
	}
}

// http://opengameart.org/content/transparent-bubble
// http://opengameart.org/content/simple-ornamented-arrow
