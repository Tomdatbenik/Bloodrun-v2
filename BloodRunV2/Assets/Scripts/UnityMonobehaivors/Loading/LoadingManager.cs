using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private readonly SceneLogic sceneLogic = new SceneLogic();
    public GameObject playerLoadingCard;
    public List<GameObject> playerLoadingCards;
    public GameObject mainscreen;

    public List<PlayerInfo> players;

    private bool isinit = false;
    private int playercount = 0;

    // Update is called once per frame
    void Update()
    {
        updateConnectIcon();
        if(AreAllPlayersConnected())
        {
            sceneLogic.LoadScene("Game");
        }
    }

    private bool AreAllPlayersConnected()
    {
        for (int i = 0; i != playercount; i++)
        {
            if(!players[i].Connected)
            {
                return false;
            }
        }

        return true;
    }

    public void updateConnectIcon()
    {
        if(GameManager.game != null)
        {
            players = GameManager.game.GetPlayers;

            if (players != null)
            {
                Init();

                for (int i = 0; i != playercount; i++)
                {
                    setConnectIcon(i);
                }             
            }
        }
    }

    private void Init()
    {
        if (!isinit)
        {
            isinit = true;

            foreach (PlayerInfo player in players)
            {
                if (player.username != "null")
                {
                    playercount++;
                }
            }

            InstantiatePlayerLoadingCards();
        }

    }


    public void InstantiatePlayerLoadingCards()
    {
        for (int i = 0; i != playercount; i++)
        {
            CreateCard(players[i], i);
        }
    }
      

    private void CreateCard(PlayerInfo player, int index)
    {
        GameObject card = Instantiate(playerLoadingCard, mainscreen.transform);
        card.transform.position = card.transform.position + new Vector3(0, (-30 * index), 0);
        playerLoadingCards.Add(card);
        setName(index);
        setConnectIcon(index);

    }

    private void setName(int index)
    {
        playerLoadingCards[index].GetComponentInChildren<TextMeshProUGUI>().text = players[index].username;
    }

    private void setConnectIcon(int index)
    {
        if (players[index].Connected)
        {
            playerLoadingCards[index].GetComponentInChildren<Image>().color = Color.green;
        }
            else
        {
            playerLoadingCards[index].GetComponentInChildren<Image>().color = Color.red;
        }

    }

    public void setPlayerLoaded()
    {

    }
}
