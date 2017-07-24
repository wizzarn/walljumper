using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	public float time;
	float tmr;
	Color color;

	void Update(){
		tmr += Time.deltaTime;
		if  (tmr > time){
			color = this.gameObject.GetComponentInChildren<SpriteRenderer> ().color;
			color.a -= .1f;
			this.gameObject.GetComponentInChildren<SpriteRenderer> ().color = color;

			if (color.a <= 0)
				Destroy (this.gameObject);
		}
	}
}
