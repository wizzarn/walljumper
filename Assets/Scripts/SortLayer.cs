using UnityEngine;
using System.Collections;

public class SortLayer : MonoBehaviour {

	public string sortLayer = "Default";
	public int orderLayer = 0;
	void Start () {
		this.GetComponent<Renderer>().sortingLayerName = sortLayer;
		this.GetComponent<Renderer>().sortingOrder = orderLayer;
	}
}
