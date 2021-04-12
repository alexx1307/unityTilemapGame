using System;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    public Resources Resources{ get; private set; }

    private Dictionary<Vector3Int, Building> possessions;

    public string Name { get; private set; }

    public Player(string _name)
    {
        this.Name = _name;
        Resources = new Resources();
        Resources.money = 1000;
        Resources.steel = 5;
        Resources.electricity = 5;
        possessions = new Dictionary<Vector3Int, Building>();
    
    }

    internal void AddPossesion(Vector3Int position, Building buidling)
    {
        possessions[position] = buidling;
    }

    public void UpdateResources(Resources resourceChange)
    {
      Resources.Update(resourceChange);
    }

    public void UpdateResourcesOnTurnStart()
    {
        Resources.electricity = 0;
        foreach(var item in possessions){
          Resources.Update(item.Value.turnResourceChange);
        }
    }
}
