using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MessageExecutor
{
    private readonly SceneLogic sceneLogic = new SceneLogic();
    private readonly GameLogic GameLogic = new GameLogic();
    private readonly ChatLogic chatLogic = new ChatLogic();

    /// <summary>
    /// Thread where messages will be executed
    /// </summary>
    private static Thread executeThread;

    /// <summary>
    /// List of incomming Messages
    /// </summary>
    private readonly List<Message> Messages = new List<Message>();

    public void Add(Message message)
    {
        Messages.Add(message);
    }

    private void ExecuteMessages()
    {
        lock(Messages)
        {
            while (Messages.Count != 0)
            {
                Message message = Messages[0];

                if (message != null)
                {
                    switch (message.getType())
                    {
                        case MessageType.CONNECT:
                            sceneLogic.LoadScene("Loading");
                            break;
                        case MessageType.PING:

                            break;
                        case MessageType.GAME:
                            GameLogic.HandleGameMessage(message);
                            break;
                        case MessageType.CHAT:
                            chatLogic.HandeChatMessage(message);
                            break;
                    }
                }

                Messages.Remove(message);
            }
        }
    }

    public void StartExecuting()
    {
        ExecuteMessages();
    }

}
