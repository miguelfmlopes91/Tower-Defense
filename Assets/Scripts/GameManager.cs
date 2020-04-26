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

    private int wave = 0;
    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private GameObject waveBtn;

    private List<Monster> activeMonsters = new List<Monster>();


    public bool waveActive {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

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

        if (currency>=tower.Price && !waveActive)
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
        wave++;
        waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);

        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        //design logic for waves
        for (int w = 0; w < wave; w++)
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

            Monster monster = Pool.GetObject(type).GetComponent<Monster>();

            monster.Spawn();

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!waveActive)
        {
            waveBtn.SetActive(true);
        }
    }
}
