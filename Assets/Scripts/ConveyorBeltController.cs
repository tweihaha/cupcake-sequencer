using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ChuckInstance> ().RunCode (@"
			fun void cupcakeTouchdown(float intensity)
			{
				BandedWG snd => dac;
				(120 - intensity)=>snd.freq;
				1 => snd.noteOn;
				0.3::second => now;
			}
			external float freqIntensity;
			external Event touchdownHappened;
			while(true)
			{
				touchdownHappened => now;
				spork ~ cupcakeTouchdown(freqIntensity);
			}
		");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
