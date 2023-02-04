using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(this, .2f);
    }

}
