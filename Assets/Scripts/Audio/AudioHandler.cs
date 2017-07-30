using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	AudioSource audioCanonSource;
	AudioSource coinSource;
	AudioSource backgroundSource;
	AudioSource fallingObjectSource;
	AudioSource machineGunSource;
	AudioSource shotGunSource;


	public GameObject canonBallAudioObj;
	public GameObject backgroundAudioObj;
	public GameObject coinAudioObj;
	public GameObject fallingObjectObj;
	public GameObject machineGunObj;
	public GameObject shotGunObj;


	void Start () {
		audioCanonSource = canonBallAudioObj.GetComponent<AudioSource> ();
		coinSource = coinAudioObj.GetComponent<AudioSource> ();
		fallingObjectSource = fallingObjectObj.GetComponent<AudioSource> ();
		machineGunSource = machineGunObj.GetComponent<AudioSource> ();
		shotGunSource = shotGunObj.GetComponent<AudioSource> ();

		if (backgroundAudioObj) {
			backgroundSource = backgroundAudioObj.GetComponent<AudioSource> ();
			//backgroundSource.Play ();
		}

	}

	public void InstanceShotGun(){
		shotGunSource.Play ();
	}

	public void InstanceMachineGunSound(){
		machineGunSource.Play ();
	}

	public void InstanceCanonBallSound(){
		audioCanonSource.Play ();
	}
	public void InstanceCoinSound(){
		coinSource.Play ();
	}
	public void InstanceFallingObjectSound(){
		fallingObjectSource.Play ();
	}
}
