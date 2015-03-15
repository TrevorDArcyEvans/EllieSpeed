//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Linq;
using EllieSpeed.Broadcast;
using EllieSpeed.Common;
using EllieSpeed.Common.GPBikes;
using EllieSpeed.Receive;

namespace EllieSpeed.DataLogger
{
  public abstract class BaseLogger : IDisposable
  {
    private readonly DataLogger mLogger;
    private readonly BikeDataReceiver mReceiver = new BikeDataReceiver(Broadcaster.BroadcastPort);

    protected abstract string ConnectionString { get; }

    public BaseLogger(DataLogger logger)
    {
      mLogger = logger;

      mReceiver.OnStartup += OnStartup;
      mReceiver.OnShutdown += OnShutdown;
      mReceiver.OnEventInit += OnEventInit;
      mReceiver.OnRunInit += OnRunInit;
      mReceiver.OnRunDeinit += OnRunDeinit;
      mReceiver.OnRunStart += OnRunStart;
      mReceiver.OnRunStop += OnRunStop;
      mReceiver.OnRunLap += OnRunLap;
      mReceiver.OnRunSplit += OnRunSplit;
      mReceiver.OnRunTelemetry += OnRunTelemetry;
      mReceiver.OnTrackCenterline += OnTrackCenterline;
    }

    private void OnStartup(object sender, EventArgs e)
    {
    }

    private void OnShutdown(object sender, EventArgs e)
    {
    }

    private void OnEventInit(object sender, DataEventArgs<SPluginsBikeEvent_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeEvent
                      {
                        RiderName = data.RiderName,
                        BikeID = data.BikeID,
                        BikeName = data.BikeName,
                        NumberOfGears = data.NumberOfGears,
                        MaxRPM = data.MaxRPM,
                        Limiter = data.Limiter,
                        ShiftRPM = data.ShiftRPM,
                        EngineOptTemperature = data.EngineOptTemperature,
                        EngineTemperatureAlarmLower = data.EngineTemperatureAlarm[0],
                        EngineTemperatureAlarmUpper = data.EngineTemperatureAlarm[1],
                        MaxFuel = data.MaxFuel,
                        Category = data.Category,
                        TrackID = data.TrackID,
                        TrackName = data.TrackName,
                        TrackLength = data.TrackLength
                      };

      mLogger.BikeEvents.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnRunInit(object sender, DataEventArgs<SPluginsBikeSession_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeSession
                      {
                        Session = data.Session,
                        Conditions = data.Conditions,
                        AirTemperature = data.AirTemperature,
                        TrackTemperature = data.TrackTemperature,
                        SetupFileName = data.SetupFileName
                      };

      mLogger.BikeSessions.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnRunDeinit(object sender, EventArgs e)
    {
    }

    private void OnRunStart(object sender, EventArgs e)
    {
    }

    private void OnRunStop(object sender, EventArgs e)
    {
    }

    private void OnRunLap(object sender, DataEventArgs<SPluginsBikeLap_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeLap
                      {
                        LapTime = data.LapTime,
                        Best = data.Best,
                        LapNum = data.LapNum
                      };

      mLogger.BikeLaps.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnRunSplit(object sender, DataEventArgs<SPluginsBikeSplit_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeSplit
                      {
                        Split = data.Split,
                        SplitTime = data.SplitTime,
                        BestDiff = data.BestDiff
                      };

      mLogger.BikeSplits.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnRunTelemetry(object sender, DataEventArgs<SPluginsBikeDataEx_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeData
                      {
                        TrackTime = data.TrackTime,
                        TrackPosition = data.TrackPosition,
                        RPM = data.BikeData.RPM,
                        EngineTemperature = data.BikeData.EngineTemperature,
                        WaterTemperature = data.BikeData.WaterTemperature,
                        Gear = data.BikeData.Gear,
                        Fuel = data.BikeData.Fuel,
                        Speedometer = data.BikeData.Speedometer,
                        PosX = data.BikeData.PosX,
                        PosY = data.BikeData.PosY,
                        PosZ = data.BikeData.PosZ,
                        VelocityX = data.BikeData.VelocityX,
                        VelocityY = data.BikeData.VelocityY,
                        VelocityZ = data.BikeData.VelocityZ,
                        AccelerationX = data.BikeData.AccelerationX,
                        AccelerationY = data.BikeData.AccelerationY,
                        AccelerationZ = data.BikeData.AccelerationZ,
                        Rot0 = data.BikeData.Rot[0],
                        Rot1 = data.BikeData.Rot[1],
                        Rot2 = data.BikeData.Rot[2],
                        Rot3 = data.BikeData.Rot[3],
                        Rot4 = data.BikeData.Rot[4],
                        Rot5 = data.BikeData.Rot[5],
                        Rot6 = data.BikeData.Rot[6],
                        Rot7 = data.BikeData.Rot[7],
                        Rot8 = data.BikeData.Rot[8],
                        Yaw = data.BikeData.Yaw,
                        Pitch = data.BikeData.Pitch,
                        Roll = data.BikeData.Roll,
                        YawVelocity = data.BikeData.YawVelocity,
                        PitchVelocity = data.BikeData.PitchVelocity,
                        RollVelocity = data.BikeData.RollVelocity,
                        SuspNormLengthFront = data.BikeData.SuspNormLength[0],
                        SuspNormLengthRear = data.BikeData.SuspNormLength[1],
                        Crashed = data.BikeData.Crashed,
                        Steer = data.BikeData.Steer,
                        Throttle = data.BikeData.Throttle,
                        FrontBrake = data.BikeData.FrontBrake,
                        RearBrake = data.BikeData.RearBrake,
                        Clutch = data.BikeData.Clutch,
                        WheelSpeedFront = data.BikeData.WheelSpeed[0],
                        WheelSpeedRear = data.BikeData.WheelSpeed[1],
                        PitLimiter = data.BikeData.PitLimiter,
                        EngineMapping = data.BikeData.EngineMapping
                      };

      mLogger.BikeDatas.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnTrackCenterline(object sender, DataEventArgs<SPluginsTrackSegment_t[]> e)
    {
      var data = e.Data;
      var dbObjs = from ts in data
                   let dbObj = new TrackSegment
                                   {
                                     Type = ts.Type,
                                     Length = ts.Length,
                                     Radius = ts.Radius,
                                     Angle = ts.Angle,
                                     Start1 = ts.Start[0],
                                     Start2 = ts.Start[1]
                                   }
                   select dbObj;

      foreach (var ts in dbObjs)
      {
        mLogger.TrackSegments.AddObject(ts);
      }
      mLogger.SaveChanges();
    }

    public virtual void Dispose()
    {
      mLogger.Dispose();
      mReceiver.Dispose();
    }
  }
}
