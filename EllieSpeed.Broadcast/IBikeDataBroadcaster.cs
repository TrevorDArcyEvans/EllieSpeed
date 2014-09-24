//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.Broadcast
{
  public interface IBikeDataBroadcaster
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
    void OnRunTelemetry(GPBikes.SPluginsBikeDataEx_t data);
    void OnTrackCenterline(GPBikes.SPluginsTrackSegment_t[] data);
  }
}
