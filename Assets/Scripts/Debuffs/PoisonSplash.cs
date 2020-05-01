using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSplash:MonoBehaviour
{
    public int Damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.GetComponent<Monster>().TakeDamage(Damage, ELEMENT.POISON);
            Destroy(gameObject);
        }
    }
}

