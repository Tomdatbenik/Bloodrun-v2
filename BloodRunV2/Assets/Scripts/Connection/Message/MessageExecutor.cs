using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MessageExecutor
{

    /// <summary>
    /// Thread where messages will be executed
    /// </summary>
    private static Thread executeThread;

    /// <summary>
    /// List of incomming Messages
    /// </summary>
    public readonly static List<Message> Messages = new List<Message>();

    public void StartExecutingMessages()
    {
        executeThread = new Thread(new ThreadStart(ExecuteMessages))
        {
            IsBackground = true
        };
    }

    private void ExecuteMessages()
    {
        foreach(Message message in Messages)
        {
            switch(message.getType())
            {
                case MessageType.CONNECT:

                    break;
                case MessageType.PING:

                    break;
                case MessageType.GAME:

                    break;
                case MessageType.MOVE:

                    break;
            }
        }

        ExecuteMessages();
    }


}
