using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricbox : MonoBehaviour {


	public float tmr_ = 0 ;
	public float tmrActivate = 8;
	public float tmrStartHurting = 13;
	public float tmrEnd = 18;
	public bool flagActivateSound = false;
	public bool isActive_ = false;
	bool isPlayerInside = false;
	GameObject mainCharacterObj;
	Animator anim;

	/* Audio */
	public GameObject energyActiveObj;
	public GameObject energyLoadingObj;
	public GameObject shockCharacterObj;
	AudioSource energyActiveSource;
	AudioSource energyLoadingSource;
	AudioSource shockCharacterSource;

	bool flagLoadingEnergy = false;
	bool flagActiveEnergy = false;

	void Start(){
		anim = this.GetComponent<Animator> ();
		energyActiveSource = energyActiveObj.GetComponent<AudioSource> ();
		energyLoadingSource = energyLoadingObj.GetComponent<AudioSource> ();
		shockCharacterSource = shockCharacterObj.GetComponent<AudioSource> ();
	}

	void PlayActiveEnergy(){
		energyActiveSource.Play ();
	}
	void StopActiveEnergy(){
		energyActiveSource.Stop ();
	}
	void PlayEnergyLoading(){
		energyLoadingSource.Play ();
	}
	void StopEnergyLoading(){
		energyLoadingSource.Stop ();
	}
	void PlayShockCharacter(){
		shockCharacterSource.Play ();
	}
	void StopShockCharacter(){
		shockCharacterSource.Stop ();
	}

	void Update () {
		tmr_ += Time.deltaTime;
		if (tmr_ > tmrActivate) {
			if (!flagLoadingEnergy) {
				PlayEnergyLoading ();
				flagLoadingEnergy = true;
			}
		}
		if (tmr_ > tmrStartHurting){
			if (!isActive_) {
				isActive_ = true;
				StartParticleEffect ();
				if (!flagActiveEnergy){
					StopEnergyLoading ();
					PlayActiveEnergy ();	
					flagActiveEnergy = true;
				}
			}
		}
		if (tmr_ > tmrEnd){
			if (isActive_)
				isActive_ = false;
			flagActivateSound = false;
			RestartCycle ();
			EndParticleEffect ();
			StopActiveEnergy ();
			flagActiveEnergy = false;
			flagLoadingEnergy = false;
		}
		if (isActive_ && isPlayerInside) {
			mainCharacterObj.GetComponent<MainCharacter> ().GetHurt (DamageObjectsEnum.electricBoxDamage);
		}
	}
	void RestartCycle(){
		tmr_ = 0;
	}
	void StartParticleEffect(){
		anim.SetBool ("active",true);
		
	}
	void EndParticleEffect(){
		anim.SetBool ("active",false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter) {
			isPlayerInside = true;
			mainCharacterObj = other.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == TagNamesEnum.mainCharacter) 
			isPlayerInside = false;
	}
}
