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
    }
}
