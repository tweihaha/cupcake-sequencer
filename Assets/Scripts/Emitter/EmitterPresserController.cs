using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterPresserController : MonoBehaviour {
	private int inactiveCount = 0;
	private int INACTIVE_LENGTH = 0;
	public int p_index;


	// Use this for initialization
	void Start () {
		INACTIVE_LENGTH = (int)(1 * 60 / (GlobalVariables.bpm * Time.deltaTime));
	}
	
	// Update is called once per frame
	void Update () {
		if (inactiveCount > 0)
			inactiveCount--;
		else {
			
		}
	}

	void OnCollisionEnter(Collision other) {
//		if (inactiveCount == 0) {
//			inactiveCount += INACTIVE_LENGTH;
//			if (other.collider.CompareTag("TriggerStick")) {
//				int index = other.collider.GetComponent<TriggerStickController> ().color_index;
//				if (index > 0) {
//					float size = index2size (index);
//					// Generate Cake
//					GameObject cupcake = (GameObject)Instantiate (Resources.Load ("Muffin"));
//					GameObject butt = transform.GetChild (2).gameObject;
//					Vector3 pos = butt.transform.position;
//					cupcake.transform.position = new Vector3 (pos.x, pos.y - 5, pos.z);
//					cupcake.transform.localScale = new Vector3 (size, size, size);
//					cupcake.SetActive (true);
//				}
//			}
//		}
	}


	float index2size(int index) {
		return (float)index * 6.0f + 20.0f;
	}
}
