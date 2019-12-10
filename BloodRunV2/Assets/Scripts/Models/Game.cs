using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : ScriptableObject
{
    private List<PlayerInfo> players;
    private List<TrapInfo> traps;

    public List<PlayerInfo> GetPlayers { get { return players; } }
    public List<TrapInfo> GetTraps { get { return traps; } }

    public Game()
    {
        players = new List<PlayerInfo>();
        traps = new List<TrapInfo>();
    }

    public static Game Fromjson(string message)
    {
        try
        {
            Game game = new Game();

            JObject jObject = JObject.Parse(message);

            game.players = GetPlayersFromJObject(jObject);

            game.traps = GetTrapsFromJObject(jObject);

            return game;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return null;
        }
    }

    private static List<TrapInfo> GetTrapsFromJObject(JObject jObject)
    {
        IEnumerable<JToken> jTraps = jObject.SelectToken("traps");

        List<TrapInfo> traps = new List<TrapInfo>();

        foreach (JToken item in jTraps)
        {
            traps.Add(TrapInfo.FromJson(item));
        }

        return traps;
    }

    private static List<PlayerInfo> GetPlayersFromJObject(JObject jObject)
    {
        JToken Jplayers = jObject.SelectToken("players");

        List<PlayerInfo> players = new List<PlayerInfo>();

        for (int i = 1; i < 5; i++)
        {
            JToken Jplayer = Jplayers.SelectToken("player_" + i);
            PlayerInfo player = PlayerInfo.FromJson(Jplayer);
            players.Add(player);
        }

        return players;
    }
}
