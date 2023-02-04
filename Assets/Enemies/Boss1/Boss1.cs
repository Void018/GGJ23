using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {
    // Public relations
    GameObject player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject[] enemiesPrefabs;

    // Variables
    [SerializeField] float shootCooldown;
    [SerializeField] float showerDuration;
    [SerializeField] float showerRadius;
    [SerializeField] float timeBetweenBullets;

    // Logic Variables
    public State currentState;
    [SerializeField] float timeBetweenStates = 3;
    [SerializeField] bool isActive;
    [SerializeField] bool isShooting;
    [SerializeField] bool inState;
    bool inRange;
    // Local Components
    Animator animator;

    public enum State {
        Default,
        Shower,
        Spawning,
        Arrows,

    }

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if (currentState == State.Shower) {
            if (!isShooting) {
                StartCoroutine(Shoot());
                animator.Play("Shower");
            }
        } else if (currentState == State.Spawning) {
            if (!inState) {
                StartCoroutine(Spawn());
                animator.Play("Default");
            }
        } else if (currentState == State.Arrows) {
            if (!inState) {
                StartCoroutine(Arrows());
                animator.Play("Shower");
            }
        } else if (currentState == State.Default) {
            if (!inState) {
                StartCoroutine(StartState());
                animator.Play("Default");
            }
        }
    }
    IEnumerator Arrows() {
        inState = true;
        isShooting = true;
        animator.Play("Arrows");
        float showerEnd = Time.time + showerDuration;
        while (Time.time < showerEnd) {
            ArrowShoot();

            yield return new WaitForSeconds(timeBetweenBullets / 2);
        }
        isShooting = false;
        currentState = default;
        inState = false;
    }
    void ArrowShoot() {
        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrow.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * 10;
        // yield return new WaitForSeconds(shootCooldown);
        Destroy(arrow, 2);
        isShooting = false;

    }
    IEnumerator StartState() {
        inState = true;
        yield return new WaitForSeconds(timeBetweenStates);
        List<State> states = new List<State>();
        states.Add(State.Arrows);
        states.Add(State.Spawning);
        states.Add(State.Shower);
        inState = false;
        currentState = states[Random.Range(0, 3)];
    }
    IEnumerator Shoot() {

        isShooting = true;
        animator.Play("Shower");
        float showerEnd = Time.time + showerDuration;
        while (Time.time < showerEnd) {
            Crash();

            yield return new WaitForSeconds(timeBetweenBullets);
        }
        isShooting = false;
        currentState = default;


    }

    void Crash() {

        GameObject bullet = Instantiate(bulletPrefab, (Vector2)transform.position + (Random.insideUnitCircle * showerRadius), Quaternion.identity);
        Destroy(bullet, 2);

    }
    IEnumerator Spawn() {
        inState = true;

        GameObject enemy1 = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], new Vector2(Random.Range(-showerRadius, showerRadius), 17), Quaternion.identity);
        GameObject enemy2 = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], new Vector2(Random.Range(-showerRadius, showerRadius), 20), Quaternion.identity);
        while (enemy1.transform.position.y > 1) {
            enemy1.transform.Translate(Vector2.down);
            yield return new WaitForEndOfFrame();
        }
        while (enemy2.transform.position.y > 1) {
            enemy2.transform.Translate(Vector2.down);
            yield return new WaitForEndOfFrame();
        }
        currentState = default;
        inState = false;

    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         inRange = true;

    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {

    //     if (other.CompareTag("Player"))
    //     {
    //         inRange = false;
    //         isShooting = false;
    //         StopCoroutine(Shoot());
    //     }
    // }
}
