using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterTubeController : MonoBehaviour {
	private const float distance = 4.0f;
	private int status;
	private float speed;
	private int index;
	private float start_y;
	private float end_y;

	// Use this for initialization
	void Start () {
		status = 0;
		speed = distance * GlobalVariables.bpm / 30;
		start_y = transform.position.y;
		end_y = start_y - distance;
	}
	
	// Update is called once per frame
	void Update () {
		if (status != 0) {
//			print (transform.position);
			move (status * speed * Time.deltaTime);
			if (transform.position.y >= start_y) {
				transform.position = new Vector3 (transform.position.x, start_y, transform.position.z);
				status = 0;
			} else if (transform.position.y <= end_y) {
				int e_type = GetComponentInParent<EmitterController> ().type;
				if (e_type == 0) {
					makeCupcake (transform.position);
				} else if (e_type == 1) {
					makeWaveCube (transform.position);
				} else if (e_type == 2) {
					makeOctaveCube (transform.position);
				} else if (e_type == 3) {
					makeEffectCube (transform.position);
				}
				status = -1;
				move (status * speed * Time.deltaTime);
			}
//			print (transform.position);
		}
	}

	public void emitCake(int idx) {
		index = idx;
		status = 1;
	}

	void makeCupcake(Vector3 pos) {
		GameObject cupcake = createGameObject (pos, "Muffin");
		float size = 50.0f - (float)index * 6.0f;
		cupcake.transform.localScale = new Vector3 (size, size, size);
		cupcake.GetComponent<CupcakeController> ().color_index = index;
		cupcake.SetActive (true);
	}

	void makeWaveCube(Vector3 pos) {
		GameObject cube = createGameObject (pos, "WaveCube");
		cube.GetComponent<WaveCubeController> ().s_type = index;
		cube.SetActive (true);
	}

	void makeOctaveCube(Vector3 pos) {
		GameObject cube = createGameObject (pos, "OctaveCube");
		cube.GetComponent<OctaveCubeController> ().octave = index - 3;
		cube.SetActive (true);
	}

	void makeEffectCube(Vector3 pos) {
		GameObject cube = createGameObject (pos, "EffectCube");
		cube.GetComponent<EffectCubeController> ().effect = index;
		cube.SetActive (true);
	}

	GameObject createGameObject(Vector3 pos, string prefab) {
		GameObject cube = (GameObject)Instantiate (Resources.Load (prefab));
		cube.transform.position = new Vector3 (
			pos.x,
			pos.y - transform.lossyScale.y/2,
			pos.z
		);
		return cube;
	}

	void move(float dy) {
		transform.position = new Vector3 (transform.position.x, transform.position.y - dy, transform.position.z);
	}
	

}
