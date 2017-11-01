using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltElemController : MonoBehaviour {

	Rigidbody rb;
	ChuckInstance chuck;
	private float speed = -15.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		chuck = GetComponentInParent<ChuckInstance> ();
//		chuck.RunCode (@"
//			fun void cupcakeTouchdown(float intensity)
//			{
//				BandedWG snd => dac;
//				(120 - intensity)=>snd.freq;
//				1 => snd.noteOn;
//				0.3::second => now;
//			}
//			external float freqIntensity;
//			external Event touchdownHappened;
//			while(true)
//			{
//				touchdownHappened => now;
//				spork ~ cupcakeTouchdown(freqIntensity);
//			}
//		");
	}

	void FixedUpdate () {
		if (Mathf.Abs (transform.position.y - 5) < 0.5) {
			transform.position = new Vector3 (transform.position.x, 5.0f, transform.position.z);
//			rb.AddForce (0.0f, rb.mass * speed, 0.0f);
//			rb.velocity = new Vector3 (speed, 0.0f, 0.0f);
		} 
		//		else if (Mathf.Abs (transform.position.y + 5) < 0.5) {
		//			rb.AddForce (-rb.mass * speed, 0.0f, 0.0f);
		//		}
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (transform.position.y - 5) < 0.5) {
//			rb.position = new Vector3 (rb.position.x, 5.0f, rb.position.z);
			rb.AddForce (rb.mass * speed, 0.0f, 0.0f);
//			rb.velocity = new Vector3 (rb.velocity.x, 0.0f, 0.0f);
		} 
//		else if (Mathf.Abs (transform.position.y + 5) < 0.5) {
//			rb.AddForce (-rb.mass * speed, 0.0f, 0.0f);
//		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Cupcake")) {
			CupcakeController cc = other.collider.GetComponent<CupcakeController> ();
			if (!cc.touchdown) {
				cc.touchdown = true;
				float size = cc.transform.localScale.x;
				chuck.SetFloat ("freqIntensity", size);
				chuck.BroadcastEvent ("touchdownHappened");
			}
		}

	}
}
