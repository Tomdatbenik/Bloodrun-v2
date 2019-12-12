using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    private TCPClient tcpClient;
    private UDPClient udpClient;

    public TCPClient TcpClient { get => tcpClient; set => tcpClient = value; }
    public UDPClient UdpClient { get => udpClient; set => udpClient = value; }

    public Connection(string Server = "Localhost", int tcpPort = 10923)
    {
        this.tcpClient = new TCPClient(Server, tcpPort);     

        this.udpClient = new UDPClient(Server);
    }

    public bool TryConnect()
    {
        int i = 0;
        while (i != 3)
        {
            if (TryConnectTCP())
            {
                udpClient.StartListening();
                return true;
            }
            else
            {
                i++;
            }
        }

        return false;
    }

    private bool TryConnectTCP()
    {
        try
        {
            tcpClient.Connect();

            return true;
        }
        catch
        {
            return false;
        }
    }
}
