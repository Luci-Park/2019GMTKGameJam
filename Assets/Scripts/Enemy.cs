using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Variables Do Not Touch")]
	public int damage;
	public int health;
	public float speed;
    public int KindOfMonster;

    [Header("Variables You Can Touch")]

    private int maxDamage;
    private int minDamage;

    private int maxHealth;
    private int minHealth;

    private int maxSpeed;
    private int minSpeed;


    [Header("Tank")]
    public int TankMaxDamage;
    public int TankMinDamage;

    public int TankMaxHealth;
    public int TankMinHealth;

    public int TankMaxSpeed;
    public int TankMinSpeed;

    [Header("Speedy")]
    public int SpeedyMaxDamage;
    public int SpeedyMinDamage;

    public int SpeedyMaxHealth;
    public int SpeedyMinHealth;

    public int SpeedyMaxSpeed;
    public int SpeedyMinSpeed;

    [Header("Normal")]
    public int NormalMaxDamage;
    public int NormalMinDamage;

    public int NormalMaxHealth;
    public int NormalMinHealth;

    public int NormalMaxSpeed;
    public int NormalMinSpeed;

    [Header("Components")]

    public float timeBetweenAttack;
    private float startTimeBetweenAttack;

    public GameObject HurtParticle;
    private Transform target;
    public Rigidbody2D rb;
    public bool testHurt;
    private Animator anim;
    public SpriteRenderer EnemyGFX;
    public Sprite NormalSprite;
    public Sprite TankSprite;
    public Sprite SpeedySprite;



    private void Awake()
    {
        KindOfMonster = Random.Range(1, 5);
        
    }

    public void Start()
    {
       


        if (KindOfMonster == 1 || KindOfMonster == 2)
        {
            maxDamage = NormalMaxDamage;
            minDamage = NormalMinDamage;

            maxHealth = NormalMaxHealth;
            minHealth = NormalMinHealth;

            maxSpeed = NormalMaxSpeed;
            minSpeed = NormalMinSpeed;


            EnemyGFX.sprite = NormalSprite;
        }
        if (KindOfMonster == 3)
        {
            maxDamage = TankMaxDamage;
            minDamage = TankMinDamage;

            maxHealth = TankMaxHealth;
            minHealth = TankMinHealth;

            maxSpeed = TankMaxSpeed;
            minSpeed = TankMinSpeed;

            EnemyGFX.sprite = TankSprite;
        }
        if (KindOfMonster == 4)
        {
            maxDamage = SpeedyMaxDamage;
            minDamage = SpeedyMinDamage;

            maxHealth = SpeedyMaxHealth;
            minHealth = SpeedyMinHealth;

            maxSpeed = SpeedyMaxSpeed;
            minSpeed = SpeedyMinSpeed;

            EnemyGFX.sprite = SpeedySprite;
        }

        damage = Random.Range(minDamage, maxDamage);
        speed = Random.Range(minSpeed, maxSpeed);
        health = Random.Range(minHealth, maxHealth);


        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        timeBetweenAttack -= Time.deltaTime;


        if (target == null)
        {
            return;
        }
        else
        {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }


        if (health < 0)
        {
            Die();
        }
        if (testHurt)
        {
            testHurt = false;
            TakeDamage(damage);
        }
      

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
        }


    }


    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Enemy Hit");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Enemy Hit");
        Instantiate(HurtParticle, transform.position,Quaternion.identity);
        health -= damage;
        anim.SetTrigger("Hurt");
    }

}
