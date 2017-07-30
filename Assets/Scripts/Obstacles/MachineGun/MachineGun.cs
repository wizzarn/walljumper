using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {

	public bool activateMachineGunFlag = false;
	float tmrActivateDelay = .3f;
	float tmr_ = 0;
	bool startFireFlag = false;

	float tmrStartFireLoop = .15f;
	float tmrStartFireLoop_ = 0;

	int totalShots = 5;
	int actualShot = 0;

	public GameObject mainCharacterObj;
	public GameObject pointPrefab;
	public List<GameObject> pointsList = new List<GameObject>();

	float amplitude = .2f;

	float extraAmplitudeX = 1;
	float extraAmplitudeY = 1.3f;
	void Start () {

	}
	
	public void ActivateMachineGun(){
		if (startFireFlag)
			return;
		tmr_ = 0;
		activateMachineGunFlag = true;
	}
	void Update () {
		if (activateMachineGunFlag){
			tmr_ += Time.deltaTime;
			if (tmr_ > tmrActivateDelay) {
				tmr_ = 0;
				activateMachineGunFlag = false;
				startFireFlag = true;
				tmrStartFireLoop_ = 0;
			}
		}
		if (startFireFlag){
			tmrStartFireLoop_ += Time.deltaTime;
			if (tmrStartFireLoop_ > tmrStartFireLoop){
				tmrStartFireLoop_ = 0;
				actualShot++;
				InstantiateShot();
			}
		}
		if (actualShot > totalShots) {
			startFireFlag = false;
			activateMachineGunFlag = false;
			actualShot = 0;
		}
	}
	void InstantiateShot(){
		Vector3 startPosition = mainCharacterObj.transform.position;
		GameObject pointObj = (GameObject)Instantiate (pointPrefab, calculateNextPosition (mainCharacterObj.GetComponent<Rigidbody2D> ().velocity, mainCharacterObj.transform.position), Quaternion.identity);
	}
	Vector3 calculateNextPosition(Vector2 velocity, Vector3 position){
		Vector3 nextPosition = position;

		if (velocity.x > 0)
			nextPosition.x += (amplitude * actualShot) + extraAmplitudeX;
		else if (velocity.x < 0)
			nextPosition.x -= (amplitude * actualShot) + extraAmplitudeX;

		if (velocity.y > 0)
			nextPosition.y += (amplitude * actualShot) + extraAmplitudeY;
		else if (velocity.y < 0)
			nextPosition.y -= (amplitude * actualShot) + extraAmplitudeY;

		return nextPosition;
	}
}
