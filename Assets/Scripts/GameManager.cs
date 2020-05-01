using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region properties
    private int currency;
    [SerializeField]
    private Text currencyText;
    public TowerButton TowerClickButton
    {
        get; set;
    }
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value.ToString() + "<color=lime>$</color>";
        }
    }
    public object MyProperty { get; set; }
    public ObjectPool Pool { get; set; }

    private int wave = 0;
    private int lives = 0;
    [SerializeField]
    private Text waveTxt;
    [SerializeField]
    private Text livesTxt;
    [SerializeField]
    private GameObject waveBtn;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private Text priceText;

    private int health = 15;

    private Range selectedTower;

    private List<Monster> activeMonsters = new List<Monster>();

    private bool gameOver = false;

    public bool waveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            livesTxt.text = value.ToString();
            if (lives <1)
            {
                lives = 0;
                GameOver();
            }
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
        Lives = 10;
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton tower)
    {

        if (currency >= tower.Price && !waveActive)
        {
            TowerClickButton = tower;
            Hover.Instance.Activate(TowerClickButton.TowerSprite);
        }

    }

    public void BuyTower()
    {
        if (Currency >= TowerClickButton.Price)
        {
            Currency -= TowerClickButton.Price;
            Hover.Instance.Deactivate();
        }
    }

    public void SelectTower(Range tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();

        priceText.text = "+ " + (selectedTower.Price/2).ToString();

        upgradePanel.SetActive(true);
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        upgradePanel.SetActive(false);
        selectedTower = null;

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

            monster.Spawn(health);

            if (wave%3 == 0)
            {
                health += 5;
            }

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!waveActive && !gameOver)
        {
            waveBtn.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SellTower()
    {
        if (selectedTower != null)
        {
            Currency += selectedTower.Price / 2;

            selectedTower.GetComponentInParent<Tile>().IsEmpty = true;

            Destroy(selectedTower.transform.parent.gameObject);

            DeselectTower();
        }
    }
}