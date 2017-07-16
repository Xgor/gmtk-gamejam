using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class BulletSpawn : MonoBehaviour {
	
	public float fireRate;
	float fireCooldown;
	public Sprite bulletSprite;
//	public Sprite fireBulletSprite;
//	public Sprite iceBulletSprite;
	public float spead;
	public int bulletAmount;
	public float aim;
	public float speed;
	public bool aimingAtPlayer;
	public bool isFire;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fireCooldown-= Time.deltaTime;
		if(fireCooldown < 0)
		{
			fireCooldown =fireRate; 
			FireBullet(transform.position,speed,aim,bulletSprite);

		}
	}

	void FireBullet(Vector2 pos,float speed,float dir,Sprite spr)
	{
		Bullet b = new GameObject().AddComponent<Bullet>();
		b.transform.position =pos;
		b.Setup(speed,dir,spr,isFire,false);
	}
}
