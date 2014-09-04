//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System.Runtime.InteropServices;
using EllieSpeed.Interfaces;

namespace EllieSpeed.Broadcast
{
  [ComVisible(true)]
  [Guid("4E134DDD-DC1D-4BFC-8B8F-5D12D4769B8F")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  public interface IBroadcaster
  {
    void OnStartup();
    void OnShutdown();
    void OnEventInit(GPBikes.SPluginsBikeEvent_t data);
    void OnRunInit(GPBikes.SPluginsBikeSession_t data);
    void OnRunDeinit();
    void OnRunStart();
    void OnRunStop();
    void OnRunLap(GPBikes.SPluginsBikeLap_t data);
    void OnRunSplit(GPBikes.SPluginsBikeSplit_t data);
    void OnRunTelemetry(GPBikes.SPluginsBikeData_t data);
  }
}
