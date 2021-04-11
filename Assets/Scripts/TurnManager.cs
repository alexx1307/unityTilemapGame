using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    private string name;
    
}
public class TurnManager : MonoBehaviour
{
    public Player[] players;
    public int currentPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        players = new Player[2];
        players[0] = new Player('gracz A');
        players[1] = new Player('gracz B');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
