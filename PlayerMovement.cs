using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animate;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animate.SetFloat("Movement", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        /* (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }*/
        /*else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }*/

    }

    void FixedUpdate()
    {
        // Move our character
		if(animate.GetBool("Attacking"))
		{
			runSpeed=0f;
			animate.SetFloat("Movement",0);
			
		}
		else if(!(animate.GetBool("Attacking")))
		{
		runSpeed=40f;	
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
		}		
    }
}