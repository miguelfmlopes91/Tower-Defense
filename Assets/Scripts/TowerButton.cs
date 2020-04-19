using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{

    [SerializeField]
    private GameObject towerButtonPrefab;
    [SerializeField]
    private Sprite towerSprite;


    public GameObject TowerButtonPrefab { get => towerButtonPrefab;}
    public Sprite TowerSprite { get => towerSprite; set => towerSprite = value; }
}
