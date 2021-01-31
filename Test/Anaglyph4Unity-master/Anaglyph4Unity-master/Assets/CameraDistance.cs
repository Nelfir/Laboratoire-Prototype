using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistance : MonoBehaviour {
	public GameObject cam1, cam2;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			cam1.transform.Translate (-0.01f, 0f, 0f);
			cam2.transform.Translate (0.01f, 0f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			cam1.transform.Translate (0.01f, 0f, 0f);
			cam2.transform.Translate (-0.01f, 0f, 0f);
		}
	}
}
