using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    // Public relations
    GameObject player;
    [SerializeField] GameObject bulletPrefab;

    // Variables
    [SerializeField] float shootCooldown;

    // Logic Variables
    [SerializeField] bool isActive;
    [SerializeField] bool isShooting;
    bool inRange;
    // Local Components
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    IEnumerator Shoot()
    {

        isShooting = true;
        Vector3 playerPosition = player.transform.position;
        animator.Play("Enemy3Shoot");
        yield return new WaitForSeconds(shootCooldown / 2);
        Crash(playerPosition);
        isShooting = false;
        yield return new WaitForSeconds(shootCooldown);

        if (inRange) StartCoroutine(Shoot());
    }

    void Crash(Vector3 playerPosition)
    {

        GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
        Destroy(bullet, 2);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            StartCoroutine(Shoot());

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            inRange = false;
            isShooting = false;
            StopCoroutine(Shoot());
        }
    }
}
