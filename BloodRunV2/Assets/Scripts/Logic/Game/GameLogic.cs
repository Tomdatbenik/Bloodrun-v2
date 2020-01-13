using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic
{
    public void HandleGameMessage(Message message)
    {
        GameManager.game = Game.Fromjson(message.content);
    }

    public void SendFinish()
    {
        Message message = new Message();
        message.sender = ConnectionManager.CurrentUsername;
        message.type = MessageType.FINISH;

        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.username = ConnectionManager.CurrentUsername;

        message.content = playerInfo.ToJson();

        ConnectionManager.connection.TcpClient.Sender.TrySend(message);
    }

    public void SetFinish(Message message)
    {
        ChatMessage chatMessage = new ChatMessage(message.content, message.sender);
        ChatManager.ChatMessage = chatMessage;
    }
}
