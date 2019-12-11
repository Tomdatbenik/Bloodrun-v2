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
    private string server;

    public TCPListener Listener { get => listener; set => listener = value; }
    public TCPSender Sender { get => sender; set => sender = value; }

    public TCPClient(string server, int port)
    {
        this.port = port;
        this.server = server;
    }

    public void Connect()
    {
        //Set up client
        client = new TcpClient(server, port);

        //Get stream from client
        stream = client.GetStream();

        //Create sender to send messages to the server
        sender = new TCPSender(this.stream);

        //Create listener to listen to messages from server
        listener = new TCPListener(this.stream);

        //Start listenening
        listener.StartListening();
    }

    public void Close()
    {
        stream.Close();
        client.Close();
    }
}
