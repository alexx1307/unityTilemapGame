using System;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    public GameResources Resources{ get; private set; }

    private Dictionary<Vector3Int, Building> possessions;

    public string Name { get; private set; }

    public Player(string _name)
    {
        this.Name = _name;
        Resources = new GameResources();
        Resources.money = 1000;
        Resources.steel = 5;
        Resources.electricity = 5;
        possessions = new Dictionary<Vector3Int, Building>();
    
    }

    public void UpdateResources(GameResources resourceChange)
    {
      Resources.Update(resourceChange);
    }

    public void UpdateResourcesOnTurnStart()
    {
        Resources.electricity = 0;
        foreach(var building in getAllBuildingPossesions()){
          Resources.Update(building.type.turnResourceChange);
        }
    }

    private List<Building> getAllBuildingPossesions()
    {
      return MapItemsManager.Instance.getAllBuildingByPlayer(this);
    }
}
