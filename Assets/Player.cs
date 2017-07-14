using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour {

	public float acceleration;
	public float friction;

	public float turnAcceleration;
	public float groundTurnFriction;
	public float airTurnFriction;
	float currentTurnSpeed;

	public float airTurnSpeed;
	public float jetpackPower;
	public float jetpackFuel;
	float currentFuel;
	public GameObject groundFire;
	public GameObject airFire;

	Vector3 v;
	Rigidbody rig;
	bool onGround;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody>();

	}

	void Update()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () {



		onGround =transform.position.y < 0;
			

		if(onGround)
		{

			groundFire.SetActive(true);
			Movement();
			currentFuel = jetpackFuel;
			currentTurnSpeed -= Mathf.Abs(currentTurnSpeed) *groundTurnFriction *Time.deltaTime;
		}
		else
		{
			groundFire.SetActive(false);
			currentTurnSpeed -= Mathf.Abs(currentTurnSpeed) *airTurnFriction *Time.deltaTime;
		}

		currentTurnSpeed += turnAcceleration * Input.GetAxis("Horizontal")*Time.deltaTime;

		transform.Rotate(Vector2.up*currentTurnSpeed*Input.GetAxis("Horizontal"));
		Jetpack();
	}

	void Movement()
	{
		v = rig.velocity;
		v -= friction * v *Time.deltaTime;
		v += acceleration * transform.forward * Time.deltaTime;
		v.y = rig.velocity.y;
		rig.velocity = v;
	}

	void Jetpack()
	{
		if(Input.GetButton("Fire1") && 0 < currentFuel)
		{
			airFire.SetActive(true);
			currentFuel -= Time.deltaTime;
			rig.AddForce(Vector3.up*jetpackPower);
		}
		else
		{
			airFire.SetActive(false);
		}
	}
}
