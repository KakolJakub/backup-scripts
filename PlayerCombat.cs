using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animate;
	public PlayerHealthBar healthBar;
	public Tint tint;
	
	public int maxHealth =100;
	int currentHealth;
	
	public Transform AttackPoint;
	public float attackRange=0.5f;
	public int attackDamage=10;
	public int critChance=80;
	public int critDamage=30;
	public LayerMask enemyLayers;
	
	public int noOfClicks=0;
	float lastClickedTime;
	//public float comboDelay;
	
	//public bool canCombo=false;
	bool canAttack=true;
	bool block=false;
	
	public float attackDelay;
	public float attackTimer;
	
    // Update is called once per frame
	void Start()
	{
		currentHealth=maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		attackDelay=0.5f;
		//comboDelay=0.4f;
		CanAttack();
		block=false;
		//canCombo=false;
	}
	
    void Update()
    {	
		/*if (Time.time-lastClickedTime>=comboDelay)
		{
			ResetCombo();
			canCombo=false;
		}
		if (Time.time-lastClickedTime<comboDelay)
		{
			canCombo=true;
		}	*/		
		//Debug.Log(block);
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //lastClickedTime=Time.time;
			if(canAttack)
			{
				noOfClicks++;
			}
			if ((noOfClicks==1)&&(canAttack))
			{
				animate.SetBool("Attack1",true);
			}
			//Attack();
        }
		if(Input.GetMouseButton(1))
		{
			CantAttack();
			animate.SetBool("Attacking",true);
			animate.SetBool("Blocking", true);
			attackTimer=attackDelay;
		}
		else
		{
			animate.SetBool("Blocking",false);
			CanAttack();
		}
		if (attackTimer >0)
		{
			attackTimer-=Time.deltaTime;
		}
		if (attackTimer <0)
		{
			attackTimer=0;
			animate.SetBool("Attacking",false);
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			animate.SetBool("Attacking",true);
			attackTimer=attackDelay;
		}
		
		
    }
    /*void Attack()
    {
        if(noOfClicks==1)
		{
			CantAttack();
			animate.SetBool("Attack1",true);
			Debug.Log("atak1");
		}
		if((noOfClicks==2)&&(canCombo))
		{
			CantAttack();
			animate.SetBool("Attack2",true);
			Debug.Log("atak2");
		}
		if((noOfClicks>=3)&&(canCombo))
		{
			CantAttack();
			animate.SetBool("Attack3",true);
			ResetCombo();
			Debug.Log("atak3");
		}	
    }*/
	public void attack1end()
	{
		animate.SetBool("Attack1",false);
		if (noOfClicks>=2)
		{
		animate.SetBool("Attack2",true);
		}
		else
		{
		animate.SetBool("Return",true);
		}
		/*else
		{
		animate.SetBool("Attack1",false);
		}*/
	}
	public void attack2end()
	{
		animate.SetBool("Attack2",false);
		if (noOfClicks>=3)
		{
		animate.SetBool("Attack3",true);
		}
		else
		{
		animate.SetBool("Return",true);
		}
		/*else
		{
		animate.SetBool("Attack2",false);
		}*/
	}
	public void attack3end()
	{
		animate.SetBool("Attack3",false);
		animate.SetBool("Return",true);
		ResetCombo();
	}
	public void ReturnEnd()
	{
		animate.SetBool("Return",false);
	}
	void ResetCombo()
	{
		noOfClicks=0;
	}
	public void CanAttack()
	{
		canAttack=true;
	}
	public void CantAttack()
	{
		canAttack=false;
	}
	public void Block()
	{
		block=true;
	}
	public void NoBlock()
	{
		block=false;
	}
	public void DealDamage()
	{
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
		foreach (Collider2D enemy in hitEnemies)
		{
			if(Chance.Proc(critChance))
			{
			enemy.GetComponent<Enemy>().TakeCriticalDamage(critDamage);
			}
			else
			{	
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
			}
		}
	}
	public void TakeDamage (int damageTaken)
	{
		if(block)
		{
			damageTaken=0;
		}
		currentHealth-=damageTaken;
		healthBar.SetHealth(currentHealth);
		if(damageTaken>0)
		{
		tint.SetTintColor();	
		}
		Debug.Log("You took "+damageTaken+" damage.");
		//play hurt animation
		
		if (currentHealth<=0)
		{
			animate.SetBool("Dead",true);
			GetComponent<Collider2D>().enabled=false;
			GetComponent<PlayerMovement>().enabled=false;
			this.enabled=false;
		}
	}
	void OnDrawGizmosSelected()
	{
		if (AttackPoint ==null)
			return;
		Gizmos.DrawWireSphere(AttackPoint.position,attackRange);
	}
}
