using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public float offsetX = 1.0f;
	public Transform target;

	int ScreenHeight = 480;
	int PixelHeight = 48;
	public float targetDistance = 10.0f;

	public float deltaY = 1.0f;

	public LayerMask mask;
	public float distance;
	public float lastY;

	bool flagX, flagY;
	public bool vibracion;
	void Start()
	{
		//camera.isOrthoGraphic = true;
		GetComponent<Camera>().orthographicSize = ((float)ScreenHeight / ((float)PixelHeight) / 2.0f);
		if (!target)
		{
			print("There is not target attached to MainCamera!");
			return;
		}
		lastY = target.position.y;
	}

	void LateUpdate()
	{
		if (target)
		{
			Vector3 destination = target.position;

			RaycastHit2D hit = Physics2D.Raycast(target.transform.position, Vector3.down, Mathf.Infinity, mask);

			if (hit.collider)
			{
				if (hit.distance < 7.0f)
				{
					destination.y = hit.point.y;
				}
				else
				{
					destination.y = target.position.y;
				}
				lastY = hit.point.y;
			}
			destination.y += deltaY;
			destination.z -= targetDistance;
			destination.x = transform.position.x;
			//

			if (vibracion)
			{
				if (!flagX)
					destination.x += .2f;
				else
					destination.x -= 2f;

				if (!flagY)
					destination.y += .2f;
				else
					destination.y -= 2f;
				flagX = (flagX) ? false : true;
				flagY = (flagY) ? false : true;
			}
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			distance = hit.distance;

		}
	}

	void Vibracion()
	{

	}
}
