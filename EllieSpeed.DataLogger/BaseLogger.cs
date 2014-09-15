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
using EllieSpeed.Receive;

namespace EllieSpeed.DataLogger
{
  public abstract class BaseLogger : IDisposable
  {
    private readonly DataLogger mLogger;
    private readonly Receiver mReceiver = new Receiver(Broadcaster.BroadcastPort);

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

    private void OnEventInit(object sender, DataEventArgs<GPBikes.SPluginsBikeEvent_t> e)
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

    private void OnRunInit(object sender, DataEventArgs<GPBikes.SPluginsBikeSession_t> e)
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

    private void OnRunLap(object sender, DataEventArgs<GPBikes.SPluginsBikeLap_t> e)
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

    private void OnRunSplit(object sender, DataEventArgs<GPBikes.SPluginsBikeSplit_t> e)
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

    private void OnRunTelemetry(object sender, DataEventArgs<GPBikes.SPluginsBikeData_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeData
                      {
                        TrackTime = data.TrackTime,
                        TrackPosition = data.TrackPosition,
                        RPM = data.RPM,
                        EngineTemperature = data.EngineTemperature,
                        WaterTemperature = data.WaterTemperature,
                        Gear = data.Gear,
                        Fuel = data.Fuel,
                        Speedometer = data.Speedometer,
                        PosX = data.PosX,
                        PosY = data.PosY,
                        PosZ = data.PosZ,
                        VelocityX = data.VelocityX,
                        VelocityY = data.VelocityY,
                        VelocityZ = data.VelocityZ,
                        AccelerationX = data.AccelerationX,
                        AccelerationY = data.AccelerationY,
                        AccelerationZ = data.AccelerationZ,
                        Rot0 = data.Rot[0],
                        Rot1 = data.Rot[1],
                        Rot2 = data.Rot[2],
                        Rot3 = data.Rot[3],
                        Rot4 = data.Rot[4],
                        Rot5 = data.Rot[5],
                        Rot6 = data.Rot[6],
                        Rot7 = data.Rot[7],
                        Rot8 = data.Rot[8],
                        Yaw = data.Yaw,
                        Pitch = data.Pitch,
                        Roll = data.Roll,
                        YawVelocity = data.YawVelocity,
                        PitchVelocity = data.PitchVelocity,
                        RollVelocity = data.RollVelocity,
                        SuspNormLengthFront = data.SuspNormLength[0],
                        SuspNormLengthRear = data.SuspNormLength[1],
                        Crashed = data.Crashed,
                        Steer = data.Steer,
                        Throttle = data.Throttle,
                        FrontBrake = data.FrontBrake,
                        RearBrake = data.RearBrake,
                        Clutch = data.Clutch,
                        WheelSpeedFront = data.WheelSpeed[0],
                        WheelSpeedRear = data.WheelSpeed[1],
                        PitLimiter = data.PitLimiter,
                        EngineMapping = data.EngineMapping
                      };

      mLogger.BikeDatas.AddObject(dbObj);
      mLogger.SaveChanges();
    }

    private void OnTrackCenterline(object sender, DataEventArgs<GPBikes.SPluginsTrackSegment_t[]> e)
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
