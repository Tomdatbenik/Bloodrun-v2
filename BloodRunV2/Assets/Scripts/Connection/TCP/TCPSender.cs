using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TCPSender
{

    private NetworkStream stream;

    public TCPSender(NetworkStream stream)
    {
        this.stream = stream;
    }

    public void Send(Message message)
    {
        //Send write
        if (this.stream.CanWrite)
        {
            byte[] data = Compressor.Compress(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message)));
            this.stream.Write(data, 0, data.Length);
            this.stream.Flush();
        }
        else
        {
            Debug.LogError("Sorry.  You cannot write to this NetworkStream.");
        }
    }

}
