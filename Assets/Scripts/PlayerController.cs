using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotationSpeed;

	private Rigidbody rb;

	private int count;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float zValue = Input.GetAxis("Vertical");
		float xValue = Input.GetAxis("Horizontal");
		rb.velocity = new Vector3 (xValue * speed, 0, zValue * speed);
//		rb.AddForce(xValue*speed, 0, zValue*speed);
//		transform.Translate(xValue*speed*Time.deltaTime, 0, zValue*speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive(false);
			count = count + 1;
			GetComponent<ChuckInstance> ().RunCode (string.Format(@"
				SinOsc foo => dac;
				repeat({0}) {{
					Math.random2f(300, 1000) => foo.freq;
					100::ms => now;
				}}
			", count));
		}
	}


}
