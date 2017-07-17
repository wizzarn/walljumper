using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPoliceUi : MonoBehaviour {

	enum StartDirection{
		Top,
		Bottom
	}
	StartDirection startDirection;
	float tmr =  .6f;
	float tmr_ = 0;
	Vector3 position;
	float yPosition;
	void Start () {
		startDirection = Random.Range (1, 10) > 5 ? StartDirection.Top : StartDirection.Bottom;
		position = this.gameObject.GetComponent<RectTransform>().position;
	}
	

	void Update () {
		tmr_ += Time.deltaTime;
		if (tmr_ > tmr) {
			tmr_ = 0;
			startDirection = startDirection == StartDirection.Top ? StartDirection.Bottom : StartDirection.Top;
		}
		MovementAnimation ();
	}
	void MovementAnimation(){
		position = this.gameObject.GetComponent<RectTransform>().position;
		position.y += startDirection == StartDirection.Top ? .15f : -.15f;
	}
	void FixedUpdate(){
		this.gameObject.GetComponent<RectTransform>().position = position;
	}
}
