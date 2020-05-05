using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tint : MonoBehaviour
{
    public Material material;
	public Color materialTintColor;
	public Color defaultColor;
	private float tintFadeSpeed;
	
	private void Awake() 
	{
		SetMaterial(material);
		material.SetColor("_Tint",defaultColor);
		materialTintColor.a=0;
		tintFadeSpeed=1f;
	}
    // Update is called once per frame
   private void Update()
    {
        if(materialTintColor.a >0)
		{
			materialTintColor.a=Mathf.Clamp01(materialTintColor.a - tintFadeSpeed * Time.deltaTime);
			material.SetColor("_Tint", materialTintColor);
		}
    }
	public void SetMaterial(Material material)
	{
		this.material=material;
	}
	public void SetTintColor ()
	{
		material.SetColor("_Tint", materialTintColor);
		materialTintColor.a=220;
		
	}
	public void SetTintFateSpeed(float tintFadeSpeed)
	{
		this.tintFadeSpeed=tintFadeSpeed;
	}
}
