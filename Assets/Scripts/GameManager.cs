using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private int currency;



    [SerializeField]
    private Text currencyText;


    public TowerButton TowerClickButton
    {
        get;set;
    }
    public int Currency { get => currency;
        set {
            currency = value;
            currencyText.text = value.ToString() + "<color=lime>$</color>";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();   
    }

    public void PickTower(TowerButton tower) {

        if (currency>=tower.Price)
        {
            TowerClickButton = tower;
            Hover.Instance.Activate(tower.TowerSprite);
        }

    }

    public void BuyTower() {
        if (Currency >= TowerClickButton.Price)
        {
            Currency -= TowerClickButton.Price;
            Hover.Instance.Deactivate();
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}
