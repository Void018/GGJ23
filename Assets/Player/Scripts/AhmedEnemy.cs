using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AhmedEnemy : MonoBehaviour {
    // TODO: ADD MAX HEALTH
    public int maxHealth = 50;
    public int health;
    public bool isBoss = false;
    SpriteRenderer sr;


    void Start() {
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }


    void Update() {

    }

    public void Hit(int damage) {
        health -= damage;
        print((float)health / maxHealth);
        var newColor = new Color(
                1,
            (float)health / maxHealth,
            (float)health / maxHealth
        );
        sr.color = newColor;
        if (health <= 0) {
            Destroy(gameObject);
            if (isBoss)
                SceneManager.LoadScene("WinGameUI");
        }

    }
}
