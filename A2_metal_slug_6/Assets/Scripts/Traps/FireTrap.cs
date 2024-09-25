using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header ("FireTrap Timers")]
    [SerializeField] private float ActivationDelay;
    [SerializeField] private float ActiveTime;
    [SerializeField] private float damage;

    private Animator anim;
    private SpriteRenderer sprite;
    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag=="Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if(active)
            {
                collision.GetComponent<Player_Health>().TakeDamage(damage);
            }
        }    
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered=true;
        sprite.color=Color.red;
        yield return new WaitForSeconds(ActivationDelay);
        sprite.color=Color.white;
        active=true;
        anim.SetBool("activated",true);
        yield return new WaitForSeconds(ActiveTime);
        active=false;
        triggered=false;
        anim.SetBool("activated",false);
    }

}
