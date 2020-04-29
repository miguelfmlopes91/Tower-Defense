using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Range : MonoBehaviour
{
    [SerializeField]
    private string projectileType;
    [SerializeField]
    private float projectileSpeed;

    private Animator myAnimator;
    [SerializeField]
    private int damage;

    public int Damage { get => damage; }

    private Queue<Monster> monstersQueue = new Queue<Monster>();

    private SpriteRenderer spriteRenderer;

    private bool canAttack = true;

    private float attackTimer;
    [SerializeField]
    private float attackCooldown;

    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public int Price { get; set; }
    public Monster Target { get; set; }

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Select()
    {
        myAnimator = transform.parent.GetComponent<Animator>();

        if (myAnimator == null) {
            print("MY ANIMATOR IS NULL");
        }

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

        if (Target == null && monstersQueue.Count > 0)
        {
            Target = monstersQueue.Dequeue();
        }

        if (Target != null && Target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                myAnimator.SetTrigger("Attack");
                canAttack = false;
            }
        }
        else if (monstersQueue.Count > 0)
        {
            Target = monstersQueue.Dequeue();
        }

        if (Target != null && !Target.Alive)
        {
            Target = null;
        }
    }

    private void Shoot() {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Initialize(this);
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
            Target = null;
        }
    }
}
