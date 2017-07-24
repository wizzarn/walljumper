﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	public float time;
	private IEnumerator Start()
	{
		 yield return new WaitForSeconds( time );
		 Destroy( gameObject ); 
	}
}
