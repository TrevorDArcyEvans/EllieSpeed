//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using EllieSpeed.Interfaces;
using EllieSpeed.Receive;
using NUnit.Framework;

namespace EllieSpeed.Plugin.Test
{
  [TestFixture]
  public class EllieSpeed_Plugin_Test
  {
    #region Plugin Exports

    /// Return Type: char*
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "GetModID")]
    public static extern IntPtr GetModID();

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "GetModDataVersion")]
    public static extern int GetModDataVersion();

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "GetInterfaceVersion")]
    public static extern int GetInterfaceVersion();

    /// Return Type: int
    /// szSavePath: char*
    /* called when software is started */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "Startup")]
    public static extern int Startup(IntPtr szSavePath);

    /// Return Type: void
    /* called when software is closed */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "Shutdown")]
    public static extern void Shutdown();

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when event is initialized */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "EventInit")]
    public static extern void EventInit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when bike goes to track */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunInit")]
    public static extern void RunInit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /* called when bike leaves the track */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunDeinit")]
    public static extern void RunDeinit();

    /// Return Type: void
    /* called when simulation is started / resumed */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunStart")]
    public static extern void RunStart();

    /// Return Type: void
    /* called when simulation is paused */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunStop")]
    public static extern void RunStop();

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when a new lap is recorded. This function is optional */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunLap")]
    public static extern void RunLap(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when a split is crossed. This function is optional */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunSplit")]
    public static extern void RunSplit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /// fTime: float
    /// fPos: float
    /* fTime is the ontrack time, in seconds. fPos is the position on centerline, from 0 to 1 */
    [DllImportAttribute("EllieSpeed.Plugin.dlo", EntryPoint = "RunTelemetry")]
    public static extern void RunTelemetry(IntPtr pData, int iDataSize, float fTime, float fPos);

    /// Return Type: void
    /// iNumSegments: int
    /// pasSegment: SPluginsTrackSegment_t*
    /// pRaceData: void*
    /* This function is optional */
    [DllImport("EllieSpeed.Plugin.dlo", EntryPoint = "TrackCenterline")]
    public static extern void TrackCenterline(int iNumSegments, [MarshalAs(UnmanagedType.LPArray)] GPBikes.SPluginsTrackSegment_t[] pasSegment, IntPtr pRaceData);

    #endregion

    private readonly int BroadcastPort = Broadcast.Broadcast.Default.BroadcastPort;

    [Test]
    public void GetModID_ReturnsExpected()
    {
      var retVal = GetModID();

      Assert.AreEqual("gpbikes", Marshal.PtrToStringAnsi(retVal));
    }

    [Test]
    public void GetModDataVersion_ReturnsExpected()
    {
      var retVal = GetModDataVersion();

      Assert.AreEqual(2, retVal);
    }

    [Test]
    public void GetInterfaceVersion_ReturnsExpected()
    {
      var retVal = GetInterfaceVersion();

      Assert.AreEqual(8, retVal);
    }

    [Test]
    [Timeout(5000)]
    public void Startup_ReturnsExpected()
    {
      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnStartup += (sender, args) => { msgReceived = true; };

        var retVal = Startup(Marshal.StringToHGlobalAnsi(@"dummy.txt"));

        Assert.AreEqual(3, retVal);

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void Shutdown_Completes()
    {
      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnShutdown += (sender, args) => { msgReceived = true; };

        Shutdown();

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void EventInit_Completes()
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
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnEventInit += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

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
        };

        EventInit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunInit_Completes()
    {
      var data = new GPBikes.SPluginsBikeSession_t
      {
        Session = 4,
        Conditions = 2,
        AirTemperature = 21,
        TrackTemperature = 30,
        SetupFileName = "RainTyres"
      };
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunInit += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

          Assert.AreEqual(recData.Session, data.Session);
          Assert.AreEqual(recData.Conditions, data.Conditions);
          Assert.AreEqual(recData.AirTemperature, data.AirTemperature);
          Assert.AreEqual(recData.TrackTemperature, data.TrackTemperature);
          Assert.AreEqual(recData.SetupFileName, data.SetupFileName);
        };

        RunInit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunDeinit_Completes()
    {
      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunDeinit += (sender, args) => { msgReceived = true; };

        RunDeinit();

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunStart_Completes()
    {
      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunStart += (sender, args) => { msgReceived = true; };

        RunStart();

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunStop_Completes()
    {
      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunStop += (sender, args) => { msgReceived = true; };

        RunStop();

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunLap_Completes()
    {
      var data = new GPBikes.SPluginsBikeLap_t
      {
        LapTime = 66000,
        Best = 1,
        LapNum = 6
      };
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunLap += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

          Assert.AreEqual(recData.LapTime, data.LapTime);
          Assert.AreEqual(recData.Best, data.Best);
          Assert.AreEqual(recData.LapNum, data.LapNum);
        };

        RunLap(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunSplit_Completes()
    {
      var data = new GPBikes.SPluginsBikeSplit_t
      {
        Split = 3,
        SplitTime = 66000,
        BestDiff = 100
      };
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunSplit += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

          Assert.AreEqual(recData.Split, data.Split);
          Assert.AreEqual(recData.SplitTime, data.SplitTime);
          Assert.AreEqual(recData.BestDiff, data.BestDiff);
        };

        RunSplit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunTelemetry_Completes()
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

      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnRunTelemetry += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

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
        };

        RunTelemetry(ptr, default(int), default(float), default(float));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }

    // cannot have a default constructor for a struct so have this factory
    // so we initialise Start array
    private static GPBikes.SPluginsTrackSegment_t CreatePluginsTrackSegment()
    {
      return new GPBikes.SPluginsTrackSegment_t
                  {
                    Start = new float[2]
                  };
    }

    private void Randomise(ref GPBikes.SPluginsTrackSegment_t seg, int seq)
    {
      seg.Angle = seq * 36f;
      seg.Length = seq * 50f;
      seg.Radius = seq * 10f;
      seg.Type = seq * 1;
      seg.Start[0] = seq * 10f;
      seg.Start[1] = seq * 10f;
    }

    [Test]
    [Timeout(5000)]
    public void TrackCenterline_Completes()
    {
      const int NumTrackSegs = 10;

      var data = new List<GPBikes.SPluginsTrackSegment_t>(NumTrackSegs);

      for (var i = 0; i < NumTrackSegs; i++)
      {
        var currData = CreatePluginsTrackSegment();
        Randomise(ref currData, i);
        data.Add(currData);
      }

      var msgReceived = false;
      using (var rec = new Receiver(BroadcastPort))
      {
        rec.OnTrackCenterline += (sender, args) =>
        {
          msgReceived = true;
          var recData = args.Data;

          for (var i = 0; i < NumTrackSegs; i++)
          {
            Assert.AreEqual(recData[i].Type, data[i].Type);
            Assert.AreEqual(recData[i].Length, data[i].Length);
            Assert.AreEqual(recData[i].Radius, data[i].Radius);
            Assert.AreEqual(recData[i].Angle, data[i].Angle);
            Assert.AreEqual(recData[i].Start1, data[i].Start[0]);
            Assert.AreEqual(recData[i].Start2, data[i].Start[1]);
          }
        };

        TrackCenterline(data.Count, data.ToArray(), IntPtr.Zero);

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }
      }
    }
  }
}
