using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberLogic : MonoBehaviour
{
	public float destroyTime = 0.9f;
	public Vector2 Randomize = new Vector2(1,1);
	
	public Animator textAnimate;
	
	public TextMeshPro textMesh;
	public string text="0";
	
    void Start()
    {
        textMesh.text=text;
		transform.localPosition = new Vector2(Random.Range(-Randomize.x, Randomize.x), Random.Range(-Randomize.y, Randomize.y));
		Destroy(gameObject, destroyTime);
    }
}
