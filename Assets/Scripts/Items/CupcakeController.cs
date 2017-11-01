using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeController : MonoBehaviour {
	ChuckInstance chuck;
	public bool touchdown;
	public int color_index;
	public int s_type;
	public int octave;
	public int effect;

	// Use this for initialization
	void Start () {
		chuck = GetComponent<ChuckInstance> ();
		touchdown = false;
		s_type = 0;
		octave = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddForce (0.0f, -50*rb.mass, 0.0f);
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.CompareTag ("BeltElement") && !touchdown) {
			GetComponent<Rigidbody> ().velocity = other.rigidbody.velocity;
			touchdown = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("WaveCube")) {
			s_type = other.gameObject.GetComponent<WaveCubeController> ().s_type;
			Object.Destroy (other.gameObject);
			Material m = GetComponent<Renderer> ().material;
			Color tc = new Color (0.375f, 0.25f, 0.1875f);
			m.color = Color.Lerp (m.color, tc, (float)s_type / 5.0f);
		} else if (other.gameObject.CompareTag ("OctaveCube")) {
			octave = other.gameObject.GetComponent<OctaveCubeController> ().octave;
			Object.Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("EffectCube")) {
			effect = other.gameObject.GetComponent<EffectCubeController> ().effect;
			Object.Destroy (other.gameObject);
		}
	}
}
