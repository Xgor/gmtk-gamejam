using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Type
{
	public float hi;
	public float so;
}

[RequireComponent (typeof(Rigidbody2D))]
public class ShmupPlayer : MonoBehaviour {

	public Rect boundaries;

	public float acceleration;
	public float freeFriction;
	public float fireFriction;
	float currentFriction;

	float heldBullets;
	public float maxHeldBullets;

	public Type test;

	bool isFireType = false;

	Rigidbody2D rig;
	Vector2 pos;
	Vector2 moveVec;


	float fireCooldown;
	public float fireFireRate;
	public Sprite fireBulletSprite;

	public float iceFireRate;
	public Sprite iceBulletSprite;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();
		moveVec = Vector2.zero;
	//	heldBullets = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveVec.x = Input.GetAxisRaw("Horizontal");
		moveVec.y = Input.GetAxisRaw("Vertical");
		if(Input.GetButton("Fire1"))
		{
			Fire();
			currentFriction =fireFriction;
		}
		else 
			currentFriction =freeFriction;

		rig.velocity -= currentFriction*Time.deltaTime*rig.velocity;
		rig.velocity += acceleration*Time.deltaTime*moveVec;

		ClampPosition();

		fireCooldown -= Time.deltaTime;
		Debug();
	}

	void Fire()
	{
		if(heldBullets > 0 && fireCooldown < 0)
		{

			if(isFireType)
			{

				for(int i = 0; i < 2;i++)
				{
					Vector2 offset = Vector2.up* (i-0.5f)*0.25f;
					FireBullet((Vector2)transform.position+offset,10,0,fireBulletSprite);
				}
				fireCooldown = fireFireRate;
			}
			else
			{
				for(int i = 0; i < 4;i++)
				{
					float dir = (i-1.5f)*5;
					FireBullet(transform.position,10,dir,iceBulletSprite);
				}
				fireCooldown = iceFireRate;
			}

		}
	}
	void FireBullet(Vector2 pos,float speed,float dir,Sprite spr)
	{
		heldBullets--;
		Bullet b = new GameObject().AddComponent<Bullet>();
		b.transform.position =pos;
		b.Setup(speed,dir,spr,isFireType,true);
		//		b.SetSprite(sprite);
	}

	void ClampPosition()
	{
		pos = transform.position;
		pos.x = Mathf.Clamp(pos.x,boundaries.position.x,boundaries.xMax);
		pos.y = Mathf.Clamp(pos.y,boundaries.position.y,boundaries.yMax);
		transform.position = pos;
	}

	public void HitByBullet(bool fire)
	{
		if(heldBullets > 0)
		{
			if(isFireType != fire)
			{
				Death();
			}
		}
		else
		{
			isFireType = fire;
		}
		heldBullets++;
	}

	void Death()
	{
		SceneManager.LoadScene(0);
	}

	public float BulletAmount()
	{
		return heldBullets;
	}

	void Debug()
	{
		if(Input.GetButtonDown("Fire2"))
		{
			heldBullets = 100;
			isFireType =! isFireType;
		}
	}

	public bool IsFire()
	{
		return isFireType;
	}

}