//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using EllieSpeed.Broadcast;
using EllieSpeed.Receive;
using EllieSpeed.Test.Utilties;
using NUnit.Framework;

namespace EllieSpeed.Plugin.DataLogger.Test
{
  [TestFixture]
  public class EllieSpeed_Plugin_Test
  {
    #region Plugin Exports

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "GetDataRate")]
    public static extern int GetDataRate();

    /// Return Type: char*
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "GetModID")]
    public static extern IntPtr GetModID();

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "GetModDataVersion")]
    public static extern int GetModDataVersion();

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "GetInterfaceVersion")]
    public static extern int GetInterfaceVersion();

    /// Return Type: int
    /// szSavePath: char*
    /* called when software is started */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "Startup")]
    public static extern int Startup(IntPtr szSavePath);

    /// Return Type: void
    /* called when software is closed */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "Shutdown")]
    public static extern void Shutdown();

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when event is initialized */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "EventInit")]
    public static extern void EventInit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when bike goes to track */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunInit")]
    public static extern void RunInit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /* called when bike leaves the track */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunDeinit")]
    public static extern void RunDeinit();

    /// Return Type: void
    /* called when simulation is started / resumed */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunStart")]
    public static extern void RunStart();

    /// Return Type: void
    /* called when simulation is paused */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunStop")]
    public static extern void RunStop();

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when a new lap is recorded. This function is optional */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunLap")]
    public static extern void RunLap(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /* called when a split is crossed. This function is optional */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunSplit")]
    public static extern void RunSplit(IntPtr pData, int iDataSize);

    /// Return Type: void
    /// pData: void*
    /// iDataSize: int
    /// fTime: float
    /// fPos: float
    /* fTime is the ontrack time, in seconds. fPos is the position on centerline, from 0 to 1 */
    [DllImportAttribute("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "RunTelemetry")]
    public static extern void RunTelemetry(IntPtr pData, int iDataSize, float fTime, float fPos);

    /// Return Type: void
    /// iNumSegments: int
    /// pasSegment: SPluginsTrackSegment_t*
    /// pRaceData: void*
    /* This function is optional */
    [DllImport("EllieSpeed.Plugin.DataLogger.dlo", EntryPoint = "TrackCenterline")]
    public static extern void TrackCenterline(int iNumSegments, [MarshalAs(UnmanagedType.LPArray)] GPBikes.SPluginsTrackSegment_t[] pasSegment, IntPtr pRaceData);

    #endregion

    private readonly int BroadcastPort = Broadcaster.BroadcastPort;

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
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        rec.OnStartup += (sender, args) => { msgReceived = true; };

        int retVal = Startup(Marshal.StringToHGlobalAnsi(@"dummy.txt"));

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
      using (var rec = new BikeDataReceiver(BroadcastPort))
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
      var data = TestUtils.CreateBikeEvent();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsBikeEvent_t();
        rec.OnEventInit += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        EventInit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        TestUtils.AssertAreEqual(recData, data);
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunInit_Completes()
    {
      var data = TestUtils.CreateBikeSession();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsBikeSession_t();
        rec.OnRunInit += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        RunInit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        TestUtils.AssertAreEqual(recData, data);
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunDeinit_Completes()
    {
      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
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
      using (var rec = new BikeDataReceiver(BroadcastPort))
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
      using (var rec = new BikeDataReceiver(BroadcastPort))
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
      var data = TestUtils.CreateBikeLap();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsBikeLap_t();
        rec.OnRunLap += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        RunLap(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        TestUtils.AssertAreEqual(recData, data);
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunSplit_Completes()
    {
      var data = TestUtils.CreateBikeSplit();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsBikeSplit_t();
        rec.OnRunSplit += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        RunSplit(ptr, default(int));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        TestUtils.AssertAreEqual(recData, data);
      }
    }

    [Test]
    [Timeout(5000)]
    public void RunTelemetry_Completes()
    {
      var data = TestUtils.CreateBikeData();

      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsBikeDataEx_t();
        rec.OnRunTelemetry += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        RunTelemetry(ptr, default(int), default(float), default(float));

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        TestUtils.AssertAreEqual(recData.BikeData, data);
      }
    }

    [Test]
    [Timeout(5000)]
    public void TrackCenterline_Completes()
    {
      const int NumTrackSegs = 10;

      var data = new List<GPBikes.SPluginsTrackSegment_t>(NumTrackSegs);

      for (var i = 0; i < NumTrackSegs; i++)
      {
        var currData = TestUtils.CreateTrackSegment();
        data.Add(currData);
      }

      var msgReceived = false;
      using (var rec = new BikeDataReceiver(BroadcastPort))
      {
        var recData = new GPBikes.SPluginsTrackSegment_t[2];
        rec.OnTrackCenterline += (sender, args) =>
        {
          msgReceived = true;
          recData = args.Data;
        };

        TrackCenterline(data.Count, data.ToArray(), IntPtr.Zero);

        while (!msgReceived)
        {
          Thread.Sleep(100);
        }

        for (var i = 0; i < NumTrackSegs; i++)
        {
          TestUtils.AssertAreEqual(recData[i], data[i]);
        }
      }
    }
  }
}
