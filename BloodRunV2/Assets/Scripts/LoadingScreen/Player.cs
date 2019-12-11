using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string username;
    public bool connected;


    public Player(string u, bool con)
    {
        this.username = u;
        this.connected = con;
    }
}
