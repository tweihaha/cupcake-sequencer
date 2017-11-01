using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaterController : MonoBehaviour {
	ChuckInstance chuck;
	List<Vector4> cache;
	public int eater_number;
	private bool played;
	private int current_index;
	private int mouth_stat;
	private float mouth_speed;
	private const float mouth_ro = 0.333f;
	private GameObject mouth;


	// Use this for initialization
	void Start () {
		chuck = GetComponent<ChuckInstance> ();
		chuck.RunCode (string.Format(@"
			external float bpm;
			{0} => bpm;
			fun void createSound(int midi, int type, int effect)
			{{
				JCRev r => Echo e => Echo e2 => dac;
				r => dac;
				240::ms => e.max => e.delay;
				480::ms => e2.max => e2.delay;
				.3 => e.gain;
				.2 => e2.gain;
				.03 => r.mix;
				StkInstrument snd;
				if (type == 1) {{
					Mandolin snd => r;
					play(snd, midi, effect);
				}} else if (type == 2) {{
					BlowHole snd => r;
					play(snd, midi, effect);
				}} else if (type == 3) {{
					ModalBar snd => r;
					play(snd, midi, effect);
				}} else if (type == 4) {{
					VoicForm snd => r;
					play(snd, midi, effect);
				}} else if (type == 5) {{
					TubeBell snd => r;
					play(snd, midi, effect);
				}} else {{
					BeeThree snd => r;
					play(snd, midi, effect);
				}}
			}}

			fun void play(StkInstrument snd, int midi, int effect) {{
				60.0 / bpm => float time;
				if (effect == 0) {{
					Std.mtof(midi) => snd.freq;
					1 => snd.noteOn;
					0.2::second => now;
					0 => snd.noteOff;
					0.8::second => now;
				}} else if (effect == 1) {{
					time / 4.0 => float pt;
					for (0 => int i; i < 4; i++) {{
						Std.mtof(midi + i) => snd.freq;
						1 => snd.noteOn;
						pt::second => now;
						0 => snd.noteOff;
					}}
					if (time < 1) {{
						(1 - time)::second => now;
					}}
				}} else if (effect == 2) {{
					time / 4.0 => float pt;
					for (3 => int i; i >= 0; i--) {{
						Std.mtof(midi + i) => snd.freq;
						1 => snd.noteOn;
						pt::second => now;
						0 => snd.noteOff;
					}}
					if (time < 1) {{
						(1 - time)::second => now;
					}}
				}} else if (effect == 3) {{
					time / 100.0 => float pt;
					for (0 => int i; i < 100; i++) {{
						Std.mtof(midi + i * 0.04) => snd.freq;
						1 => snd.noteOn;
						pt::second => now;
						0 => snd.noteOff;
					}}
					if (time < 1) {{
						(1 - time)::second => now;
					}}
				}} else if (effect == 4) {{
					time / 100.0 => float pt;
					for (99 => int i; i >= 0; i--) {{
						Std.mtof(midi + i * 0.04) => snd.freq;
						1 => snd.noteOn;
						pt::second => now;
						0 => snd.noteOff;
					}}
					if (time < 1) {{
						(1 - time)::second => now;
					}}
				}} else if (effect == 5) {{
					SndBuf buf => dac;
					""GetSchwifty.wav"" => string fileName;
					me.dir() + fileName => buf.read;
					0 => buf.pos;
					60.0 / (time * 106.0) => buf.rate;
					1.0 => buf.gain;
					buf.length() / buf.rate() => now;
				}}

			}}

			external int midi;
			external int type;
			external int effect;
			external Event genSound;
			while(true)
			{{
				genSound => now;
				spork ~ createSound(midi, type, effect);
			}}
		", GlobalVariables.bpm));
		cache = new List<Vector4> ();
		mouth_stat = 0;
		mouth_speed = -mouth_ro * GlobalVariables.bpm * 3.0f;
		mouth = transform.GetChild (1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// Sounds
		if (GlobalVariables.counter == eater_number) {
			if (!played && cache.Count > 0) {
				// Generate Sound
				Vector4 v = cache[0];
				current_index = (int)v.x;
				int s_type = (int)v.y;
				int octave = (int)v.z;
				int effect = (int)v.w;
				cache.RemoveAt (0);
				chuck.SetInt ("midi", GlobalVariables.midi_list[current_index] + octave * 12);
				chuck.SetInt ("type", s_type);
				chuck.SetInt ("effect", effect);
				chuck.SetFloat ("bpm", GlobalVariables.bpm);
				chuck.BroadcastEvent ("genSound");

				played = true;
			}
			if (current_index > 0 && current_index < GlobalVariables.color_list.Count)
				change_color(GlobalVariables.color_list[current_index]);
//			distance * GlobalVariables.bpm / 30;
		} else {
			played = false;
		}

		// Mouth
//		if (mouth_stat != 0) {
//			mouth.transform.Rotate (0.0f, mouth_stat * mouth_speed * Time.deltaTime, 0.0f);
//			print (mouth.transform.rotation.y);
//			if (mouth.transform.rotation.y >= mouth_ro) {
//				mouth_stat = -1;
//				mouth.transform.Rotate (0.0f, mouth_stat * mouth_speed * Time.deltaTime, 0.0f);
//			} else if (mouth.transform.rotation.y <= 0.0f) {
//				mouth_stat = 0;
//				mouth.transform.rotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
//			}
//		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Cupcake")) {
			CupcakeController cc = other.GetComponent<CupcakeController> ();
			Vector4 v = new Vector4 ((float)cc.color_index, (float)cc.s_type, (float)cc.octave, cc.effect);
			cache.Add (v);
//			mouth_stat = 1;
			
//			Object.Destroy (other.gameObject);
		}
	}

	void change_color(Color target) {
		Material m = GetComponent<Renderer> ().material;
		Material mm = mouth.GetComponent<Renderer> ().material;
		Color diff = (target - m.color) * 0.5f;
		m.color = m.color + diff;
		mm.color = mm.color + diff;
	}

}
