//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using EllieSpeed.Broadcast;
using EllieSpeed.DataLogger;
using NUnit.Framework;

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

    public static GPBikes.SPluginsBikeDataEx_t CreateBikeDataEx()
    {
      var data = new GPBikes.SPluginsBikeDataEx_t
      {
        TrackTime = 1.57f,
        TrackPosition = 0.657f,
        BikeData = CreateBikeData()
      };

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

    public static void AssertAreEqual(GPBikes.SPluginsBikeEvent_t recData, GPBikes.SPluginsBikeEvent_t data)
    {
      Assert.AreEqual(recData.RiderName, data.RiderName);
      Assert.AreEqual(recData.BikeID, data.BikeID);
      Assert.AreEqual(recData.BikeName, data.BikeName);
      Assert.AreEqual(recData.NumberOfGears, data.NumberOfGears);
      Assert.AreEqual(recData.MaxRPM, data.MaxRPM);
      Assert.AreEqual(recData.Limiter, data.Limiter);
      Assert.AreEqual(recData.ShiftRPM, data.ShiftRPM);
      Assert.AreEqual(recData.EngineOptTemperature, data.EngineOptTemperature);
      Assert.AreEqual(recData.EngineTemperatureAlarm[0], data.EngineTemperatureAlarm[0]);
      Assert.AreEqual(recData.EngineTemperatureAlarm[1], data.EngineTemperatureAlarm[1]);
      Assert.AreEqual(recData.MaxFuel, data.MaxFuel);
      Assert.AreEqual(recData.Category, data.Category);
      Assert.AreEqual(recData.TrackID, data.TrackID);
      Assert.AreEqual(recData.TrackName, data.TrackName);
      Assert.AreEqual(recData.TrackLength, data.TrackLength);
    }

    public static void AssertAreEqual(BikeEvent recData, GPBikes.SPluginsBikeEvent_t data)
    {
      Assert.AreEqual(recData.RiderName, data.RiderName);
      Assert.AreEqual(recData.BikeID, data.BikeID);
      Assert.AreEqual(recData.BikeName, data.BikeName);
      Assert.AreEqual(recData.NumberOfGears, data.NumberOfGears);
      Assert.AreEqual(recData.MaxRPM, data.MaxRPM);
      Assert.AreEqual(recData.Limiter, data.Limiter);
      Assert.AreEqual(recData.ShiftRPM, data.ShiftRPM);
      Assert.AreEqual(recData.EngineOptTemperature, data.EngineOptTemperature);
      Assert.AreEqual(recData.EngineTemperatureAlarmLower, data.EngineTemperatureAlarm[0]);
      Assert.AreEqual(recData.EngineTemperatureAlarmUpper, data.EngineTemperatureAlarm[1]);
      Assert.AreEqual(recData.MaxFuel, data.MaxFuel);
      Assert.AreEqual(recData.Category, data.Category);
      Assert.AreEqual(recData.TrackID, data.TrackID);
      Assert.AreEqual(recData.TrackName, data.TrackName);
      Assert.AreEqual(recData.TrackLength, data.TrackLength);
    }

    public static void AssertAreEqual(GPBikes.SPluginsBikeSession_t recData, GPBikes.SPluginsBikeSession_t data)
    {
      Assert.AreEqual(recData.Session, data.Session);
      Assert.AreEqual(recData.Conditions, data.Conditions);
      Assert.AreEqual(recData.AirTemperature, data.AirTemperature);
      Assert.AreEqual(recData.TrackTemperature, data.TrackTemperature);
      Assert.AreEqual(recData.SetupFileName, data.SetupFileName);
    }

    public static void AssertAreEqual(BikeSession recData, GPBikes.SPluginsBikeSession_t data)
    {
      Assert.AreEqual(recData.Session, data.Session);
      Assert.AreEqual(recData.Conditions, data.Conditions);
      Assert.AreEqual(recData.AirTemperature, data.AirTemperature);
      Assert.AreEqual(recData.TrackTemperature, data.TrackTemperature);
      Assert.AreEqual(recData.SetupFileName, data.SetupFileName);
    }

    public static void AssertAreEqual(GPBikes.SPluginsBikeLap_t recData, GPBikes.SPluginsBikeLap_t data)
    {
      Assert.AreEqual(recData.LapTime, data.LapTime);
      Assert.AreEqual(recData.Best, data.Best);
      Assert.AreEqual(recData.LapNum, data.LapNum);
    }

    public static void AssertAreEqual(BikeLap recData, GPBikes.SPluginsBikeLap_t data)
    {
      Assert.AreEqual(recData.LapTime, data.LapTime);
      Assert.AreEqual(recData.Best, data.Best);
      Assert.AreEqual(recData.LapNum, data.LapNum);
    }

    public static void AssertAreEqual(GPBikes.SPluginsBikeSplit_t recData, GPBikes.SPluginsBikeSplit_t data)
    {
      Assert.AreEqual(recData.Split, data.Split);
      Assert.AreEqual(recData.SplitTime, data.SplitTime);
      Assert.AreEqual(recData.BestDiff, data.BestDiff);
    }

    public static void AssertAreEqual(BikeSplit recData, GPBikes.SPluginsBikeSplit_t data)
    {
      Assert.AreEqual(recData.Split, data.Split);
      Assert.AreEqual(recData.SplitTime, data.SplitTime);
      Assert.AreEqual(recData.BestDiff, data.BestDiff);
    }

    public static void AssertAreEqual(GPBikes.SPluginsBikeData_t recData, GPBikes.SPluginsBikeData_t data)
    {
      //Assert.AreEqual(recData.TrackTime, data.TrackTime);
      //Assert.AreEqual(recData.TrackPosition, data.TrackPosition);

      Assert.AreEqual(recData.RPM, data.RPM);
      Assert.AreEqual(recData.EngineTemperature, data.EngineTemperature);
      Assert.AreEqual(recData.WaterTemperature, data.WaterTemperature);
      Assert.AreEqual(recData.Gear, data.Gear);
      Assert.AreEqual(recData.Fuel, data.Fuel);
      Assert.AreEqual(recData.Speedometer, data.Speedometer);

      Assert.AreEqual(recData.PosX, data.PosX);
      Assert.AreEqual(recData.PosY, data.PosY);
      Assert.AreEqual(recData.PosZ, data.PosZ);

      Assert.AreEqual(recData.VelocityX, data.VelocityX);
      Assert.AreEqual(recData.VelocityY, data.VelocityY);
      Assert.AreEqual(recData.VelocityZ, data.VelocityZ);

      Assert.AreEqual(recData.AccelerationX, data.AccelerationX);
      Assert.AreEqual(recData.AccelerationY, data.AccelerationY);
      Assert.AreEqual(recData.AccelerationZ, data.AccelerationZ);

      for (var i = 0; i < 9; i++)
      {
        Assert.AreEqual(recData.Rot[i], data.Rot[i]);
      }

      Assert.AreEqual(recData.Yaw, data.Yaw);
      Assert.AreEqual(recData.Pitch, data.Pitch);
      Assert.AreEqual(recData.Roll, data.Roll);

      Assert.AreEqual(recData.YawVelocity, data.YawVelocity);
      Assert.AreEqual(recData.PitchVelocity, data.PitchVelocity);
      Assert.AreEqual(recData.RollVelocity, data.RollVelocity);

      for (var i = 0; i < 2; i++)
      {
        Assert.AreEqual(recData.SuspNormLength[i], data.SuspNormLength[i]);
      }

      Assert.AreEqual(recData.Crashed, data.Crashed);
      Assert.AreEqual(recData.Throttle, data.Throttle);
      Assert.AreEqual(recData.FrontBrake, data.FrontBrake);
      Assert.AreEqual(recData.RearBrake, data.RearBrake);
      Assert.AreEqual(recData.Clutch, data.Clutch);

      for (var i = 0; i < 2; i++)
      {
        Assert.AreEqual(recData.WheelSpeed[i], data.WheelSpeed[i]);
      }

      Assert.AreEqual(recData.PitLimiter, data.PitLimiter);
      Assert.AreEqual(recData.EngineMapping, data.EngineMapping);
    }

    public static void AssertAreEqual(BikeData recData, GPBikes.SPluginsBikeData_t data)
    {
      Assert.AreEqual(recData.RPM, data.RPM);
      Assert.AreEqual(recData.EngineTemperature, data.EngineTemperature);
      Assert.AreEqual(recData.WaterTemperature, data.WaterTemperature);
      Assert.AreEqual(recData.Gear, data.Gear);
      Assert.AreEqual(recData.Fuel, data.Fuel);
      Assert.AreEqual(recData.Speedometer, data.Speedometer);

      Assert.AreEqual(recData.PosX, data.PosX);
      Assert.AreEqual(recData.PosY, data.PosY);
      Assert.AreEqual(recData.PosZ, data.PosZ);

      Assert.AreEqual(recData.VelocityX, data.VelocityX);
      Assert.AreEqual(recData.VelocityY, data.VelocityY);
      Assert.AreEqual(recData.VelocityZ, data.VelocityZ);

      Assert.AreEqual(recData.AccelerationX, data.AccelerationX);
      Assert.AreEqual(recData.AccelerationY, data.AccelerationY);
      Assert.AreEqual(recData.AccelerationZ, data.AccelerationZ);

      Assert.AreEqual(recData.Rot0, data.Rot[0]);
      Assert.AreEqual(recData.Rot1, data.Rot[1]);
      Assert.AreEqual(recData.Rot2, data.Rot[2]);
      Assert.AreEqual(recData.Rot3, data.Rot[3]);
      Assert.AreEqual(recData.Rot4, data.Rot[4]);
      Assert.AreEqual(recData.Rot5, data.Rot[5]);
      Assert.AreEqual(recData.Rot6, data.Rot[6]);
      Assert.AreEqual(recData.Rot7, data.Rot[7]);
      Assert.AreEqual(recData.Rot8, data.Rot[8]);

      Assert.AreEqual(recData.Yaw, data.Yaw);
      Assert.AreEqual(recData.Pitch, data.Pitch);
      Assert.AreEqual(recData.Roll, data.Roll);

      Assert.AreEqual(recData.YawVelocity, data.YawVelocity);
      Assert.AreEqual(recData.PitchVelocity, data.PitchVelocity);
      Assert.AreEqual(recData.RollVelocity, data.RollVelocity);

      Assert.AreEqual(recData.SuspNormLengthFront, data.SuspNormLength[0]);
      Assert.AreEqual(recData.SuspNormLengthRear, data.SuspNormLength[1]);

      Assert.AreEqual(recData.Crashed, data.Crashed);
      Assert.AreEqual(recData.Throttle, data.Throttle);
      Assert.AreEqual(recData.FrontBrake, data.FrontBrake);
      Assert.AreEqual(recData.RearBrake, data.RearBrake);
      Assert.AreEqual(recData.Clutch, data.Clutch);

      Assert.AreEqual(recData.WheelSpeedFront, data.WheelSpeed[0]);
      Assert.AreEqual(recData.WheelSpeedRear, data.WheelSpeed[1]);

      Assert.AreEqual(recData.PitLimiter, data.PitLimiter);
      Assert.AreEqual(recData.EngineMapping, data.EngineMapping);
    }

    public static void AssertAreEqual(GPBikes.SPluginsTrackSegment_t recData, GPBikes.SPluginsTrackSegment_t data)
    {
      Assert.AreEqual(recData.Type, data.Type);
      Assert.AreEqual(recData.Length, data.Length);
      Assert.AreEqual(recData.Radius, data.Radius);
      Assert.AreEqual(recData.Angle, data.Angle);
      Assert.AreEqual(recData.Start[0], data.Start[0]);
      Assert.AreEqual(recData.Start[1], data.Start[1]);
    }

    public static void AssertAreEqual(TrackSegment recData, GPBikes.SPluginsTrackSegment_t data)
    {
      Assert.AreEqual(recData.Type, data.Type);
      Assert.AreEqual(recData.Length, data.Length);
      Assert.AreEqual(recData.Radius, data.Radius);
      Assert.AreEqual(recData.Angle, data.Angle);
      Assert.AreEqual(recData.Start1, data.Start[0]);
      Assert.AreEqual(recData.Start2, data.Start[1]);
    }

    public static void AssertAreEqual(BikeData recData, GPBikes.SPluginsBikeDataEx_t data)
    {
      Assert.AreEqual(recData.TrackTime, data.TrackTime);
      Assert.AreEqual(recData.TrackPosition, data.TrackPosition);
      AssertAreEqual(recData, data.BikeData);
    }
  }
}
