using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MessageExecutor
{
    private SceneLogic sceneLogic = new SceneLogic();

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
        List<Message> deletedMessages = new List<Message>();

        foreach(Message message in Messages)
        {
            if (message != null)
            {
                switch (message.getType())
                {
                    case MessageType.CONNECT:
                        deletedMessages.Add(message);
                        sceneLogic.LoadScene("Loading");
                        break;
                    case MessageType.PING:

                        break;
                    case MessageType.GAME:

                        break;
                    case MessageType.MOVE:

                        break;
                }
            }
        }

        foreach (Message message in deletedMessages)
        {
            Messages.Remove(message);
        }

    }

    public void StartExecuting()
    {
        ExecuteMessages();
    }

}
