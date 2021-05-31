using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UnitCreatingBuilding : BuildingType
{
    public List<UnitType> unitsToBuy;
    
    private Transform detailsPanel; 
    private GameObject buildButtonPrefab;
    private Grid grid;

    void Start(){
        detailsPanel = GameObject.Find("DetailsPanel").transform;
        buildButtonPrefab = (GameObject)Resources.Load("Prefabs/buildButton", typeof(GameObject));
        grid = GameObject.FindObjectOfType<Grid>();
    }
    public override void OnFocus(Building building)
    {
        foreach (UnitType unit in unitsToBuy)
        {
            GameObject unitBuyButton = Instantiate(buildButtonPrefab);
            unitBuyButton.transform.SetParent(transform);
            unitBuyButton.GetComponent<Image>().sprite = unit.Sprite;
            unitBuyButton.GetComponent<Button>().onClick.AddListener(() => BuyUnit(unit, building.position, building.owner));
            unitBuyButton.transform.SetParent(detailsPanel);
        }
         
    }

    public override void OnLoseFocus()
    {
        foreach (Transform child in detailsPanel) {
            GameObject.Destroy(child.gameObject);
        }
    }


    private void BuyUnit(UnitType unitType, Vector3Int position, Player owner)
    {
        GameObject go = new GameObject("unit");
        Unit unit = go.AddComponent<Unit>();
        unit.type = unitType;
        unit.position = position;
        unit.owner = owner;
        go.transform.position =grid.CellToWorld(position);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = unitType.Sprite;
        sr.sortingOrder = 3;

        MapItemsManager.Instance.AddUnit(unit);
    }
}
