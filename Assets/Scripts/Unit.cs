using System;
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, FocusHolder{
    public UnitType type;
    public Vector3Int position;
    public Player owner;
    private Grid grid;
    private IEnumerator moveCoroutine;

    void Start(){
        grid = GameObject.FindObjectOfType<Grid>();
    }
    public FocusHolder DoActionOnHoverAndRetrieveNextFocus(Vector3 mousePosition)
    {
        return this;
    }

    public FocusHolder DoActionOnClickAndRetrieveNextFocus(Vector3 mousePosition)
    {
       return null;
    }


    public void LoseFocus()
    {
        return;
    }

    public void GetFocus()
    {
        Debug.Log("jestem");
        return;
    }
}