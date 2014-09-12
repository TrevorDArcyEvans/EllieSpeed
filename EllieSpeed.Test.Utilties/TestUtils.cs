//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Test.Utilties
{
  public class TestUtils
  {
    public static GPBikes.SPluginsBikeEvent_t CreateBikeEvent()
    {
      var data = new GPBikes.SPluginsBikeEvent_t
      {
        RiderName = "trevorde",
        BikeID = "VFR 400",
        BikeName = "Ellie",
        NumberOfGears = 6,
        MaxRPM = 14500,
        Limiter = 15500,
        ShiftRPM = 14500,
        EngineOptTemperature = 85,
        EngineTemperatureAlarm = new float[2],
        MaxFuel = 14,
        Category = "Super Sports",
        TrackID = "20060220",
        TrackName = "Wanneroo",
        TrackLength = 2160
      };
      data.EngineTemperatureAlarm[0] = 50;
      data.EngineTemperatureAlarm[1] = 95;
      return data;
    }

    public static GPBikes.SPluginsBikeSession_t CreateBikeSession()
    {
      var data = new GPBikes.SPluginsBikeSession_t
      {
        Session = 4,
        Conditions = 2,
        AirTemperature = 21,
        TrackTemperature = 30,
        SetupFileName = "RainTyres"
      };
      return data;
    }

    public static GPBikes.SPluginsBikeLap_t CreateBikeLap()
    {
      var data = new GPBikes.SPluginsBikeLap_t
      {
        LapTime = 66000,
        Best = 1,
        LapNum = 6
      };
      return data;
    }

    public static GPBikes.SPluginsBikeSplit_t CreateBikeSplit()
    {
      var data = new GPBikes.SPluginsBikeSplit_t
      {
        Split = 3,
        SplitTime = 66000,
        BestDiff = 100
      };
      return data;
    }

    public static GPBikes.SPluginsBikeData_t CreateBikeData()
    {
      var data = new GPBikes.SPluginsBikeData_t
      {
        RPM = 13500f,
        EngineTemperature = 220f,
        WaterTemperature = 85f,
        Gear = 3,
        Fuel = 6f,
        Speedometer = 55.5f,
        PosX = 21f,
        PosY = 23f,
        PosZ = 25f,
        VelocityX = 24f,
        VelocityY = 26f,
        VelocityZ = 28f,
        AccelerationX = 3f,
        AccelerationY = 5f,
        AccelerationZ = 7f,
        Rot = new float[9],
        Yaw = 45f,
        Pitch = 22.5f,
        Roll = -12.5f,
        YawVelocity = 1f,
        PitchVelocity = 2f,
        RollVelocity = 3f,
        SuspNormLength = new float[2],
        Crashed = 1,
        Throttle = 0.95f,
        FrontBrake = 0.03f,
        RearBrake = 0.01f,
        Clutch = 1f,
        WheelSpeed = new float[2],
        PitLimiter = 1,
        EngineMapping = "RainTyres"
      };
      for (var i = 0; i < 9; i++)
      {
        data.Rot[i] = i * i;
      }
      for (var i = 0; i < 2; i++)
      {
        data.SuspNormLength[i] = 0.5f * (i + 1);
      }
      for (var i = 0; i < 2; i++)
      {
        data.WheelSpeed[i] = 100f * (i + 1);
      }
      return data;
    }

    public static GPBikes.SPluginsTrackSegment_t CreateTrackSegment()
    {
      var rand = new Random();
      var data = new GPBikes.SPluginsTrackSegment_t
      {
        Start = new[]
                {
                  (float)rand.NextDouble() * 10f,
                  (float)rand.NextDouble() * 10f
                },
        Angle = (float)rand.NextDouble() * 360f,
        Length = (float)rand.NextDouble() * 500f,
        Radius = (float)rand.NextDouble() * 100f,
        Type = rand.Next(5)
      };

      return data;
    }
  }
}
