using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoxController : MonoBehaviour {
	private float fadePerSecond = 2.5f;
	private bool canFade = false;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canFade) {
			var material = GetComponent<Renderer> ().material;
			var color = material.color;

			material.color = new Color (color.r, color.g, color.b, color.a - Time.deltaTime/GetComponent<AudioSource> ().clip.length);
			if (material.color.a <= 0)
				this.gameObject.SetActive (false);
		}
	}

	void FixedUpdate () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			canFade = true;
			GetComponent<AudioSource> ().Play ();
		}
	}
}
