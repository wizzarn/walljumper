using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallspike : MonoBehaviour {

	bool enableSpikesDelay, enableSpikes;
	public float tmrDelay=2;
	public float tmrDelay_=0;

	public float tmrSpikesActive=4;
	public float tmrSpikesActive_=0;

	bool isPlayerInside= false;
	Animator anim;

	public GameObject mainCharacterObj;
	public bool activeHurt = false;
	void Start () {
		mainCharacterObj = GameObject.Find ("character");
		enableSpikesDelay = false;
		enableSpikes = false;
		anim = this.gameObject.GetComponent<Animator> ();
	}

	public void ActiveHurt(){
		activeHurt = true;
	}
	public void DisableHurt(){
		activeHurt = false;
	}
	void Update () {
		Timer ();
		Animator ();
	}

	void Timer(){
		if (enableSpikesDelay){
			tmrDelay_ += Time.deltaTime;
			if (tmrDelay_>tmrDelay){
				tmrDelay_ = 0;
				enableSpikesDelay = false;
				enableSpikes = true;
				tmrSpikesActive_ = 0;
			}
		}
		if(enableSpikes){
			tmrSpikesActive_ += Time.deltaTime;
			if (tmrSpikesActive_ > tmrSpikesActive){
				enableSpikes = false;
				tmrSpikesActive_ = 0;
				DisableHurt ();
			}
			if (isPlayerInside && activeHurt)
				mainCharacterObj.GetComponent<MainCharacter> ().GetHurt (DamageObjectsEnum.wallSpikeDamage);
		}
	}
	void Animator(){
		anim.SetBool ("active",enableSpikes);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag==TagNamesEnum.mainCharacter){
			isPlayerInside = true;
			if (!enableSpikes && !enableSpikesDelay) {
				enableSpikesDelay = true;
				tmrDelay_ = 0;
				tmrSpikesActive_ = 0;
				enableSpikes = false;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == TagNamesEnum.mainCharacter){
			isPlayerInside = false;
		}
	}

}
