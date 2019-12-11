using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPListener
{
    private UdpClient client;

    public UDPListener(UdpClient client)
    {
        this.client = client;
    }

    private void recvUDP(IAsyncResult res)
    {
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 10922);
        byte[] received = client.EndReceive(res, ref RemoteIpEndPoint);

        //Process codes
        MessageExecutor.Messages.Add(Message.FromJson(System.Text.Encoding.UTF8.GetString(Compressor.Decompress(received))));

        ReceiveDataUDP();
    }

    public void ReceiveDataUDP()
    {
        try
        {
            client.BeginReceive(new AsyncCallback(recvUDP), null);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
