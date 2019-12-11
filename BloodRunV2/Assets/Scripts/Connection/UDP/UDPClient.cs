using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UDPClient
{
    /// <summary>
    /// ip endpoin
    /// </summary>
    private IPEndPoint ipLocalEndPoint;

    /// <summary>
    /// client that will send and receive
    /// </summary>
    private UdpClient client;

    /// <summary>
    /// The port where the UDP message get send and received from.
    /// </summary>
    private int port;

    /// <summary>
    /// IP address
    /// </summary>
    private IPAddress address;

    /// <summary>
    /// Ip address
    /// </summary>
    private string ip;

    /// <summary>
    /// Writer of udp messages
    /// </summary>
    private UDPWriter writer;

    /// <summary>
    /// Listener of udp messages
    /// </summary>
    private UDPListener listener;


    /// <summary>
    /// Thread where messages will be received
    /// </summary>
    private Thread receiveThread;

    /// <summary>
    /// Propertie of writer of udp messages
    /// </summary>
    public UDPListener Listener { get => listener; set => listener = value; }

    /// <summary>
    /// Propertie of listener of udp messages
    /// </summary>
    public UDPWriter Writer { get => writer; set => writer = value; }

    /// <summary>
    /// Ip of this device
    /// </summary>
    public string Ip { get => ip; set => ip = value; }
    
    /// <summary>
    /// Port that listens to UDP messages on this device
    /// </summary>
    public int Port { get => port; set => port = value; }



    /// <summary>
    /// Constructor of udpclient
    /// </summary>
    /// <param name="Port">The port where the UDP message get send and received from.</param>
    public UDPClient(int Port = 19224)
    {
        //Get ip of this device
        ip = "";
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                this.ip = ip.ToString();
            }
        }

        IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        ipLocalEndPoint = new IPEndPoint(ipAddress, Port);

        client = new UdpClient(ipLocalEndPoint.Port);
        port = ((IPEndPoint)client.Client.LocalEndPoint).Port;

        Setup();
    }

    /// <summary>
    /// Set up listener and writer
    /// </summary>
    private void Setup()
    {
        listener = new UDPListener(client);
        writer = new UDPWriter(client);
    }

    /// <summary>
    /// Start listeneing to incoming UDP messages
    /// </summary>
    public void StartListening()
    {
        receiveThread = new Thread(new ThreadStart(listener.ReceiveDataUDP))
        {
            IsBackground = true
        };

        receiveThread.Start();
    }

    public void SendUdpMessage(Message message)
    {
        writer.SendUdpMessage(message);
    }
}
