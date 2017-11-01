using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundItemController : MonoBehaviour {
	public string audioFileName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.CompareTag("Player")) {
			playAudio ();
		}
	}

	void OnTriggerEnter(Collider other) {
		print ("YO");
		if (other.gameObject.CompareTag("Scanner")) {
			playAudio ();
		}
	}

	private void playAudio() {
		GetComponent<ChuckInstance> ().RunCode (string.Format(@"
				SndBuf buf => dac;
				me.dir() + ""{0}"" => buf.read;
				0 => buf.pos;
				1.0 => buf.gain;
				buf.length() / buf.rate() => now;
			", audioFileName));
	}
}
