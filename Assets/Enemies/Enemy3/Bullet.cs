using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerCombat player;
    [SerializeField] float attack;

    private void Start()
    {

        player = PlayerController.instance.GetComponent<PlayerCombat>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(attack);
        }
    }


}
