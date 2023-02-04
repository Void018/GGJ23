using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    [Header("Setup")]
    public Collider2D attackSensor;
    private ContactFilter2D filter = new ContactFilter2D().NoFilter();
    private PlayerAnimation playerAnimation;


    [Space]

    [Header("Attack Parameters")]
    public int attackDamage;

    void Start() {
        playerAnimation = GetComponent<PlayerAnimation>();
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
    }

    void Attack() {
        playerAnimation.SwingSword();

        var colliders = new List<Collider2D>();
        attackSensor.OverlapCollider(filter, colliders); // output to colliders list

        colliders
            .Where(c => (c.CompareTag("AhmedEnemy")))
            .ToList()
            .ForEach(e => e.GetComponent<AhmedEnemy>().Hit(attackDamage));

    }
}
