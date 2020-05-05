using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	public Slider slider;
	
	public void SetMaxHealth(int health)
	{
		slider.maxValue=health;
		slider.value=health;
	}
	
	public void SetHealth(int health)
	{
		slider.value=health;
	}
	void Update()
	{
	if (slider.value==0)
	{
		GameObject Bar=GameObject.Find("EnemyHealthBar");
		Destroy(Bar);
		this.enabled=false;
	}
	}
}
