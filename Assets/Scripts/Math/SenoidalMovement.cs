using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenoidalMovement : MonoBehaviour {

	public float livingTime = 15;
	public float tmrLive = 0;
	public bool diyingFlag;

	float tmrMove;
	Vector3 inverseMove;

	public float amplitudeX = .7f;
	public float amplitudeY = .5f;
	public float omegaX = 5.0f;
	public float omegaY = 10.0f;
	float index;
	Vector3 movement;
	float lastX = 0;

	float tmrInit= 0;
	float tmrEnd=0;
	Color initColor;
	bool flagEndInit = false; 
	bool flagEndEnd = false; 

	float inversed = 0;
	bool initialized = false;
	float initY = 0;
	float tmrDelay = 0.05f;
	public enum Direction{
		Left,
		Right
	};
	public Direction direction;
	Vector3 initPosition;
	void Start(){
		inverseMove = this.transform.localScale;;

		initY = 0;
		if (direction == Direction.Right) {
			inversed = -.3f;
			inverseMove.x = -1 * inversed;
		} else {
			inversed = .3f;
			inverseMove.x = 1 * inversed;
		}
		

		initialized = true;
		index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
		initPosition = this.transform.position;
		movement.x  = initPosition.x + (x*inversed);
		movement.y += initPosition.y + y;
		movement.z += initPosition.z + 0;
		if (x > lastX) 
			inverseMove.x = 1 * inversed;
		else
			inverseMove.x = -1 * inversed;
		lastX = x;

		transform.position= movement;
		this.gameObject.transform.localScale = inverseMove;

	}

	void Update () {
		if (!initialized)
			return;
		tmrLive += Time.deltaTime;
		tmrMove += Time.deltaTime;
		tmrInit += Time.deltaTime;
		tmrEnd += Time.deltaTime;

		if (!flagEndInit) {
			if (tmrInit > tmrDelay){
				tmrInit = 0;
				if (initColor.a > 1)
					flagEndInit = true;
				initColor = this.GetComponent<SpriteRenderer> ().color;
				initColor.a += .03f;
				this.GetComponent<SpriteRenderer> ().color = initColor;
			}
		}
		if (flagEndEnd){
			if (tmrEnd > tmrDelay){
				tmrEnd = 0;
				if (initColor.a < 0)
					Destroy (this.gameObject);
				else {
					initColor.a -= .03f;
					this.GetComponent<SpriteRenderer> ().color = initColor;
				}

			}
		}

		if (tmrMove > tmrDelay) {
			tmrMove = 0;
			index += Time.deltaTime;
			float x = amplitudeX*Mathf.Cos (omegaX*index);
			float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
			y += initY;
			movement.x = initPosition.x + (x*inversed);
			movement.y = initPosition.y + y;
			movement.z = initPosition.z + 0;
			if (x > lastX) {
				inverseMove.x = 1 * inversed;

			}
			else
				inverseMove.x = -1 * inversed;
			lastX = x;
		}
		if (tmrLive > livingTime){
			flagEndEnd = true;
			//Destroy (this.gameObject);
		}
	}
	void FixedUpdate(){
		transform.position= movement;
		this.gameObject.transform.localScale = inverseMove;
	}
}
