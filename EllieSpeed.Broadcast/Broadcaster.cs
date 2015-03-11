//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EllieSpeed.Broadcast
{
  public class Broadcaster : IBikeDataBroadcaster, IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly IPEndPoint mEndPt;
    private readonly UdpClient mSender;

    public static int BroadcastPort
    {
      get
      {
        var cfg = ConfigurationManager.OpenExeConfiguration(new Broadcaster(string.Empty).GetType().Assembly.Location);
        var appSettings = (AppSettingsSection)cfg.GetSection("appSettings");

        return int.Parse(appSettings.Settings["BroadcastPort"].Value);
      }
    }

    public Broadcaster()
    {
      mEndPt = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);
      mSender = new UdpClient
                      {
                        EnableBroadcast = true
                      };
    }

    private Broadcaster(string requiredForStaticContext)
    {
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

    public void OnRunTelemetry(GPBikes.SPluginsBikeDataEx_t data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void OnTrackCenterline(GPBikes.SPluginsTrackSegment_t[] data)
    {
      var msg = ObjectToByteArray(data);
      SendMessage(msg);
    }

    public void Dispose()
    {
      if (Disposed)
      {
        return;
      }

      mSender.Close();
      Disposed = true;
    }
  }
}
