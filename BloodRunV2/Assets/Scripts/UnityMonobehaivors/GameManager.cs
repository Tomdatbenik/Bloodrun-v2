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

    private void SpawnPlayers()
    {
        players = new List<PlayerGameObjectData>();

        foreach (PlayerInfo player in game.GetPlayers)
        {
            PlayerGameObjectData playerdata = new PlayerGameObjectData();

            if (player.username != "null")
            {
                playerdata.PlayerInfo.username = player.username;
                playerdata.Player = Instantiate(playerPrefab);
                PlayerBodyData playerbodydata = playerdata.Player.GetComponent(typeof(PlayerBodyData)) as PlayerBodyData;

                if (player.username == connectionManager.Username)
                {
                    cam.Follow = playerdata.Player.transform;
                    cam.LookAt = playerdata.Player.transform;
                    playerdata.Player.tag = "Player";
                }
                else
                {
                    PlayerMovement playerMovement = playerbodydata.body.GetComponent(typeof(PlayerMovement)) as PlayerMovement;

                    Destroy(playerMovement);

                    playerdata.Player.tag = "OtherPlayer";
                }

                playerdata.Player.transform.position = new Vector3(float.Parse(player.transform.location.x), float.Parse(player.transform.location.y), float.Parse(player.transform.location.z));
                playerdata.Player.transform.rotation = new Quaternion(float.Parse(player.transform.rotation.x), float.Parse(player.transform.rotation.y), float.Parse(player.transform.rotation.z), float.Parse(player.transform.rotation.w));

                PlayerDeath playerDeath = playerbodydata.body.GetComponent(typeof(PlayerDeath)) as PlayerDeath;
                playerDeath.Spawnpoint = Spawnpoint;

                players.Add(playerdata);
            }
        }
    }
}
