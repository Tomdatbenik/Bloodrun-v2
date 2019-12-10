using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TCPClient
{
    private TCPSender sender;
    private TCPListener listener;

    private NetworkStream stream;
    private TcpClient client;
    readonly int port;
    private string server = "Localhost";

    public TCPListener Listener { get => listener; set => listener = value; }
    public TCPSender Sender { get => sender; set => sender = value; }

    public TCPClient(string server, TCPSender sender, TCPListener listener, int port = 10923)
    {
        this.port = port;
        this.server = server;
        this.sender = sender;
        this.listener = listener;
    }

    public void Connect()
    {
        this.client = new TcpClient(server, port);
        this.stream = client.GetStream();

        this.sender = new TCPSender(this.stream);
    }

    public void Close()
    {
        stream.Close();
        client.Close();
    }
}
