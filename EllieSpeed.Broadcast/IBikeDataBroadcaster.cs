//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using EllieSpeed.Common.GPBikes;

namespace EllieSpeed.Broadcast
{
  public interface IBikeDataBroadcaster
  {
    void OnStartup();
    void OnShutdown();
    void OnEventInit(SPluginsBikeEvent_t data);
    void OnRunInit(SPluginsBikeSession_t data);
    void OnRunDeinit();
    void OnRunStart();
    void OnRunStop();
    void OnRunLap(SPluginsBikeLap_t data);
    void OnRunSplit(SPluginsBikeSplit_t data);
    void OnRunTelemetry(SPluginsBikeDataEx_t data);
    void OnTrackCenterline(SPluginsTrackSegment_t[] data);
  }
}
