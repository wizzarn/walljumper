using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

	public float horizontalSpeed;
	public float verticalSpeed;
	public float amplitude;

	Vector3 tempPosition;
	float startY=0;
	float tmr = 0;
	float livingTime = 18;


	void Start () {
		tempPosition = transform.position;
		startY = tempPosition.y;
	}
	void Update(){
		tmr += Time.deltaTime;
		if (tmr > livingTime) {
			Destroy (this.gameObject);
		}
	}
	
	void FixedUpdate(){
		tempPosition.x += horizontalSpeed;
		tempPosition.y = Mathf.Sin (Time.realtimeSinceStartup * verticalSpeed) * amplitude + startY;
		transform.position = tempPosition;
	}
}
