using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoControlSurface : MonoBehaviour {

	bool selected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		selected = !selected;
		// Move Camera
		GameObject camera = GameObject.Find ("Main Camera");
		if (selected) {
			camera.GetComponent<CameraController> ().moveToHorizontal (transform.position);
		} else {
			camera.GetComponent<CameraController> ().backToOrigin ();
		}	
	}
}
