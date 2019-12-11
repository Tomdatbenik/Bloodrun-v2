﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPListener
{
    Byte[] bytes = new Byte[1040];
    private NetworkStream stream;
    private Thread receiveThread;

    public TCPListener(NetworkStream stream)
    {
        this.stream = stream;
    }


    /// <summary>
    /// Reseave tcp message
    /// </summary>
    private void resvTCP()
    {
        while ((stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            Message message = Message.FromJson(System.Text.Encoding.UTF8.GetString(Compressor.Decompress(bytes)));
            Debug.Log(message.toString());
        }

        TryReceiveTcpMessage();
    }

    /// <summary>
    /// Try to receive a message otherwise stop listening.
    /// </summary>
    private void TryReceiveTcpMessage()
    {
        try
        {
            resvTCP();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log("Connection is closed");
        }
    }

    /// <summary>
    /// Start listening
    /// </summary>
    public void StartListening()
    {
        receiveThread = new Thread(new ThreadStart(TryReceiveTcpMessage))
        {
            IsBackground = true
        };

        receiveThread.Start();
    }
}
