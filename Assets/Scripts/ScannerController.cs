using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerController : MonoBehaviour {
	private bool isPlaying;
	private float speed;


	// Use this for initialization
	void Start () {
		isPlaying = false;
		speed = 10;
	}

	void FixedUpdate() {
		if (isPlaying) {
			Vector3 pos = transform.position;
			float z = (pos.z > 40.0f) ? -40.1f : speed * Time.deltaTime + pos.z;
			transform.position = new Vector3 (pos.x, pos.y, z);
				
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			isPlaying = !isPlaying;
		else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			isPlaying = false;
			Vector3 pos = transform.position;
			transform.position = new Vector3 (pos.x, pos.y, -40.1f);
		}
			
	}

	void OnTriggerEnter(Collider other) {
		print ("YO!!!");

	}
		
}
