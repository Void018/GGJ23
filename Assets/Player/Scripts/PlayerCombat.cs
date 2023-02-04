using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Setup")]
    public Collider2D attackSensor;
    private ContactFilter2D filter = new ContactFilter2D().NoFilter();
    private PlayerAnimation playerAnimation;


    [Space]

    [Header("Attack Parameters")]
    public int attackDamage;
    public float maxHP;
    public float HP;
    bool isDamaged;
    [SerializeField] float damageCooldown;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        playerAnimation.SwingSword();

        var colliders = new List<Collider2D>();
        attackSensor.OverlapCollider(filter, colliders); // output to colliders list

        colliders
            .Where(c => (c.CompareTag("Enemy")))
            .ToList()
            .ForEach(e => e.GetComponent<AhmedEnemy>().Hit(attackDamage));

    }
    public void TakeDamage(float damage)
    {
        if (isDamaged) return;

        HP -= damage;

        if (HP <= 0)
        {
            Lose();
        }
        else
        {
            StartCoroutine(DamageCooldown());
        }

    }

    IEnumerator DamageCooldown()
    {
        isDamaged = true;
        yield return new WaitForSeconds(damageCooldown);
        isDamaged = false;
    }

    void Lose()
    {

    }
}
