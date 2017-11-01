using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {
	private float degree_per_beat = -22.5f;
	public bool selected;
	public int number;

	// Use this for initialization
	void Start () {
		transform.Rotate (0.0f, number*90.0f, 0.0f);
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalVariables.action) {
			transform.Rotate (0.0f, speed (), 0.0f);
		}
	}

	private float speed() {
		return degree_per_beat * GlobalVariables.bpm * Time.deltaTime / 60.0f;
	}

	void OnMouseDown() {
		selected = !selected;
		float cr = selected ? 0.3f : 0;
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
			r.material.SetColor("_EmissionColor", new Color (cr, cr, cr));
		}
		GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (cr, cr, cr));

		// Move Camera
		GameObject camera = GameObject.Find ("Main Camera");
		if (selected) {
			camera.GetComponent<CameraController> ().moveTo (transform.position);
		} else {
			camera.GetComponent<CameraController> ().backToOrigin ();
		}

	}
}
