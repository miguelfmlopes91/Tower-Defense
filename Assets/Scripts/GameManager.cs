using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region properties
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
    public ObjectPool Pool { get; set; }

    #endregion

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int monsterIndex = Random.Range(0, 4);

        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "BlueMonster";
                break;
            case 1:
                type = "RedMonster";
                break;
            case 2:
                type = "GreenMonster";
                break;
            case 3:
                type = "PurpleMonster";
                break;
        }

        Pool.GetObject(type);//.GetComponent<Monster>();
        yield return new WaitForSeconds(2.5f);
    }
}
