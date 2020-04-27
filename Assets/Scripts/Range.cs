using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField]
    private string projectileType;

    private Monster target;

    private Queue<Monster> monstersQueue = new Queue<Monster>();

    private SpriteRenderer spriteRenderer;

    private bool canAttack;

    private float attackTimer;
    [SerializeField]
    private float attackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
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
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if (target == null && monstersQueue.Count > 0)
        {
            target = monstersQueue.Dequeue();
        }

        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                canAttack = false;
            }
        }
    }

    private void Shoot() {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
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
