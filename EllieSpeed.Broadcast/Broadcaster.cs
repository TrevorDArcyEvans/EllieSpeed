//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Runtime.InteropServices;
using EllieSpeed.Interfaces;

namespace EllieSpeed.Broadcast
{
  [ComVisible(true)]
  [Guid("2FC343DE-970F-46B8-8C66-C66A92E70A7B")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("EllieSpeed.Broadcaster")]
  public class Broadcaster : IBroadcaster
  {
    public void OnStartup()
    {
      Console.WriteLine("OnStartup");
    }

    public void OnShutdown()
    {
      Console.WriteLine("OnShutdown");
    }

    public void OnEventInit(GPBikes.SPluginsBikeEvent_t data)
    {
      Console.WriteLine("OnEventInit");
    }

    public void OnRunInit(GPBikes.SPluginsBikeSession_t data)
    {
      Console.WriteLine("OnRunInit");
    }

    public void OnRunDeinit()
    {
      Console.WriteLine("OnRunDeinit");
    }

    public void OnRunStart()
    {
      Console.WriteLine("OnRunStart");
    }

    public void OnRunStop()
    {
      Console.WriteLine("OnRunStop");
    }

    public void OnRunLap(GPBikes.SPluginsBikeLap_t data)
    {
      Console.WriteLine("OnRunLap");
    }

    public void OnRunSplit(GPBikes.SPluginsBikeSplit_t data)
    {
      Console.WriteLine("OnRunSplit");
    }

    public void OnRunTelemetry(GPBikes.SPluginsBikeData_t data)
    {
      Console.WriteLine("OnRunTelemetry");
    }

    public void OnTrackCenterline(IPluginsTrackSegmentInfo[] data)
    {
      Console.WriteLine("OnTrackCenterline");
    }
  }
}
