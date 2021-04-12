using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Player[] players;
    public int currentPlayerIndex;

    public PlayerStatsPanel currentPlayerStatsPanel;

    public Player CurrentPlayer { 
        get{
            return players[currentPlayerIndex];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new Player[2];
        players[0] = new Player("gracz A");
        players[1] = new Player("gracz B");
    }

    public void EndTurn(){
        currentPlayerIndex = (currentPlayerIndex + 1 )%players.Length;
        CurrentPlayer.UpdateResourcesOnTurnStart();
    }
}
