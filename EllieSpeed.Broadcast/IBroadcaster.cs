//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

namespace EllieSpeed.Broadcast
{
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
    void OnTrackCenterline(GPBikes.SPluginsTrackSegment_t[] data);
  }
}
