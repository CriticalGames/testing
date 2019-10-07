using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour {

	public Vector3 rotation = Vector3.zero;

	public float offset = -20f;
	public float max = 220f;
	public float speed = 20f;

	// Update is called once per frame
	void Update () {

		rotation.y = -(offset + Mathf.Max( Time.time * speed, max ));
		transform.localEulerAngles = rotation;

	}
}
