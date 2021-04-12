using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    public Text playerName;
    public Text money;
    public Text steel;
    public Text electricity;

    public void Update(){
        Player currentPlayer = FindObjectOfType<TurnManager>().CurrentPlayer;
        if(currentPlayer != null){
            playerName.text = currentPlayer.Name;
            money.text = currentPlayer.Resources.money.ToString();
            steel.text = currentPlayer.Resources.steel.ToString();
            electricity.text = currentPlayer.Resources.electricity.ToString();
        }
    }


}