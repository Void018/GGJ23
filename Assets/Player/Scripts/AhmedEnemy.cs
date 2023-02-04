using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhmedEnemy : MonoBehaviour
{
    // TODO: ADD MAX HEALTH
    public int maxHealth = 50;
    public int health;
    SpriteRenderer sr;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }


    void Update()
    {

    }

    public void Hit(int damage)
    {
        health -= damage;
        var newColor = Color.Lerp(Color.red, Color.white, (float)health / maxHealth);
        sr.color = newColor;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
