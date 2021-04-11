using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurnManager : MonoBehaviour
{
    public Player[] players;
    public int currentPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        players = new Player[2];
        players[0] = new Player("gracz A");
        players[1] = new Player("gracz B");
    }

    
}
