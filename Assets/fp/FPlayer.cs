using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class FPlayer : MonoBehaviour {

	public float jumpPower;
	public float acceleration;
	public float friction;
	public float mouseSensitivity;

	public bool OnGround;
	Rigidbody rig;

	Vector3 moveVec;
	Vector3 v;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		CameraMovement();
		OnGround = Physics.BoxCast(transform.position-Vector3.down,Vector3.one,Vector3.down,Quaternion.identity,1);
		if(OnGround)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				rig.AddForce(Vector3.up *jumpPower);
			}
		}
	}

	void FixedUpdate()
	{
		Movement();
	}

	void Movement()
	{
		moveVec.x = Input.GetAxisRaw("Horizontal");
		moveVec.z = Input.GetAxisRaw("Vertical");
	
		moveVec = transform.TransformDirection(moveVec);

		print(moveVec);
		v = rig.velocity;
		v -= rig.velocity *friction *Time.deltaTime;
		v += moveVec *acceleration *Time.deltaTime;
		v.y = rig.velocity.y;
		rig.velocity = v;
	}

	void CameraMovement()
	{
		transform.Rotate(mouseSensitivity*Vector2.up*Input.GetAxisRaw("Mouse X"));
		Camera.main.transform.Rotate(mouseSensitivity*Vector2.left*Input.GetAxisRaw("Mouse Y"));
	}
}
