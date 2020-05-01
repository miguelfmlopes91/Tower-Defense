using System.Collections;
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
}
