using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // Public relations
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPrefab;

    // Variables
    [SerializeField] float shootCooldown;
    [SerializeField] float showerDuration;
    [SerializeField] float showerRadius;
    [SerializeField] float timeBetweenBullets;

    // Logic Variables
    public State currentState;
    [SerializeField] bool isActive;
    [SerializeField] bool isShooting;
    bool inRange;
    // Local Components
    Animator animator;

    public enum State
    {
        Default,
        Shower,
        Spawning,
        Arrows,

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (currentState == State.Shower)
        {
            if (!isShooting) StartCoroutine(Shoot());
        }
        else if (currentState == State.Spawning)
        {

        }
        else if (currentState == State.Arrows)
        {

        }
        else if (currentState == State.Default)
        {

        }
    }

    IEnumerator Shoot()
    {

        isShooting = true;
        animator.Play("Shower");
        float showerEnd = Time.time + showerDuration;
        while (Time.time < showerEnd)
        {
            Crash();

            yield return new WaitForSeconds(timeBetweenBullets);
        }
        isShooting = false;

    }

    void Crash()
    {

        GameObject bullet = Instantiate(bulletPrefab, Random.insideUnitCircle * showerRadius, Quaternion.identity);
        Destroy(bullet, 2);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;

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
