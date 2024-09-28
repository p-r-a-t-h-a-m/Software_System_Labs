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
        else if (collision.gameObject.CompareTag("Health"))
        {
            addHealth(10f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            playerLife = Mathf.Clamp(playerLife + 1, 0, 5f);
            Destroy(collision.gameObject);
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
        else
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(2f); // Add delay for respawn
        playerRespawnedPositionAdjust();
    }

    private void PlayerControls(int dead)
    {
        // Called by animation event during death and respawn

        bool enable = dead != 0;

        // Set Rigidbody2D state
        rb.bodyType = enable ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;

        // Toggle rifle visibility
        GameObject rifle = transform.GetChild(0).gameObject;
        rifle.GetComponent<SpriteRenderer>().enabled = enable;

        // Enable/disable player movement and collider
        GetComponent<PlayerMovement>().enabled = enable;
        bc.enabled = enable;
    }

    private void gameOver()
    {
        Debug.Log("Gameover");
        //SceneManager.LoadScene("EndGame");
    }

    private void addHealth(float value)
    {
        playerHealth = Mathf.Clamp(playerHealth + value, 0, 100f);
    }

    private void decreaseHealth(float value)
    {
        playerHealth = Mathf.Clamp(playerHealth - value, 0, 100f);
    }

    private void playerRespawnedPositionAdjust()
    {
        transform.position = new Vector2(transform.position.x, 7.5f);
        playerHealth = 100f;
    }
}
