﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer rangeSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }


    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }


    public void Activate(Sprite sprite) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
        rangeSpriteRenderer.enabled = true;

    }

    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        rangeSpriteRenderer.enabled = false;
        GameManager.Instance.TowerClickButton = null;
    }
}
