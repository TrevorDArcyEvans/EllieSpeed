//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Text;
using EllieSpeed.Common;
using EllieSpeed.Common.GPBikes;

namespace EllieSpeed.Receive
{
  public class BikeDataReceiver : ReceiverBase
  {
    public event EventHandler OnStartup;
    public event EventHandler OnShutdown;
    public event EventHandler<DataEventArgs<SPluginsBikeEvent_t>> OnEventInit;
    public event EventHandler<DataEventArgs<SPluginsBikeSession_t>> OnRunInit;
    public event EventHandler OnRunDeinit;
    public event EventHandler OnRunStart;
    public event EventHandler OnRunStop;
    public event EventHandler<DataEventArgs<SPluginsBikeLap_t>> OnRunLap;
    public event EventHandler<DataEventArgs<SPluginsBikeSplit_t>> OnRunSplit;
    public event EventHandler<DataEventArgs<SPluginsBikeDataEx_t>> OnRunTelemetry;
    public event EventHandler<DataEventArgs<SPluginsTrackSegment_t[]>> OnTrackCenterline;

    public BikeDataReceiver(int port) :
      base (port)
    {
    }

    protected override void ProcessMessage(byte[] msgBytes)
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

      if (obj is SPluginsBikeEvent_t && OnEventInit != null)
      {
        OnEventInit(this, new DataEventArgs<SPluginsBikeEvent_t>((SPluginsBikeEvent_t)obj));
        return;
      }

      if (obj is SPluginsBikeSession_t && OnRunInit != null)
      {
        OnRunInit(this, new DataEventArgs<SPluginsBikeSession_t>((SPluginsBikeSession_t)obj));
        return;
      }

      if (obj is SPluginsBikeLap_t && OnRunLap != null)
      {
        OnRunLap(this, new DataEventArgs<SPluginsBikeLap_t>((SPluginsBikeLap_t)obj));
        return;
      }

      if (obj is SPluginsBikeSplit_t && OnRunSplit != null)
      {
        OnRunSplit(this, new DataEventArgs<SPluginsBikeSplit_t>((SPluginsBikeSplit_t)obj));
        return;
      }

      if (obj is SPluginsBikeDataEx_t && OnRunTelemetry != null)
      {
        OnRunTelemetry(this, new DataEventArgs<SPluginsBikeDataEx_t>((SPluginsBikeDataEx_t)obj));
        return;
      }

      if (obj is SPluginsTrackSegment_t[] && OnTrackCenterline != null)
      {
        OnTrackCenterline(this, new DataEventArgs<SPluginsTrackSegment_t[]>((SPluginsTrackSegment_t[])obj));
        return;
      }
    }
  }
}
