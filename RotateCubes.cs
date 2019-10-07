using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubes : MonoBehaviour {


	public Vector3 rotation = Vector3.zero;
	public float speed = 45f;

	// Update is called once per frame
	void Update () {

		rotation.y += Time.deltaTime * speed;
		transform.localEulerAngles = rotation;

	}
}
