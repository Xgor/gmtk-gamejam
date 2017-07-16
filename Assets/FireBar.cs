using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireBar : MonoBehaviour {

	public Slider slider;
	public ShmupPlayer player;
	public Color FireColor;
	public Color IceColor;
	ColorBlock cb;


	// Use this for initialization
	void Start () {
		slider.maxValue = player.maxHeldBullets;
		cb = slider.colors;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = player.BulletAmount();
		if(player.IsFire())
			cb.disabledColor = FireColor;
		else
			cb.disabledColor = IceColor;

		slider.colors = cb;
	}
}
