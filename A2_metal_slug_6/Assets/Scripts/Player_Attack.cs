using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Animator anim;
    private Player_Movement playerMovement;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer=Mathf.Infinity;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        playerMovement=GetComponent<Player_Movement>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Z) && cooldownTimer>=attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer+=Time.deltaTime;
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer=0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}