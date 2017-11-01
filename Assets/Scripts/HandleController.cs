using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//		if (mouseDown) {
//			Vector3 pos = transform.position;
//			Vector3 mpos = Input.mousePosition;
//			float d = Mathf.Sqrt (Mathf.Pow (mpos.x - pos.x, 2.0f) + Mathf.Pow (mpos.y - pos.y, 2.0f));
//			float angle = Mathf.Rad2Deg * Mathf.Atan ((mpos.y - pos.y) / (mpos.x - pos.x));
//			if ((mpos.y - pos.y) < 0) {
//				angle = 180 - angle;
//			}
//			angle = angle - transform.rotation.eulerAngles.y;
//			transform.Rotate(new Vector3(0.0f, angle, 0.0f));
////			print(angle);
//
////			transform.rotation = Quaternion.Euler (-90.0f, 0.0f, angle);
//		}
	}

	void OnMouseDown() {
		GlobalVariables.action = !GlobalVariables.action;
		if (GlobalVariables.action) {
			transform.GetChild (1).localPosition = new Vector3 (0.0f, 1.0f, 0.0f);
		} else {
			transform.GetChild (1).localPosition = new Vector3 (0.0f, 2.0f, 0.0f);
		}
		GlobalVariables.round = 0;
	}
}
