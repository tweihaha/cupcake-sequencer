using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStickController : MonoBehaviour {
	public int color_index;
	public KeyCode keycode;
	public int wheel_number;
	TriggerController trigger;

	// Use this for initialization
	void Start () {
//		GetComponent<Renderer>().material.color = new Color(
		Color origin = GetComponent<Renderer>().material.color;
		color_index = 0;
		trigger = transform.parent.parent.GetComponent<TriggerController> ();
	}

	void FixedUpdate () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (keycode) && trigger.selected) {
			switchColor ();
		}
	}

	void OnMouseDown() {
		if (trigger.selected) {
			switchColor ();
		}
	}

	void switchColor() {
		color_index = (color_index + 1) % GlobalVariables.color_list.Count;
		GetComponent<Renderer> ().material.color = GlobalVariables.color_list [color_index];
	}
}
