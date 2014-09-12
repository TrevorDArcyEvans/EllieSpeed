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
using System.Text;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Receive
{
  public class Receiver : IDisposable
  {
    public event EventHandler OnStartup;
    public event EventHandler OnShutdown;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsBikeEvent_t>> OnEventInit;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsBikeSession_t>> OnRunInit;
    public event EventHandler OnRunDeinit;
    public event EventHandler OnRunStart;
    public event EventHandler OnRunStop;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsBikeLap_t>> OnRunLap;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsBikeSplit_t>> OnRunSplit;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsBikeData_t>> OnRunTelemetry;
    public event EventHandler<DataEventArgs<GPBikes.SPluginsTrackSegment_t[]>> OnTrackCenterline;

    private readonly UdpClient mReceiver;
    private IPEndPoint mEndPt;

    public Receiver(int port)
    {
      mEndPt = new IPEndPoint(IPAddress.Any, port);
      mReceiver = new UdpClient();
      mReceiver.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      mReceiver.Client.Bind(mEndPt);
      StartListening();
    }

    private void StartListening()
    {
      mReceiver.BeginReceive(Receive, null);
    }

    private void Receive(IAsyncResult ar)
    {
      var msgBytes = mReceiver.EndReceive(ar, ref mEndPt);
      ProcessMessage(msgBytes);
      StartListening();
    }

    private Object ByteArrayToObject(byte[] arrBytes)
    {
      var memStream = new MemoryStream();
      var bf = new BinaryFormatter();

      memStream.Write(arrBytes, 0, arrBytes.Length);
      memStream.Seek(0, SeekOrigin.Begin);

      var obj = bf.Deserialize(memStream);

      return obj;
    }

    private void ProcessMessage(byte[] msgBytes)
    {
      var msg = Encoding.ASCII.GetString(msgBytes);

      if (msg == "OnStartup" && OnStartup != null)
      {
        OnStartup(this, new EventArgs());
        return;
      }

      if (msg == "OnShutdown" && OnShutdown != null)
      {
        OnShutdown(this, new EventArgs());
        return;
      }

      if (msg == "OnRunDeinit" && OnRunDeinit != null)
      {
        OnRunDeinit(this, new EventArgs());
        return;
      }

      if (msg == "OnRunStart" && OnRunStart != null)
      {
        OnRunStart(this, new EventArgs());
        return;
      }

      if (msg == "OnRunStop" && OnRunStop != null)
      {
        OnRunStop(this, new EventArgs());
        return;
      }

      // got an object but which one?
      var obj = ByteArrayToObject(msgBytes);

      if (obj is GPBikes.SPluginsBikeEvent_t && OnEventInit != null)
      {
        OnEventInit(this, new DataEventArgs<GPBikes.SPluginsBikeEvent_t>((GPBikes.SPluginsBikeEvent_t)obj));
        return;
      }

      if (obj is GPBikes.SPluginsBikeSession_t && OnRunInit != null)
      {
        OnRunInit(this, new DataEventArgs<GPBikes.SPluginsBikeSession_t>((GPBikes.SPluginsBikeSession_t)obj));
        return;
      }

      if (obj is GPBikes.SPluginsBikeLap_t && OnRunLap != null)
      {
        OnRunLap(this, new DataEventArgs<GPBikes.SPluginsBikeLap_t>((GPBikes.SPluginsBikeLap_t)obj));
        return;
      }

      if (obj is GPBikes.SPluginsBikeSplit_t && OnRunSplit != null)
      {
        OnRunSplit(this, new DataEventArgs<GPBikes.SPluginsBikeSplit_t>((GPBikes.SPluginsBikeSplit_t)obj));
        return;
      }

      if (obj is GPBikes.SPluginsBikeData_t && OnRunTelemetry != null)
      {
        OnRunTelemetry(this, new DataEventArgs<GPBikes.SPluginsBikeData_t>((GPBikes.SPluginsBikeData_t)obj));
        return;
      }

      if (obj is GPBikes.SPluginsTrackSegment_t[] && OnTrackCenterline != null)
      {
        OnTrackCenterline(this, new DataEventArgs<GPBikes.SPluginsTrackSegment_t[]>((GPBikes.SPluginsTrackSegment_t[])obj));
        return;
      }
    }

    public void Dispose()
    {
      mReceiver.Close();
    }
  }
}
