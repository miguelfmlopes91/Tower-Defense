using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int Price { get; set; }
    public int Damage { get; set; }
    public float DebuffDuration { get; set; }
    public float ProcChanche { get; set; }
    public float SlowingFactor { get; set; }
    public float TickTime { get; set; }
    public int SpecialDamage { get; set; }


    public TowerUpgrade(int price, int damage, float debuffDuration, float procChance, float slowfct)
    {
        Damage = damage;
        DebuffDuration = debuffDuration;
        ProcChanche = procChance;
        SlowingFactor = slowfct;
        Price = price;
    }

    public TowerUpgrade(int price, int damage, float debuffDuration, float procChance)
    {
        Damage = damage;
        DebuffDuration = debuffDuration;
        ProcChanche = procChance;
        Price = price;
    }

    public TowerUpgrade(int price, int damage, float debuffDuration, float procChance, float tickTime, int specialDmg)
    {
        Damage = damage;
        Price = price;
        DebuffDuration = debuffDuration;
        ProcChanche = procChance;
        TickTime = tickTime;
        SpecialDamage = specialDmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
