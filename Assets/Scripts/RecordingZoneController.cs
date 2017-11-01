using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum recStatus : int {Invalid=0, Ready=1, Recording=2};

public class RecordingZoneController : MonoBehaviour {
	private int recordStatus;
	private int recordFileCount;
	private int minFreq;
	private int maxFreq;
	private AudioSource goAudioSource;
	private string dName;
	private const float duration = 8.0f;
	private GameObject player;

	// Use this for initialization
	void Start () {
		recordStatus = (int)recStatus.Invalid;
		recordFileCount = 1;
//		AudioSource aud = GetComponent<AudioSource>();
//		print ("recording");
//		aud.clip = Microphone.Start("", true, 5, 44100);
//		print ("end");
//		aud.Play();

		if (Microphone.devices.Length <= 0) {
			Debug.LogWarning ("Mic not connected!");
		} else {
			dName = Microphone.devices[0];
			Microphone.GetDeviceCaps (dName, out minFreq, out maxFreq);
			if (minFreq == 0 && maxFreq == 0) {
				maxFreq = 44100;
			}
			goAudioSource = this.GetComponent<AudioSource> ();
			goAudioSource.loop = false;
		}
	}

	void FixedUpdate ()
	{
		if (recordStatus == (int)recStatus.Ready &&
		    Input.GetKeyDown (KeyCode.R)) {
			print ("start recording...");
			recordStatus = (int)recStatus.Recording;
			// TODO: Start recording
			goAudioSource.clip = Microphone.Start (dName, false, 1000, maxFreq);
//			GetComponent<ChuckInstance> ().RunCode (string.Format (@"
//				external int shouldBreak;
//				""rec{0}.wav"" => string fn;
//				0 => shouldBreak;
//				me.dir() + fn => fn;
//				adc => Gain g => WvOut w => dac;
//				fn => w.wavFilename;
//				1.0 => g.gain;
//				chout <= ""writing to file:"" <= ""'"" + w.filename() + ""'"" <= IO.newline();
//				null @=> w;
//				while(shouldBreak == 0) {{
//					10::ms => now;
//				}}
//				chout <= ""bye"" <= IO.newline();
//			", recordFileCount));
		} else if (recordStatus == (int)recStatus.Recording &&
		           Input.GetKeyUp (KeyCode.R)) {
			print ("stop recording.");
			recordStatus = (int)recStatus.Ready;
			// TODO: Stop recording
//			GetComponent<ChuckInstance>().SetInt("shouldBreak", 1);
			int pos = Microphone.GetPosition (dName);
			Microphone.End (dName);
//			print (goAudioSource.clip.length);
			goAudioSource.clip = trimAudioClip (goAudioSource.clip, pos);
			string fn = "rec"+recordFileCount.ToString()+".wav";
			SavWav.Save (fn, goAudioSource.clip);
			recordFileCount++;

			GameObject soundItem = (GameObject)Instantiate (Resources.Load ("SoundItem"));
			soundItem.GetComponent<SoundItemController> ().audioFileName = fn;
			float length = goAudioSource.clip.length * 40.0f / duration;
			soundItem.transform.localScale = new Vector3 (1.0f, length, 1.0f);
			Vector3 p_pos = player.transform.position;
			float x = Mathf.Ceil ((p_pos.x + 10) / 4) * 4 - 12;
			float z = p_pos.z;
			soundItem.transform.position = new Vector3 (x, 0, z);


//			goAudioSource.Play ();
//			print (goAudioSource.clip.length);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision other) {
		if (other.collider.gameObject.CompareTag ("Player")) {
			recordStatus = (int)recStatus.Ready;
			print ("enter");
			player = other.collider.gameObject;
		}

	}

	void OnCollisionExit (Collision other) {
		if (other.collider.gameObject.CompareTag ("Player")) {
			if (recordStatus == (int)recStatus.Recording) {
				print ("stop recording since exit.");
				// TODO: Stop recording
			}
			recordStatus = (int)recStatus.Invalid;
			print ("exit");
			player = null;
		}

//		goAudioSource.volume = 1.0f;
//		goAudioSource.Play ();
	}

	AudioClip trimAudioClip(AudioClip c, int position) {
		var data = new float[c.samples * c.channels];
		c.GetData (data, 0);
		var newData = new float[position * c.channels];

		for (int i = 0; i < newData.Length; i++) {
			newData [i] = data [i];
		}
		AudioClip newClip = AudioClip.Create (c.name, position, c.channels, c.frequency, false, false);
		newClip.SetData(newData, 0);
		return newClip;
	}
}
