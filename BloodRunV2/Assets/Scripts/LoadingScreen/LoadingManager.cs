using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject playerLoadingCard;
    public List<GameObject> playerLoadingCards;
    public GameObject mainscreen;

    public List<Player> players;

    // Start is called before the first frame update
    void Start()
    {
        players.Add(new Player("Tom", false));
        players.Add(new Player("Wiebe", true));
        players.Add(new Player("Joost", true));
        players.Add(new Player("Nicky", false));

        InstantiatePlayerLoadingCards();
    }

    // Update is called once per frame
    void Update()
    {
        updateConnectIcon();
    }

    public void updateConnectIcon()
    {
        for (int i = 0; i < players.Count; i++)
        {
            setConnectIcon(i);
        }
    }

    public void InstantiatePlayerLoadingCards()
    {
        for (int i = 0; i < players.Count; i++)
        {
            CreateCard(players[i], i);
        }     
    }
      

    private void CreateCard(Player player, int index)
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
        if (players[index].connected)
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
