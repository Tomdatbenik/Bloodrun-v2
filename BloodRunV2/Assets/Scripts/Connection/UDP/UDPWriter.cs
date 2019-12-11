using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UDPWriter
{

    private UdpClient client;

    public UDPWriter(UdpClient client)
    {
        this.client = client;
    }

    public void SendUdpMessage(Message message, string ServerIp = "localhost", int Port = 10922)
    {
        byte[] data = Compressor.Compress(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message)));

        client.Send(data, data.Length, ServerIp, Port);
    }
}
