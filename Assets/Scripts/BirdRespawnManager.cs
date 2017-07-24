using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRespawnManager : MonoBehaviour {

	public List<GameObject> objRespawnPoints = new List<GameObject>();
	List<float> respawnRandomTimers = new List<float>();
	List<float> respawnTmrs_ = new List<float>();
	public GameObject birdPrefabLeft;
	public GameObject birdPrefabRight;
	Vector3 respawnPositionRnd;
	Vector2 rangeValueTimer = new Vector2(5,15);
	void Start () {
		for (int i = 0; i < objRespawnPoints.Count; i++){
			respawnRandomTimers.Add(Random.Range(rangeValueTimer.x,rangeValueTimer.y));
			respawnTmrs_.Add(0);
		}
	}

	void Update () {
		for(int i = 0; i < respawnTmrs_.Count; i++){
			respawnTmrs_ [i] += Time.deltaTime;
			if (respawnTmrs_ [i] > respawnRandomTimers [i]) {
				respawnTmrs_ [i] = 0;
				respawnRandomTimers [i] = Random.Range (rangeValueTimer.x,rangeValueTimer.y);
				respawnPositionRnd = objRespawnPoints [i].transform.position;
				respawnPositionRnd.y = Random.Range (respawnPositionRnd.y-5,respawnPositionRnd.y+5);
				if (objRespawnPoints[i].transform.localScale.x == -1)
					Instantiate (birdPrefabLeft,respawnPositionRnd, objRespawnPoints[i].transform.rotation);
				else
					Instantiate (birdPrefabRight,respawnPositionRnd, objRespawnPoints[i].transform.rotation);
			}
		}
	}
}
