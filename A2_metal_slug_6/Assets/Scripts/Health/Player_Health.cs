using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Health : MonoBehaviour
{
    [Header("Health Objects")]
    [SerializeField] private Player_Life PlayerLife;
    [SerializeField] private HealthBar hBar;
     public float startLife { get; private set; }   // Total number of lives
     public float startHealth { get; private set; }   // Health per life
    public float currentHealth { get; private set; }
    public float currentLife { get; private set; }

    [Header("IFrames")]
    [SerializeField] private float invulnerable;
    [SerializeField] private int flashes;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isDead;
    private GameObject player;
    private BoxCollider2D bcollider;

    private void Awake()
    {
        hBar.SetMaxHealth(100);
        startHealth=100;
        startLife=3;
        currentLife=startLife;
        currentHealth=startHealth;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        bcollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }

    public void TakeDamage(float _damage)
    {
        // Reduce health based on the incoming damage
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
        hBar.SetHealth(currentHealth);

        // If health is greater than 0, trigger hurt animation and invulnerability frames
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerable());
        }
        else if (currentHealth <= 0 && currentLife >= 1)  // Health depleted but player has extra lives
        {
            currentLife--;   // Reduce one life
            anim.SetTrigger("dead");
            currentHealth=startHealth;
            
            void OnCollisionEnter2D(Collision2D other) {
                if(other.gameObject.tag != "Ground")
                    bcollider.enabled = false;
            }
            StartCoroutine(Respawning());
            bcollider.enabled=true;
        }
        else if (currentHealth == 0 && currentLife == 0)  // Health and life both depleted
        {
            if (!isDead)
            {
                anim.SetTrigger("dead");
                GetComponent<Player_Movement>().enabled = false;
                isDead = true;
            }
        }
    }

    // Method to add health
    public void AddHealth(int _health)
    {
        currentHealth = Mathf.Clamp(currentHealth+ _health, 0, 200);
        hBar.SetHealth(currentHealth);
    }

    // Method to add an extra life
    public void AddExtraLife(float _life)
    {
        currentLife = Mathf.Clamp(currentLife + _life, 0, 5);
    }

    private IEnumerator Invulnerable()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < flashes; i++)
        {
            sprite.color = new Color(1, 0, 0, 0.5f);  // Flash effect
            yield return new WaitForSeconds(invulnerable / flashes);
            sprite.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    // Respawning logic
    private IEnumerator Respawning()
    {
        yield return new WaitForSeconds(5);
        player.SetActive(false);
        player.transform.position = new Vector3(player.transform.position.x, 500, 0);
        currentHealth = startHealth;  // Reset health upon respawn
        player.SetActive(true);
    }
}
