using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Public relations
    GameObject player;
    [SerializeField] GameObject arrowPrefab;

    // Variables
    [SerializeField] float speed;
    [SerializeField] float driftFactor;
    [SerializeField] float shootCooldown;


    // Logic Variables
    [SerializeField] bool isActive;
    [SerializeField] Vector2 attackRange;
    [SerializeField] bool inRange;
    bool isShooting;
    Vector3 distanceToPlayer;

    // Local components
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (player.transform.position - transform.position);
        GetComponent<SpriteRenderer>().flipX = !(distanceToPlayer.x > 0);
        if (isActive)
        {
            FollowPlayer();


        }
        if (inRange)
        {
            GetComponent<Animator>().Play("Shoot");
            if (!isShooting)
                StartCoroutine(Shoot());

            isShooting = true;
        }
        else
        {
            GetComponent<Animator>().Play("Enemy2Walk");

            isShooting = false;
            StopCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {

        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrow.GetComponent<Rigidbody2D>().velocity = distanceToPlayer.normalized * speed * 2;
        arrow.GetComponent<SpriteRenderer>().flipX = distanceToPlayer.x < 0;
        yield return new WaitForSeconds(shootCooldown);
        Destroy(arrow, 2);
        isShooting = false;
    }

    void FollowPlayer()
    {
        if (distanceToPlayer.magnitude > attackRange.x && !inRange)
        {

            Vector3 newVelocity = distanceToPlayer.normalized;
            rb.velocity = Vector3.Lerp(rb.velocity.normalized, newVelocity, driftFactor) * speed;
        }
        else
        {
            inRange = true;
        }
        if (distanceToPlayer.magnitude > attackRange.y) inRange = false;

    }
}
