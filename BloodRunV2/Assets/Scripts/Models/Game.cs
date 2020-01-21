using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private List<PlayerInfo> players;
    private List<TrapInfo> traps;
    private List<CheckpointInfo> checkpoints;
    private FinishInfo finish;

    public List<PlayerInfo> GetPlayers { get { return players; } }
    public List<TrapInfo> GetTraps { get { return traps; } }

    public List<CheckpointInfo> GetCheckpoints { get { return checkpoints; } }

    public FinishInfo GetFinish { get { return finish; } }

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
            game.checkpoints = GetCheckpointsFromJObject(jObject);
            game.finish = GetFinishFromJObject(jObject);

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

            if(Jplayer != null)
            {
                PlayerInfo player = PlayerInfo.FromJson(Jplayer);
                players.Add(player);
            }
        
        }

        return players;
    }

    private static FinishInfo GetFinishFromJObject(JObject jObject)
    {
        JToken token = jObject.SelectToken("finish");

        return FinishInfo.FromJson(token);
    }

    private static List<CheckpointInfo> GetCheckpointsFromJObject(JObject jObject)
    {
        IEnumerable<JToken> jCheckpoints = jObject.SelectToken("checkpoints");

        List<CheckpointInfo> checkpoints = new List<CheckpointInfo>();

        foreach (JToken item in jCheckpoints)
        {
            checkpoints.Add(CheckpointInfo.FromJson(item));
        }

        return checkpoints;
    }
}
