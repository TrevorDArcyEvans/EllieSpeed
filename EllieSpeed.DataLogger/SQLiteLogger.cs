﻿//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using EllieSpeed.Broadcast;
using EllieSpeed.Receive;

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger : IDisposable
  {
    private readonly DataLogger mLogger;
    private readonly Receiver mReceiver = new Receiver(Broadcaster.BroadcastPort);
    private readonly IBroadcaster mRecLog = new NullBroadcaster();

    public SQLiteLogger(string filePath)
    {
      if (!File.Exists(filePath))
      {
        var assyPath = Assembly.GetExecutingAssembly().Location;
        var assyDir = Path.GetDirectoryName(assyPath);
        var baseDataFilePath = Path.Combine(assyDir, "EllieSpeed.DataLogger.sqlite3");
        File.Copy(baseDataFilePath, filePath);
      }

      if (Directory.Exists(filePath))
      {
        throw new ArgumentException(filePath + " is a directory");
      }

      var connStr = string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                          "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", filePath);

      mLogger = new DataLogger(connStr);

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

    internal SQLiteLogger(string filePath, IBroadcaster broadcaster) :
      this(filePath)
    {
      mRecLog = broadcaster;
    }

    public void OnStartup(object sender, EventArgs e)
    {
      mRecLog.OnStartup();
    }

    private void OnShutdown(object sender, EventArgs e)
    {
      mRecLog.OnShutdown();
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

      mRecLog.OnEventInit(data);
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

      mRecLog.OnRunInit(e.Data);
    }

    private void OnRunDeinit(object sender, EventArgs e)
    {
      mRecLog.OnRunDeinit();
    }

    private void OnRunStart(object sender, EventArgs e)
    {
      mRecLog.OnRunStart();
    }

    private void OnRunStop(object sender, EventArgs e)
    {
      mRecLog.OnRunStop();
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

      mRecLog.OnRunLap(e.Data);
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

      mRecLog.OnRunSplit(e.Data);
    }

    private void OnRunTelemetry(object sender, DataEventArgs<GPBikes.SPluginsBikeData_t> e)
    {
      var data = e.Data;
      var dbObj = new BikeData
      {
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

      mRecLog.OnRunTelemetry(e.Data);
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

      mRecLog.OnTrackCenterline(e.Data);
    }

    public void Dispose()
    {
      mLogger.Dispose();
      mReceiver.Dispose();
    }

    private class NullBroadcaster : IBroadcaster
    {
      public void OnStartup()
      {
      }

      public void OnShutdown()
      {
      }

      public void OnEventInit(GPBikes.SPluginsBikeEvent_t data)
      {
      }

      public void OnRunInit(GPBikes.SPluginsBikeSession_t data)
      {
      }

      public void OnRunDeinit()
      {
      }

      public void OnRunStart()
      {
      }

      public void OnRunStop()
      {
      }

      public void OnRunLap(GPBikes.SPluginsBikeLap_t data)
      {
      }

      public void OnRunSplit(GPBikes.SPluginsBikeSplit_t data)
      {
      }

      public void OnRunTelemetry(GPBikes.SPluginsBikeData_t data)
      {
      }

      public void OnTrackCenterline(GPBikes.SPluginsTrackSegment_t[] data)
      {
      }

      public void Dispose()
      {
      }
    }
  }
}
