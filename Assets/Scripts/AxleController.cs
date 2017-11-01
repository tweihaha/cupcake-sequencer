using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxleController : MonoBehaviour {
	public float rotateSpeed = 0;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalVariables.action) {
			rb.AddTorque (0.0f, 0.0f, rb.mass * GlobalVariables.bpm / 3.0f);
		}
//		transform.Rotate (0.0f, rotateSpeed * Time.deltaTime, 0.0f);
	}
}
