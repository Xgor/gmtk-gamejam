using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

	public float speed;
//	public float dir;
	Rigidbody2D rig;
	bool byPlayer;
	bool isFire;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();
		rig.gravityScale =0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector2.right*speed*Time.deltaTime);
	}


	public void Setup(float spd, float dir,Sprite spr,bool fire,bool player)
	{
		transform.rotation = Quaternion.Euler(0,0,dir);
		speed = spd;
		gameObject.AddComponent<SpriteRenderer>().sprite = spr;
		gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
		byPlayer = player;
		isFire = fire;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		print("eh");
		ShmupPlayer p = col.GetComponent<ShmupPlayer>();
		if(p != null && !byPlayer)
		{
			
			p.HitByBullet(isFire);
			Destroy(gameObject);
		}
		else
		{
			Enemy e = col.GetComponent<Enemy>();
			if(e != null && byPlayer)
			{
				e.HitByBullet(isFire);
				Destroy(gameObject);
			}

		}

	}
}
