using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShot : MonoBehaviour {

	float tmr=.5f;
	float tmr_ = 0;

	float tmrDestroy=1;
	float tmrDestroy_=0;
	bool flagDestroyDelay = false;

	public GameObject audioSourceObj;
	public GameObject mainCharacterObj;
	bool isPlayerInside = false;
	void Start () {
		audioSourceObj = GameObject.Find ("AudioHandler");
		mainCharacterObj = GameObject.Find ("character");
	}

	void Update () {
		if (!flagDestroyDelay) {
			tmr_ += Time.deltaTime;
			if (tmr_ > tmr) {
				tmr_ = 0;
				flagDestroyDelay = true;
				InstantiateShotParticle ();
			}	
		} else {
			tmrDestroy_ += Time.deltaTime;
			if (tmrDestroy_ > tmrDestroy){
				audioSourceObj.GetComponent<AudioHandler>().InstanceMachineGunSound();
				tmrDestroy_ = 0;
				if (isPlayerInside)
					mainCharacterObj.GetComponent<MainCharacter> ().GetHurt (DamageObjectsEnum.machineGunDamage);
				Destroy (this.gameObject);
			}
		}
	}
	void InstantiateShotParticle(){
		//this.getComponent<Particle>().SetActive(true);
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
