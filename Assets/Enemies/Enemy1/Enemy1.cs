using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
    // Public relations
    PlayerCombat player;

    // Variables
    [SerializeField] float speed;
    [SerializeField] float driftFactor;
    [SerializeField] float damage;

    // Logic Variables
    [SerializeField] bool isActive;
    [SerializeField] float attackRange;
    Vector3 distanceToPlayer;

    // Local components
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start() {
        player = PlayerController.instance.GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        distanceToPlayer = (player.transform.position - transform.position);
        GetComponent<SpriteRenderer>().flipX = !(distanceToPlayer.x > 0);
        // if (isActive)
        if (distanceToPlayer.magnitude <= 15) {
            FollowPlayer();


        }
    }
    void FollowPlayer() {
        if (distanceToPlayer.magnitude > attackRange) {
            Vector3 newVelocity = distanceToPlayer.normalized;
            rb.velocity = Vector3.Lerp(rb.velocity.normalized, newVelocity, driftFactor) * speed;
        } else {
            player.TakeDamage(damage);
        }

    }
}
