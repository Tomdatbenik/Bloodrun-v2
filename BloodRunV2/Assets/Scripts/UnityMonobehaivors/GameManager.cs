using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ConnectionManager connectionManager;

    public static Game game;

    public GameObject Spawnpoint;

    public GameObject playerPrefab;

    public List<PlayerGameObjectData> players;
    public List<GameObject> traps;

    public CinemachineVirtualCamera cam;


    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        GameObject bloodrun = GameObject.Find("Bloodrun");
        connectionManager = bloodrun.GetComponent(typeof(ConnectionManager)) as ConnectionManager;
        SpawnPlayers();
    }

    #region SpawnPlayers

    private void SpawnPlayers()
    {
        players = new List<PlayerGameObjectData>();

        foreach (PlayerInfo player in game.GetPlayers)
        {
            PlayerGameObjectData playerdata = new PlayerGameObjectData();

            if (PlayerIsNotNull(player))
            {
                playerdata.PlayerInfo.username = player.username;
                playerdata.Player = Instantiate(playerPrefab);

                PlayerBodyData playerbodydata = GetPlayerBodyData(playerdata.Player);

                if (player.username == connectionManager.Username)
                {
                    cam.Follow = playerdata.Player.transform;
                    cam.LookAt = playerdata.Player.transform;
                    playerdata.Player.tag = "Player";
                }
                else
                {
                    RemovePlayerMovement(playerbodydata.body);

                    playerdata.Player.tag = "OtherPlayer";
                }

                SetTransformFromTransformInfo(playerdata.Player, player.transform);
                
                PlayerDeath playerDeath = GetPlayerdeath(playerbodydata.body);

                playerDeath.Spawnpoint = Spawnpoint;

                players.Add(playerdata);
            }
        }
    }

    private PlayerBodyData GetPlayerBodyData(GameObject gameObject)
    {
        return gameObject.GetComponent(typeof(PlayerBodyData)) as PlayerBodyData;
    }

    private PlayerDeath GetPlayerdeath(GameObject gameObject)
    {
        return gameObject.GetComponent(typeof(PlayerDeath)) as PlayerDeath;
    }

    private void RemovePlayerMovement(GameObject gameObject)
    {
        PlayerMovement playerMovement = gameObject.GetComponent(typeof(PlayerMovement)) as PlayerMovement;

        Destroy(playerMovement);
    }

    private bool PlayerIsNotNull(PlayerInfo player)
    {
        if(player.username != "null")
        {
            return true;
        }

        return false;
    }

    private bool IsCurrentPlayer(PlayerInfo player)
    {
        if(connectionManager.Username == player.username)
        {
            return true;
        }
        return false;
    }

    private void SetTransformFromTransformInfo(GameObject gameObject, TransformInfo transform)
    {
        gameObject.transform.position = new Vector3(
            float.Parse(transform.location.x), 
            float.Parse(transform.location.y), 
            float.Parse(transform.location.z));

        gameObject.transform.rotation = new Quaternion(
            float.Parse(transform.rotation.x), 
            float.Parse(transform.rotation.y), 
            float.Parse(transform.rotation.z), 
            float.Parse(transform.rotation.w));
    }

    #endregion

    private void MovePlayers()
    {
        foreach (PlayerInfo player in game.GetPlayers)
        {
            foreach (PlayerGameObjectData Playerdata in players)
            {

                if (Playerdata.PlayerInfo.username == player.username && !IsCurrentPlayer(Playerdata.PlayerInfo))
                {
                    float x = float.Parse(player.transform.location.x);
                    float y = float.Parse(player.transform.location.y);
                    float z = float.Parse(player.transform.location.z);

                    Rigidbody rb = Playerdata.Player.GetComponent(typeof(Rigidbody)) as Rigidbody;

                    Vector3 location = new Vector3(x, y, z);

                    rb.MovePosition(location);
                    gameObject.transform.position = location;
                    rb.rotation = new Quaternion(float.Parse(player.transform.rotation.x), float.Parse(player.transform.rotation.y), float.Parse(player.transform.rotation.z), float.Parse(player.transform.rotation.w));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayers();
    }
}
