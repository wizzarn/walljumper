using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameObject targetObj;
	float speed = .1f;
	Vector3 startPosition;
	Vector3 endPosition;

	float delayAfterFire = 1.5f;
	bool delayFlag = false;
	float currentLerpValue = 0;
	float currentTimer = 0;
	float tmr = 0;
	bool isPlayerInside = false;
	public GameObject objExplosion;
	public GameObject audioSourceObj;

	void Start () {
		startPosition = this.transform.position;
		endPosition = targetObj.transform.position;
	}
	void Update () {
		if (delayFlag) {
			tmr += Time.deltaTime;
			if (tmr > delayAfterFire) {
				tmr = 0;
				delayFlag = false;
				Instantiate (objExplosion,this.transform.position, this.transform.rotation);
				audioSourceObj.GetComponent<AudioHandler> ().InstanceShotGun ();
				if (isPlayerInside)
					targetObj.GetComponent<MainCharacter> ().GetHurt (DamageObjectsEnum.shotGunDamage);
			}
			return;
		} else {
			endPosition = targetObj.transform.position;
			currentTimer += Time.deltaTime;
			currentLerpValue = currentTimer * speed;
			transform.position = Vector3.Lerp(startPosition, endPosition, currentLerpValue);
			if (currentLerpValue > 1){
				//instaniate
				SetFire();
			}
		}

	}
	void SetFire(){
		currentLerpValue = 0;
		currentTimer = 0;
		startPosition = this.transform.position;
		endPosition = targetObj.transform.position;
		delayFlag = true;
		tmr = 0;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter){
			isPlayerInside = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter){
			isPlayerInside = false;
		}
	}

}
