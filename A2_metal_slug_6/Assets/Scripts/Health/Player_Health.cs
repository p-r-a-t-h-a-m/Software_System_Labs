using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startHealth;

    [Header ("IFrames")]
    [SerializeField] private float invulnerable;
    [SerializeField] private int flashes;
    private SpriteRenderer sprite;

    public float currentHealth {get; private set;}
    private Animator anim;
    private bool isDead;
    private void Awake()
    {
        currentHealth=startHealth;
        anim = GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth=Mathf.Clamp(currentHealth-_damage, 0, startHealth);
        currentHealth-=_damage;
        if(currentHealth>0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerable());
        }
        else
        {
            if(!isDead)
            {
            anim.SetTrigger("dead");
            GetComponent<Player_Movement>().enabled=false;
            isDead=true;
            }
            
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(0.5f);
    }

    public void AddHealth(float _health)
    {
        currentHealth=Mathf.Clamp(currentHealth+_health, 0, startHealth);
    }

    private IEnumerator Invulnerable()
    {
        Physics2D.IgnoreLayerCollision(7,8,true);
        for(int i=0;i<flashes;i++)
        {
            sprite.color=new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(invulnerable/flashes);
            sprite.color=Color.white;
            yield return new WaitForSeconds(1);
        }   
        Physics2D.IgnoreLayerCollision(7,8,false);
    }
}
