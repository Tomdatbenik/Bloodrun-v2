using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject spawn;

    private bool set = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "OtherPlayer")
        {
            List<PlayerGameObjectData> players = GameManager.players;

            foreach(PlayerGameObjectData player in players)
            {
                GameObject body = GetPlayerBodyData(player.Player).body;
                PlayerDeath playerDeath = body.GetComponent<PlayerDeath>();

                playerDeath.Spawnpoint = spawn;

                if(!set)
                {
                    playerDeath.deadposition = gameObject;
                    playerDeath.died = true;
                }
            }

            set = true;
        }
    }

    private PlayerBodyData GetPlayerBodyData(GameObject gameObject)
    {
        return gameObject.GetComponent(typeof(PlayerBodyData)) as PlayerBodyData;
    }
}
