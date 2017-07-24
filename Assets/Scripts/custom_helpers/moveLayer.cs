using UnityEngine;
using System.Collections;

public class moveLayer : MonoBehaviour {

	private float moveX;
	public float speedX;
	public float limitX;

	private Vector3 firstPos;

	void Start () {
		moveX = 0.0f;
		firstPos = transform.localPosition;
	}

	void Update () {
		transform.position = firstPos;
		Vector3 newPos = transform.position;

		if (speedX > 0) {

			if (moveX < limitX) {
				moveX += speedX;
				newPos.x += moveX;
			} else {
				newPos = firstPos;
				moveX = 0;
			}
		} else {
			if (moveX > limitX) {
				moveX += speedX;
				newPos.x += moveX;
			} else {
				moveX = 0;
			}
		}

		transform.position = newPos;
	
	}
}
