using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour {

	public Transform target;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		if(offset == Vector3.zero)
		{
			offset = transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = offset+target.position;
	}
}
