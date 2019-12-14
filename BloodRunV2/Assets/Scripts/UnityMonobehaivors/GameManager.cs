using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ConnectionManager connectionManager;

    public static Game game;

    public GameObject Spawnpoint;

    public GameObject playerPrefab;

    private static List<PlayerGameObjectData> players;
    private static List<TrapGameObjectData> Traps;

    public CinemachineVirtualCamera cam;

    public GameObject AlwaysActiveTrap;
    public GameObject RotateTrap;
    public GameObject RotatingDarter;
    public GameObject Darter;
    public GameObject Spiketrap;


    private static bool isInit = true;


    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        if(isInit)
        {
            isInit = false;
            GameObject bloodrun = GameObject.Find("Bloodrun");
            connectionManager = bloodrun.GetComponent(typeof(ConnectionManager)) as ConnectionManager;

            if(connectionManager == null)
            {
                isInit = true;
            }
            else
            {
                SpawnPlayers();
                SpawnTraps();
            }

        }
   
    }

    #region Spawns

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

    private void SpawnTraps()
    {
        Traps = new List<TrapGameObjectData>();

        foreach (TrapInfo trap in game.GetTraps)
        {
            TrapGameObjectData trapData = new TrapGameObjectData();
            trapData.TrapInfo = trap;

            switch (trap.type)
            {
                //case TrapType.AlwaysActiveTrap:
                //    trapData.Trap = AlwaysActiveTrap;
                //    break;
                case TrapType.RotateTrap:
                    trapData.Trap = RotateTrap;
                    break;
                //case TrapType.RotatingDarter:
                //    trapData.Trap = RotatingDarter;
                //    break;
                //case TrapType.Darter:
                //    trapData.Trap = Darter;
                //    break;
                case TrapType.SpikeTrap:
                    trapData.Trap = Spiketrap;
                    break;
                default:
                    trapData.Trap = Spiketrap;
                    break;
            }

            trapData.Trap = Instantiate(trapData.Trap);

            if (trap.type == TrapType.AlwaysActiveTrap || trap.type == TrapType.SpikeTrap)
            {
                trapData.Trap.transform.localScale = new Vector3(float.Parse(trap.scale.x), float.Parse(trap.scale.y), float.Parse(trap.scale.z));
            }

            trapData.Trap.transform.position = new Vector3(float.Parse(trap.transform.location.x), float.Parse(trap.transform.location.y), float.Parse(trap.transform.location.z));
            trapData.Trap.transform.rotation = new Quaternion(float.Parse(trap.transform.rotation.x), float.Parse(trap.transform.rotation.y), float.Parse(trap.transform.rotation.z), float.Parse(trap.transform.rotation.w));

            Traps.Add(trapData);
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
                    Playerdata.Player.transform.position = location;
                    rb.rotation = new Quaternion(float.Parse(player.transform.rotation.x), float.Parse(player.transform.rotation.y), float.Parse(player.transform.rotation.z), float.Parse(player.transform.rotation.w));
                }
            }
        }
    }

    private void ActivateTraps()
    {
        foreach (TrapGameObjectData trap in Traps)
        {
            TrapInfo trapInfo = game.GetTraps.Find(x => trap.TrapInfo.id == x.id);

            trap.TrapInfo = trapInfo;

            switch (trap.TrapInfo.type)
            {
                case TrapType.RotateTrap:
                    trap.Trap.transform.rotation = transform.rotation = Quaternion.AngleAxis(float.Parse(trapInfo.transform.rotation.y), Vector3.up);
                    break;
                //case TrapType.RotatingDarter:
                //    gameObject = RotatingDarter;
                //    break;
                case TrapType.Darter:
                    //ShootDart shootDart = trapInfo.trap.GetComponent(typeof(ShootDart)) as ShootDart;
                    //if (trapInfo.activated)
                    //{
                    //    shootDart.Shoot();
                    //}
                    break;
                case TrapType.SpikeTrap:
                    SpikeTrap spiketrap = trap.Trap.GetComponent(typeof(SpikeTrap)) as SpikeTrap;
                    if (trapInfo.activated)
                    {
                        spiketrap.Activate();
                    }
                    else
                    {
                        spiketrap.DeActivate();
                    }
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if(connectionManager != null)
        {
            MovePlayers();
            ActivateTraps();
        }
    }
}
