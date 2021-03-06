using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerController animator;
    public LayerMask Enemy;
    public Transform attackPoint;
    string attack = "Male Attack";
    private string idle = "Male Idle";

    public float attackRate;
    public float nextAttackTime;
    public float attackRange;
    public int damage;
    
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetMouseButtonDown(0)) Damage();
            if(Input.GetMouseButtonUp(0)) ResetDamage();
        }        
    }
    void Damage()
    {
        animator.GetComponent<PlayerController>().ChangeAnimationState(attack);
         Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, Enemy);
         foreach (Collider enemy in hitEnemy)
         {
             enemy.GetComponent<EnemyController>().TakeDamage(damage);
         }

    }
    void ResetDamage(){
        animator.GetComponent<PlayerController>().ChangeAnimationState(idle);

    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;

        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
