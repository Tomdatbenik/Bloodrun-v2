using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{
    public void MovePlayerOnServer(PlayerInfo player)
    {
        Message message = new Message(player.username, player.ToJson(), MessageType.MOVE);

        ConnectionManager.connection.UdpClient.SendUdpMessage(message);
    }

    public void PlayerPushing(PlayerInfo player)
    {
        Message message = new Message(player.username, player.ToJson(), MessageType.PUSHING);
        ConnectionManager.connection.UdpClient.SendUdpMessage(message);
    }
}
