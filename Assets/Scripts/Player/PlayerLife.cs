using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;

    [HideInInspector] internal float playerHealth = 100f;
    [HideInInspector] internal float playerLife = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            reduceHealth(5f);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            reduceHealth(0.5f);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            reduceHealth(5f);
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            addHealth(10f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Life"))
        {
            playerLife = Mathf.Clamp(playerLife + 1, 0, 5f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            reduceHealth(10f); // or however much damage you want the bomb to deal
                               // You may also want to trigger any bomb-specific logic here, like the explosion animation
        }
    }

    private void reduceHealth(float reduce)
    {
        decreaseHealth(reduce);
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        anim.SetTrigger("death");

        playerLife = Mathf.Clamp(playerLife - 1, 0, 5f);
        playerHealth = 100f;

        if (playerLife <= 0)
        {
            gameOver();
        }
    }

    private void PlayerControls(int dead)
    {
        // This function is called by event in playerDead and player spawned animation

        bool enable;

        //make player static when die
        if (dead == 0)
        {
            enable = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            enable = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        //Remove rifle from Player
        GameObject originalGameObject = GameObject.Find("Player");
        GameObject rifle = originalGameObject.transform.GetChild(0).gameObject;

        SpriteRenderer rifleSprite = rifle.GetComponent<SpriteRenderer>();
        rifleSprite.enabled = enable;

        //disable player movement controls when die.
        GetComponent<PlayerMovement>().enabled = enable;
        bc.enabled = enable;
    }

    private void gameOver()
    {
        Debug.Log("Gameover");
        //SceneManager.LoadScene(SceneManager.GetSceneByName("EndGame").name);
    }
    private void addHealth(float value)
    {
        playerHealth = Mathf.Clamp(playerHealth + value, 0, 100f); // collected positive health.
    }

    private void decreaseHealth(float value)
    {
        playerHealth = Mathf.Clamp(playerHealth - value, 0, 100f); // collected positive health.
    }

    private void playerRespawnedPositionAdjust()
    {
        transform.position = new Vector2(transform.position.x, 7.5f);
        playerHealth = 100f;
    }

}
