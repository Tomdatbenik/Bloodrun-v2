using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : ScriptableObject
{
    public string username;
    public TransformInfo transform;
    public bool Connected = false;

    public PlayerInfo()
    {
        username = "";
        transform = new TransformInfo();
    }

    public static PlayerInfo FromJson(Newtonsoft.Json.Linq.JToken jToken)
    {
        PlayerInfo player = new PlayerInfo();

        player.username = (string)jToken.SelectToken("username");
        player.transform = TransformInfo.FromJson(jToken.SelectToken("transform"));
        player.Connected = (bool)jToken.SelectToken("connected");

        return player;
    }

    public string ToJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
