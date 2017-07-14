using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour {

	public float acceleration;
	public float friction;
	public float groundTurnSpeed;
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
			transform.Rotate(Vector2.up*groundTurnSpeed*Input.GetAxis("Horizontal"));
		}
		else
		{
			groundFire.SetActive(false);
			transform.Rotate(Vector2.up*airTurnSpeed*Input.GetAxis("Horizontal"));
		}

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
