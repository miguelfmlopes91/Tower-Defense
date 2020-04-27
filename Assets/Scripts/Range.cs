using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private Monster target;

    private Queue<Monster> monstersQueue = new Queue<Monster>();

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Select()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void Attack()
    {
        if (target == null && monstersQueue.Count > 0)
        {
            target = monstersQueue.Dequeue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            
            monstersQueue.Enqueue(collision.GetComponent<Monster>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            target = null;
        }
    }
}
