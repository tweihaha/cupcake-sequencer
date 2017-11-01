using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiiterSensorController : MonoBehaviour {
	ChuckInstance chuck;
	List<GameObject> tubes;
	public bool sync = false;


	// Use this for initialization
	void Start () {
		if (sync) {
			chuck = GetComponent<ChuckInstance> ();
			float rate = Mathf.Max (GlobalVariables.bpm / 120.0f, 1.0f);
			chuck.RunCode (string.Format (@"
				external float bpm;
				{0} => bpm;
				fun void TriggerStickTouched(int idx)
				{{
					SndBuf buf => dac;
					string fileName;
					if (idx == 0) 
						""door-hinge1.wav"" => fileName;
					else
						""door-hinge2.wav"" => fileName;
					me.dir() + fileName => buf.read;
					0 => buf.pos;
					Math.max(bpm/120.0, 1.0) => buf.rate;
					0.1 => buf.gain;
					buf.length() / buf.rate() => now;
				}}
				fun void GenNoise(int i)
				{{
					if ( i > 0) {{
						SubNoise sn => Envelope env => dac;
						10 * i => sn.rate;
						0.1 => float value;
						60.0 / bpm => float time;
						value => env.target;
						time => env.time;
						value => env.value;
						env.keyOff(1);
						time::second => now;
					}}
				}}
				external int sndIdx;
				external int noiseIdx;
				external Event touchHappened;
				while(true)
				{{
					touchHappened => now;
					spork ~ TriggerStickTouched(sndIdx);
					spork ~ GenNoise(noiseIdx);
				}}
			", GlobalVariables.bpm));
		}
		tubes = new List<GameObject> ();
//		print (gen_cake_pos.Count);
		for (int i = 0; i < transform.parent.childCount; i++) {
			Transform tube = transform.parent.GetChild (i);
			if (tube.CompareTag ("EmitterTube")) {
				tubes.Add (tube.gameObject);
//				GameObject butt = presser.GetChild (2).gameObject;
//				Vector3 pos = butt.transform.position;
//				gen_cake_pos.Add(new Vector3 (pos.x, pos.y - 5, pos.z));
//				int p_index = presser.GetComponent<EmitterPresserController> ().p_index;
//				print (p_index);
//				gen_cake_pos[p_index] = new Vector3 (pos.x, pos.y - 5, pos.z);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("TriggerStick")) {
			int wheel_number = other.GetComponent<TriggerStickController> ().wheel_number;
			// Generate Sound
			if (sync) {
				chuck.SetInt ("sndIdx", wheel_number);
				chuck.SetInt ("noiseIdx", other.GetComponent<TriggerStickController> ().color_index);
				chuck.SetFloat ("bpm", GlobalVariables.bpm);
				chuck.BroadcastEvent ("touchHappened");
				GlobalVariables.counter = wheel_number;
			}
			// Make cupcake
			int index = other.GetComponent<Collider>().GetComponent<TriggerStickController> ().color_index;
			if (index > 0 && !GlobalVariables.warmingUp()) {
				tubes [wheel_number].GetComponent<EmitterTubeController>().emitCake(index);
//				GameObject cupcake = (GameObject)Instantiate (Resources.Load ("Muffin"));
//				cupcake.transform.position = gen_cake_pos[t_count];
//				cupcake.transform.localScale = new Vector3 (size, size, size);
//				cupcake.SetActive (true);
			}
			if (sync && wheel_number == tubes.Count - 1) {
				GlobalVariables.round++;
			}
		}
	}

}
