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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();   
    }

    public void PickTower(TowerButton tower) {
        TowerClickButton = tower;
        Hover.Instance.Activate(tower.TowerSprite);
    }

    public void BuyTower() {
        Hover.Instance.Deactivate();
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}
