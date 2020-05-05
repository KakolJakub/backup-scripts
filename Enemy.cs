using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth =100;
	int currentHealth;
	public int enemyDamage=20;
	
	bool combo=false;
	
	public Animator animateEnemy;
	public GameObject damageText;
	public EnemyHealthBar healthBar;
	
	public Transform aggroPoint;
	public Transform hitPoint;
	public Transform attackPoint;
	
	public float aggroRange =8;
	public float attackDistance=2;
	public float attackRange=1.25f;
	
	public LayerMask playerLayer;
	
	public EnemyTint eTint;
	
	// Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
		healthBar.SetMaxHealth(currentHealth);
		combo=false;
    }
	void Update()
	{
		EnemyLogic();
	}
	
	
	public void TakeDamage (int damage)
	{
		if(damageText!=null)
		{
			ShowDamage(damage.ToString());
		}
		currentHealth-=damage;
		healthBar.SetHealth(currentHealth);
		eTint.SetTintColor();
		Debug.Log("Enemy took "+damage+" damage.");
		//play hurt animation
		
		if (currentHealth<=0)
		{
			Die();
		}
	}
	public void TakeCriticalDamage (int damage)
	{
		if(damageText!=null)
		{
			ShowCriticalDamage(damage.ToString());
		}
		currentHealth-=damage;
		healthBar.SetHealth(currentHealth);
		eTint.SetTintColor();
		Debug.Log("Enemy took "+damage+" critical damage.");
		//play hurt animation
		
		if (currentHealth<=0)
		{
			Die();
		}
	}
	public void DealDamageToPlayer ()
	{
		Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
		foreach (Collider2D playerDetected in hitPlayer)
		{
			playerDetected.GetComponent<PlayerCombat>().TakeDamage(enemyDamage);
		}
	}
	void Die()
	{
		Debug.Log("Enemy died");
		animateEnemy.SetBool("eDead",true);
		//disable the enemy
		GetComponent<Collider2D>().enabled=false;
		//GetComponent<Animator>().enabled=false;
		this.enabled=false;
	}
	void ShowDamage(string a)
	{
		var x = Instantiate(damageText, transform.position, Quaternion.identity, transform);
		x.GetComponent<DamageNumberLogic>().text=a;
	}
	void ShowCriticalDamage(string a)
	{
		var x = Instantiate(damageText, transform.position, Quaternion.identity, transform);
		x.GetComponent<DamageNumberLogic>().textMesh.color=Color.yellow;
		x.GetComponent<DamageNumberLogic>().text=a;
	}
	void OnDrawGizmosSelected()
	{
		if (aggroPoint==null)
			return;
		if (hitPoint==null)
			return;
		if (attackPoint==null)
			return;
		Gizmos.DrawWireSphere(aggroPoint.position, aggroRange);
		Gizmos.DrawWireSphere(hitPoint.position, attackDistance);
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		
	}
	void EnemyLogic()
	{
		Collider2D[] detectPlayer = Physics2D.OverlapCircleAll(aggroPoint.position, aggroRange, playerLayer);
		Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitPoint.position, attackDistance, playerLayer);
		
		if(detectPlayer.Length>0)
		{
		//Debug.Log("Enemy1 sees you");
		animateEnemy.SetBool("CombatMode",true);
		}
		else
		{
		animateEnemy.SetBool("CombatMode",false);
		//Debug.Log("Enemy1 doesn't see you");
		}
		
		if (hitPlayer.Length>0&&combo==false)
		{
			animateEnemy.SetBool("eAttack1",true);
			//Debug.Log("Enemy1 attacks you");
		}
		else
		{
			animateEnemy.SetBool("eAttack1",false);
			//Debug.Log("Enemy1 doesnt attack you");
		}
		if(hitPlayer.Length>0&&combo==true)
		{
			animateEnemy.SetBool("eAttack2",true);
		}
		else
		{
			animateEnemy.SetBool("eAttack2",false);
		}
		
	}
	public void ResetEnemyCombo()
	{
		combo=false;
	}
	public void ActivateEnemyCombo()
	{
		combo=true;
	}
}
