using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Player[] players = {new Player("gracz A"), new Player("gracz B")};
    public int currentPlayerIndex = 0;
    public Text notifications;
    public PlayerStatsPanel currentPlayerStatsPanel;

    private FocusHolder currentFocus;
    public Grid grid;

    public Player CurrentPlayer { 
        get{
            return players[currentPlayerIndex];
        }
    }

    void Update(){
        if(Input.GetMouseButtonDown(2)){
            ChangeFocus(null);
        }
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ){
            if(currentFocus != null){
                FocusHolder newFocus = currentFocus.DoActionOnClickAndRetrieveNextFocus(Input.mousePosition);
                if(newFocus != currentFocus){
                    ChangeFocus(newFocus);
                }
            } else {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPoint.z = 0;
                Vector3Int position = grid.WorldToCell(worldPoint);
                Unit unit = MapItemsManager.Instance.GetUnitAt(position);
                if(unit != null){
                    ChangeFocus(unit);
                } else {
                    Building building = MapItemsManager.Instance.GetBuildingAt(position);
                    if(building != null){
                        ChangeFocus(building);
                    } 
                }
            }
        } else {
            if(currentFocus != null){
                currentFocus.DoActionOnHoverAndRetrieveNextFocus(Input.mousePosition);
            } 
        }
        
        
    }

    private void ChangeFocus(FocusHolder newFocus)
    {
        if(currentFocus != null){
            currentFocus.LoseFocus();
        }
        currentFocus = newFocus;
        if(currentFocus != null){
            currentFocus.GetFocus();
        }
    }



    public void EndTurn(){
        currentPlayerIndex = (currentPlayerIndex + 1 )%players.Length;
        CurrentPlayer.UpdateResourcesOnTurnStart();
        
        StopCoroutine("DisplayCurrentPlayer");
        StartCoroutine("DisplayCurrentPlayer");
        currentFocus = null;
    }


    public void Build(){
        ChangeFocus(GameObject.FindObjectOfType<BuilderManager>());
    }
    private IEnumerator DisplayCurrentPlayer(){
        notifications.gameObject.SetActive(true);
        notifications.text = "Turn of "+CurrentPlayer.Name;
        notifications.transform.position = new Vector3(50, 500, 0);
        int count = 300;
        while(count > 0){
            notifications.transform.position += new Vector3(2,0,0);
            count--;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        notifications.gameObject.SetActive(false);
    }
}
