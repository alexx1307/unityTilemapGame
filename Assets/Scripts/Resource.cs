using System;
using UnityEngine;
[Serializable]
public class Resources 
{
    public int money;
    public int steel;
    public int electricity;
    

    public void Update(Resources other){
        money += other.money;
        steel += other.steel;
        electricity += other.electricity;
    }
}