using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalBoss : MonoBehaviour
{
    private Transform firePoint1;
    private Transform firePoint2;
    private Transform leftBoss;
    private Transform rightBoss;
    private Animator anim;

    private bool isDead = false;
    private BoxCollider2D boxCollider2D;
    private Transform targetPlayer;
    private int health;
    public GameObject boss;
    public GameObject bullet1;
    public GameObject bullet2;
    private bool boss1Dead;
    private bool boss2Dead;
    private int totalBoss = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 200;
        boxCollider2D = GetComponent<BoxCollider2D>();
        targetPlayer = GameObject.Find("Player").transform;
        firePoint1 = transform.Find("BossLevel3").Find("UniverseBossFirePoint1");
        firePoint2 = transform.Find("BossLevel3").Find("UniverseBossFirePoint2");
        leftBoss = transform.Find("LeftBoss");
        leftBoss.position = new Vector3(leftBoss.position.x - 5.4f, leftBoss.position.y, leftBoss.position.z);
        rightBoss = transform.Find("RightBoss");
        rightBoss.position = new Vector3(rightBoss.position.x - 5.4f, rightBoss.position.y, rightBoss.position.z);

        if (!isDead)
        {
            InvokeRepeating("Shoot", 0, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("Collided");
            Debug.Log(health);
            Destroy(collision.gameObject);
            if (health > 100)
            {
                health--;
            }
            else if (health == 100)
            {
                health--;
                Instantiate(boss, leftBoss.position, Quaternion.identity);
                Instantiate(boss, rightBoss.position, Quaternion.identity);
            }
            else
            {

                health--;
            }
        }
        if (health == 0)
        {
            Debug.Log("health is 0");
            //anim.SetTrigger("isEnemyDead");
            anim.SetBool("dead", true);
            Debug.Log("Animator started");
            GetComponent<AudioSource>().Play();
            Debug.Log("audio played");
            Destroy(gameObject);
            Debug.Log("Game object destroyed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("next scene loaded");
            isDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Shoot()
    {
        if (health != 0)
        {
            Instantiate(bullet1, firePoint1.position, Quaternion.identity);
            Instantiate(bullet2, firePoint2.position, Quaternion.identity);
        }
    }

    public void BossDied()
    {
        totalBoss--;
        if (totalBoss == 1)
        {
            boss1Dead = true;
        }
        else if (totalBoss == 0)
        {
            boss2Dead = true;
        }
    }
}
