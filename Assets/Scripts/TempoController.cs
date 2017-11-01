using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoController : MonoBehaviour {

	bool mouseDown;
	float upperBound;
	float lowerBound;
	float length;
	Transform tr;

	public Text bpmText;

	// Use this for initialization
	void Start () {
		mouseDown = false;
		tr = GetComponent<Transform> ();
		Transform pt = GetComponentInParent<Transform> ();
		upperBound = pt.position.y + pt.lossyScale.y / 2;
		lowerBound = pt.position.y - pt.lossyScale.y / 2;
		length = pt.lossyScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		float mpy = Input.mousePosition.y;
		if (mouseDown &&  mpy <= upperBound && mpy >= lowerBound) {
			tr.position = new Vector3 (tr.position.x, mpy, tr.position.z);
			GlobalVariables.bpm = 120.0f * (tr.position.y - lowerBound) / length;
		}

	}

	void OnMouseDown() {
		mouseDown = true;
	}

	void OnMouseUp() {
		mouseDown = false;
	}
}
