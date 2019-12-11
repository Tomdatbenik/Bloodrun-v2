using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{

    public string Username;
    public string IP;

    private Connection connection;

    public static readonly MessageExecutor Executor = new MessageExecutor();

    public GameObject ReconnectButton;

    private void Start()
    {
        ReconnectButton.SetActive(false);

        connection = new Connection(IP);

        Connect();
    }

    public void Reconnect()
    {
        Connect();
    }

    private void Connect()
    {
        if (connection.TryConnect())
        {
            ReconnectButton.SetActive(false);
            SendInitMessageToServer();
        }
        else
        {
            ReconnectButton.SetActive(true);
        }
    }

    private void SendInitMessageToServer()
    {
        Message message = new Message(Username, connection.UdpClient.Ip + ":" + connection.UdpClient.Port.ToString(), MessageType.CONNECT);

        if(!connection.TcpClient.Sender.TrySend(message))
        {
            ReconnectButton.SetActive(true);
        }
    }

    void OnApplicationQuit()
    {
        Message message = new Message(Username, "none", MessageType.DISCONNECT);

        connection.TcpClient.Sender.TrySend(message);

        connection.TcpClient.Close();        
    }

    private void Update()
    {
        Executor.StartExecuting();
    }


    #region DoNotDestroy
    /// <summary>Static reference to the instance of our DataManager</summary>
    public static ConnectionManager instance;

    /// <summary>Awake is called when the script instance is being loaded.</summary>
    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }
    #endregion
}
