using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBox : MonoBehaviour {


	string left = "LightBallLimitLeft";
	string right  = "LightBallLimitRight";
	string top = "LightBallLimitTop";
	string bottom = "LightBallLimitBottom";
	Vector2 currentVelocity;
	Vector2 velocity = new Vector2(1,1.5f);
	Rigidbody2D rigidBody;
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
		if (Random.Range(1,10) > 5)
			velocity.x *= -1;
		
		if (Random.Range (1, 10) > 5) 
			velocity.y *= -1;
		rigidBody.velocity = velocity;
	}
	

	void Update () {
			
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == left || other.tag == right)
			velocity.x *= -1;
		else if (other.tag == top || other.tag == bottom)
			velocity.y *= -1;
		rigidBody.velocity = velocity;
	}
}
