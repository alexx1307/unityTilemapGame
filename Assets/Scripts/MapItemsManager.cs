using System;
using System.Collections.Generic;
using UnityEngine;

public class MapItemsManager : MonoBehaviour
{
    private static MapItemsManager _instance;

    public static MapItemsManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MapItemsManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private List<Building> buildings = new List<Building>();
    private List<Unit> units = new List<Unit>();

    public List<Building> getAllBuildingByPlayer(Player player)
    {
        return buildings.FindAll(building => building.owner == player);
    }

    public List<Unit> getAllUnitsByPlayer(Player player)
    {
        return units.FindAll(unit => unit.owner == player);
    }

    internal void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    internal Unit GetUnitAt(Vector3Int position)
    {
        return units.Find(unit => unit.position.Equals(position));
    }    internal Building GetBuildingAt(Vector3Int position)
    {
        return buildings.Find(building => building.position.Equals(position));
    }

    internal void AddUnit(Unit unit)
    {
        units.Add(unit);
    }
}