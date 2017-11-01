using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	private Vector3 origin;
	private Vector3 start;
	public Vector3 target;
	private float speed = 100.0f;
	private float dist;
	private float startTime;

	// Use this for initialization
	void Start () {
		origin = start = target = transform.position;
		offset = new Vector3(10.0f, 20.0f, -50.0f);
		dist = 1.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (start != target) {
			float distCovered = (Time.time - startTime) / 0.3f;
//			float fracJourney = distCovered / dist;
			transform.position = Vector3.Lerp (start, target, Mathf.Min(distCovered, 1.0f));
		}
	}

	public void moveTo(Vector3 v) {
		start = transform.position;
		target = v + offset;
		dist = Vector3.Distance (start, target);
		startTime = Time.time;
	}

	public void moveToHorizontal(Vector3 v) {
		start = transform.position;
		target = v;
		target.z -= 50.0f;
		dist = Vector3.Distance (start, target);
		startTime = Time.time;
	}

	public void backToOrigin() {
		start = transform.position;
		target = origin;
		dist = Vector3.Distance (start, target);
		startTime = Time.time;
	}
}
