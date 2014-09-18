//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace EllieSpeed.Receive
{
  public abstract class ReceiverBase : IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly UdpClient mReceiver;
    private IPEndPoint mEndPt;

    protected ReceiverBase(int port)
    {
      mEndPt = new IPEndPoint(IPAddress.Any, port);
      mReceiver = new UdpClient();
      mReceiver.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      mReceiver.Client.Bind(mEndPt);
      StartListening();
    }

    protected abstract void ProcessMessage(byte[] msgBytes);

    private void StartListening()
    {
      mReceiver.BeginReceive(Receive, null);
    }

    private void Receive(IAsyncResult ar)
    {
      if (Disposed)
      {
        return;
      }

      var msgBytes = mReceiver.EndReceive(ar, ref mEndPt);
      ProcessMessage(msgBytes);
      StartListening();
    }

    protected Object ByteArrayToObject(byte[] arrBytes)
    {
      var memStream = new MemoryStream();
      var bf = new BinaryFormatter();

      memStream.Write(arrBytes, 0, arrBytes.Length);
      memStream.Seek(0, SeekOrigin.Begin);

      var obj = bf.Deserialize(memStream);

      return obj;
    }

    public void Dispose()
    {
      if (Disposed)
      {
        return;
      }

      mReceiver.Close();
      Disposed = true;
    }
  }
}
