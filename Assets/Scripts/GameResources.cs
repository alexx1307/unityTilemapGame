using System;
using UnityEngine;
[Serializable]
public class GameResources 
{
    public int money;
    public int steel;
    public int electricity;
    

    public void Update(GameResources other){
        money += other.money;
        steel += other.steel;
        electricity += other.electricity;
    }
}