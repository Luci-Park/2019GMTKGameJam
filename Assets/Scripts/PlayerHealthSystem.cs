using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public GameObject LoseScreen;
    public Shake shake;

  
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject HurtParticle;
    private Animator anim;

    int maxHeart = 7;
    float invincibleTime;
    const float maxInvincibleTime = 1f;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        invincibleTime = maxInvincibleTime;

    }

    // Update is called once per frame
    void Update()
    {
        invincibleTime -= Time.deltaTime;
    }

    public void GiveHealth(int health)
    {
        this.health += health; 
        SetHearts();
        if (this.health > maxHeart)
        {
            this.health = maxHeart;
        }
    }

   public void TakeDamage(int damage)
    {
        if (invincibleTime > 0) return;
        invincibleTime = maxInvincibleTime;
        print(damage);
        shake.ShakeStart(0.2f, 1, 1f);
        anim.SetTrigger("Hurt");
        Instantiate(HurtParticle, transform.position, Quaternion.identity);
        health -= damage;
        SetHearts();
        if (health <= 0)
        {
            Destroy(gameObject);
            LoseScreen.SetActive(true);
        }
    }

    void SetHearts()
    {
        Debug.Log("sethearts");
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
