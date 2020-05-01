﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Monster target;
    private Range parent;
    private Animator myAnimator;
    private ELEMENT elementType;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void Initialize(Range parent)
    {
        this.parent = parent;
        target = parent.Target;
        elementType = parent.ElementType;
    }

    private void MoveToTarget()
    {
        if (target!=null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);
            Vector2 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if(!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            if (target.gameObject == collision.gameObject)
            {
                target.TakeDamage(parent.Damage, elementType);

                myAnimator.SetTrigger("Impact");

                ApplyDebuffs();

                //GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }

    private void ApplyDebuffs() {

        if (target.ElementType != elementType)
        {
            float roll = Random.Range(0, 100);
            if (roll <= parent.Proc)
            {
                target.AddDebuff(parent.GetDebuff());
            }
        }

        target.AddDebuff(parent.GetDebuff());
    }
}
