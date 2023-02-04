using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] float damage;
    PlayerCombat player;
    private void Start()
    {
        player = PlayerController.instance.GetComponent<PlayerCombat>();
    }
    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = GetComponent<Rigidbody2D>().velocity.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        player.TakeDamage(damage);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(this, .2f);
    }

}
