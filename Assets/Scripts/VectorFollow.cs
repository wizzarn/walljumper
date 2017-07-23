using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFollow : MonoBehaviour {

	public Transform mTarget;
	Vector3 mLookAtDirection;
	float mSpeed = .8f;
	const float EPSILON = .1f;
	void Start () {
		
	}
	

	void Update () {
		mLookAtDirection = (mTarget.position - transform.position).normalized;
		if ((transform.position - mTarget.position).magnitude > EPSILON)
			transform.Translate (mLookAtDirection * Time.deltaTime * mSpeed);
	}
}
