using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour {

	bool playerInside;
	GameObject mainCharacter;
	void Start () {
		mainCharacter = GameObject.Find ("character");
	}

	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter){
			playerInside = true;
			mainCharacter.GetComponent<MainCharacter> ().WindMovementReduction ();
			mainCharacter.GetComponent<MainCharacter> ().isUnderWindEffect = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter && playerInside){
			playerInside = false;
			mainCharacter.GetComponent<MainCharacter> ().RecoveryMovementFromWind ();
			mainCharacter.GetComponent<MainCharacter> ().isUnderWindEffect = false;
		}
	}
}
