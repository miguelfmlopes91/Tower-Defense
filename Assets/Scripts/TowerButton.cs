﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    [SerializeField]
    private GameObject towerButtonPrefab;
    [SerializeField]
    private Sprite towerSprite;
    [SerializeField]
    private int price;
    [SerializeField]
    private Text priceText;


    public GameObject TowerButtonPrefab { get => towerButtonPrefab;}
    public Sprite TowerSprite { get => towerSprite; set => towerSprite = value; }
    public int Price { get => price; set => price = value; }

    private void Start()
    {
        priceText.text = Price + "$";

        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
    }

    private void PriceCheck()
    {
        if (price <= GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.white;
            priceText.color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.grey;
            priceText.color = Color.grey;
        }
    }

    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;
        switch (type)
        {
            case "Fire":
                FireTower fire = towerButtonPrefab.GetComponentInChildren<FireTower>();
                tooltip = string.Format("<color=#ffa500ff><size=20><b>Fire</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec \nTick time: {3} sec \nTick damage: {4}\nCan apply a DOT to the target", fire.Damage, fire.Proc, fire.DebuffDuration, fire.TickTime, fire.TickDamage);
                break;
            case "Frozen":
                IceTower frost = towerButtonPrefab.GetComponentInChildren<IceTower>();
                tooltip = string.Format("<color=#00ffffff><size=20><b>Frost</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec\nSlowing factor: {3}%\nHas a chance to slow down the target", frost.Damage, frost.Proc, frost.DebuffDuration, frost.SlowingFactor);
                break;
            case "Poison":
                PoisonTower poison = towerButtonPrefab.GetComponentInChildren<PoisonTower>();
                tooltip = string.Format("<color=#00ff00ff><size=20><b>Poison</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec \nTick time: {3} sec \nSplash damage: {4}\nCan apply dripping poison", poison.Damage, poison.Proc, poison.DebuffDuration, poison.TickTime, poison.SplashDamage);
                break;
            case "Storm":
                StormTower storm = towerButtonPrefab.GetComponentInChildren<StormTower>();
                tooltip = string.Format("<color=#add8e6ff><size=20><b>Storm</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec\n Has a chance to stunn the target", storm.Damage, storm.Proc, storm.DebuffDuration);
                break;

            default:
                break;
        }

        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
