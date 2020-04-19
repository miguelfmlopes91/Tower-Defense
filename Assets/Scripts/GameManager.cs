using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private TowerButton towerClickButton;

    public TowerButton TowerClickButton
    {
        get {
            return towerClickButton;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickTower(TowerButton tower) {
        towerClickButton = tower;
    }

    public void BuyTower() {
        towerClickButton = null;
    }
}
