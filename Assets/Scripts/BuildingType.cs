using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildingType : MonoBehaviour
{    public Sprite Sprite;

    public TileBase Tile;

    public GameResources buildingCost;
    public GameResources turnResourceChange;

    public virtual void OnFocus(Building building)
    {
        return;
    }

    public virtual void OnLoseFocus()
    {
        return;
    }
}
