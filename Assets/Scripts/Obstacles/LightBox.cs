using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBox : MonoBehaviour {


	private string left = "LightBallLimitLeft";
	private string right  = "LightBallLimitRight";
	private string top = "LightBallLimitTop";
	private string bottom = "LightBallLimitBottom";
	private Vector2 currentVelocity;
	private Vector2 velocity = new Vector2(.04f,.06f);
	Rigidbody2D rigidBody;
	bool triggeredFlag = false;
	float tmrTriggered = 3;
	float tmrTriggered_ = 0;
	float tmrAfterDelay = 2;
	float tmrAfterDelay_ = 0;
	bool afterDelayFlag = false;
	public GameObject mainCharacterObj;
	GameObject parentOBJ;

	Vector3 charPosition;
	MachineGun machineGunScript;
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
		if (Random.Range(1,10) > 5)
			velocity.x *= -1;
		
		if (Random.Range (1, 10) > 5) 
			velocity.y *= -1;
		rigidBody.velocity = velocity;
		parentOBJ = this.transform.parent.gameObject;
		machineGunScript = this.GetComponent<MachineGun> ();
	}

	void Update () {
		if (triggeredFlag){
			tmrTriggered_ += Time.deltaTime;
			if (tmrTriggered_ > tmrTriggered) {
				tmrTriggered_ = 0;
				triggeredFlag = false;
				afterDelayFlag = true;
				tmrAfterDelay_ = 0;
				this.transform.parent = null;
				machineGunScript.ActivateMachineGun ();
			}
		}
		if (afterDelayFlag){
			tmrAfterDelay_ += Time.deltaTime;
			if (tmrAfterDelay_ > tmrAfterDelay){
				tmrAfterDelay_ = 0;
				afterDelayFlag = false;
				this.transform.parent = parentOBJ.transform;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == left || other.tag == right)
			velocity.x *= -1;
		if (other.tag == top || other.tag == bottom)
			velocity.y *= -1;

		if (other.tag == TagNamesEnum.mainCharacter){
			if (triggeredFlag || afterDelayFlag)
				return;
			triggeredFlag = true;
		}
	}
	void FixedUpdate(){
		if (!triggeredFlag && !afterDelayFlag)
			transform.Translate (velocity);
		else if (triggeredFlag && !afterDelayFlag){
			charPosition = mainCharacterObj.transform.position;
			charPosition.z = transform.position.z;
			transform.position = charPosition;
		}else if (!triggeredFlag && afterDelayFlag){
			transform.Translate (new Vector2(0,0));
			print (1);
		}
	}
}
