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
using EllieSpeed.Interfaces;
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
    public void Startup_ReturnsExpected()
    {
      var retVal = Startup(Marshal.StringToHGlobalAnsi(@"dummy.txt"));

      Assert.AreEqual(3, retVal);
    }

    [Test]
    public void Shutdown_Completes()
    {
      Shutdown();
    }

    [Test]
    public void EventInit_Completes()
    {
      var data = new GPBikes.SPluginsBikeEvent_t();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      EventInit(ptr, default(int));
    }

    [Test]
    public void RunInit_Completes()
    {
      var data = new GPBikes.SPluginsBikeSession_t();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      RunInit(ptr, default(int));
    }

    [Test]
    public void RunDeinit_Completes()
    {
      RunDeinit();
    }

    [Test]
    public void RunStart_Completes()
    {
      RunStart();
    }

    [Test]
    public void RunStop_Completes()
    {
      RunStop();
    }

    [Test]
    public void RunLap_Completes()
    {
      var data = new GPBikes.SPluginsBikeLap_t();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      RunLap(ptr, default(int));
    }

    [Test]
    public void RunSplit_Completes()
    {
      var data = new GPBikes.SPluginsBikeSplit_t();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      RunSplit(ptr, default(int));
    }

    [Test]
    public void RunTelemetry_Completes()
    {
      var data = new GPBikes.SPluginsBikeData_t();
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      RunTelemetry(ptr, default(int), default(float), default(float));
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

      TrackCenterline(data.Count, data.ToArray(), IntPtr.Zero);
    }
  }
}
