using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatLogic
{
    public void HandleChatMessage(Message message)
    {
        ChatMessage chatMessage = new ChatMessage(message.content, message.sender);
        ChatManager.ChatMessage = chatMessage;
    }
}
