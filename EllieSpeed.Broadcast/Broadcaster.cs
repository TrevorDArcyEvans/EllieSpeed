//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using EllieSpeed.Interfaces;

namespace EllieSpeed.Broadcast
{
  [ComVisible(true)]
  [Guid("2FC343DE-970F-46B8-8C66-C66A92E70A7B")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("EllieSpeed.Broadcaster")]
  public class Broadcaster : IBroadcaster, IDisposable
  {
    private readonly IPEndPoint mEndPt;
    private readonly UdpClient mSender;

    public Broadcaster()
    {
      // TODO   read from config file
      const int BroadcastPort = 11000;

      mEndPt = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);
      mSender = new UdpClient();
    }

    private void SendMessage(byte[] msg)
    {
      mSender.BeginSend(msg, msg.Length, mEndPt,
        ar =>
        {
          var u = (UdpClient)ar.AsyncState;
          u.EndSend(ar);
        },
        mSender);
    }

    private byte[] ObjectToByteArray(Object obj)
    {
      var bf = new BinaryFormatter();
      using (var ms = new MemoryStream())
      {
        bf.Serialize(ms, obj);
        return ms.ToArray();
      }
    }

    public void OnStartup()
    {
      var msg = Encoding.ASCII.GetBytes(@"OnStartup");
      SendMessage(msg);
    }

    public void OnShutdown()
    {
      var msg = Encoding.ASCII.GetBytes(@"OnShutdown");
      SendMessage(msg);
    }

    public void OnEventInit(GPBikes.SPluginsBikeEvent_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnRunInit(GPBikes.SPluginsBikeSession_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnRunDeinit()
    {
      var msg = Encoding.ASCII.GetBytes(@"OnRunDeinit");
      SendMessage(msg);
    }

    public void OnRunStart()
    {
      var msg = Encoding.ASCII.GetBytes(@"OnRunStart");
      SendMessage(msg);
    }

    public void OnRunStop()
    {
      var msg = Encoding.ASCII.GetBytes(@"OnRunStop");
      SendMessage(msg);
    }

    public void OnRunLap(GPBikes.SPluginsBikeLap_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnRunSplit(GPBikes.SPluginsBikeSplit_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnRunTelemetry(GPBikes.SPluginsBikeData_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnTrackCenterline(IPluginsTrackSegmentInfo[] data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void Dispose()
    {
      mSender.Close();
    }
  }
}
