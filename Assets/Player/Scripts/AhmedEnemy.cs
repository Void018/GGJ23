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
        if (health <= 0) {
            Destroy(gameObject);
            SceneManager.LoadScene("WinGameUI");
        }

    }
}
