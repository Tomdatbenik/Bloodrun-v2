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

    public bool TrySend(Message message)
    {
        //Send write
        if (this.stream.CanWrite)
        {
            byte[] data = Compressor.Compress(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message)));
            stream.Write(data, 0, data.Length);
            stream.Flush();
            return true;
        }
        else
        {
            Debug.LogError("Sorry.  You cannot write to this NetworkStream.");
            return false;
        }
    }

}
