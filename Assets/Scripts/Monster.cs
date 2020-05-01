using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public float MaxSpeed { get; set; }

    private Stack<Node> path;

    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> newDebuffs = new List<Debuff>();


    public Point GridPosition { get; set; }

    private Vector3 destination;//next tile

    public bool IsActive;

    private int invulnerability = 2;

    public bool Alive
    {
        get =>  healthStat.CurrentValue > 0;
    }
    public ELEMENT ElementType { get => elementType; set => elementType = value; }
    public float Speed { get => speed; set => speed = value; }

    private Animator myAnimator;

    [SerializeField]
    private Stat healthStat;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ELEMENT elementType;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        healthStat.Initialize();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MaxSpeed = speed;
    }

    private void Update()
    {
        HandleDebuffs();
        Move();
    }

    public void Spawn(int health)
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;
        healthStat.Bar.Reset();

        healthStat.MaxValue = health;
        healthStat.CurrentValue = healthStat.MaxValue;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f), new Vector3(1f, 1f), false));

        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        float progress = 0;
        while (progress < 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;

        if (remove)
        {
            Release();
        }
    }

    private void Move()
    {
        if (IsActive)
        {
            //destination is just next tile
            transform.position = Vector2.MoveTowards(transform.position, destination, Speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    Animate(GridPosition, path.Peek().GridPosition);
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }

    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;

            GridPosition = path.Peek().GridPosition;
            Animate(GridPosition, path.Peek().GridPosition);
            destination = path.Pop().WorldPosition;
        }
    }

    public void Animate(Point currentPos, Point newPos)
    {
        if (currentPos.Y > newPos.Y)
        {
            //Moving Down
            myAnimator.SetInteger("Horizontal", 0);
            myAnimator.SetInteger("Vertical", 1);
        }
        else if (currentPos.Y < newPos.Y)
        {
            //Moving Up
            myAnimator.SetInteger("Horizontal", 0);
            myAnimator.SetInteger("Vertical", -1);
        }
        if (currentPos.Y == newPos.Y)
        {
            if (currentPos.X > newPos.X)
            {
                //Move to the left
                myAnimator.SetInteger("Horizontal", -1);
                myAnimator.SetInteger("Vertical", 0);
            }
            else if (currentPos.X < newPos.X)
            {
                myAnimator.SetInteger("Horizontal", 1);
                myAnimator.SetInteger("Vertical", 0);
                //Moving to the right
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RedPortal")
        {
            StartCoroutine(Scale(new Vector3(1,1), new Vector3(0.1f,0.1f), true));
            collision.GetComponent<Portal>().Open();
            GameManager.Instance.Lives--;
        }
        if (collision.tag == "Itle")
        {
            spriteRenderer.sortingOrder = collision.GetComponent<Tile>().GridPosition.Y;
        }
    }

    public void Release()
    {
        IsActive = false;
        GridPosition = LevelManager.Instance.BlueSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage(float damage, ELEMENT damageSrc)
    {
        if (IsActive)
        {
            if (damageSrc == ElementType)
            {
                damage /= invulnerability;
                invulnerability++;
            }

            healthStat.CurrentValue -= damage;
            if (healthStat.CurrentValue <= 0)
            {
                GameManager.Instance.Currency += 2;

                myAnimator.SetTrigger("Die");

                IsActive = false;

                GetComponent<SpriteRenderer>().sortingOrder--;
            }
        }
    }

    public void AddDebuff(Debuff debuff) {

        if (!debuffs.Exists(x=> x.GetType() == debuff.GetType()))
        {
            newDebuffs.Add(debuff);
        }
    }

    private void HandleDebuffs()
    {
        if (newDebuffs.Count > 0)
        {
            debuffs.AddRange(newDebuffs);
            newDebuffs.Clear();
        }
        foreach (Debuff item in debuffsToRemove)
        {
            debuffs.Remove(item);
        }
        foreach (Debuff item in debuffs)
        {
            item.Update();
        }
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
    }
}
