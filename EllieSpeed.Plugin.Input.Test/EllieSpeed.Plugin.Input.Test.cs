//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Runtime.InteropServices;
using EllieSpeed.GPBikes;
using NUnit.Framework;

namespace EllieSpeed.Plugin.Input.Test
{
  [TestFixture]
  public class EllieSpeed_Plugin_Input_Test
  {
    #region Plugin Exports

    /// Return Type: int
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "Version")]
    public static extern int Version();

    /// Return Type: int
    /* called when software is started. If return value is not 0, the plugin is disabled */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "Startup")]
    public static extern int Startup();

    /// Return Type: void
    /* called when software is closed */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "Shutdown")]
    public static extern void Shutdown();

    /// Return Type: void
    /* called every rendering frame. This function is optional */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "Update")]
    public static extern void Update();

    /// Return Type: void
    /* called when a control is queried */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "Reset")]
    public static extern void Reset();

    /// Return Type: int
    /* called every few seconds to support hot plugging. The return value is the number of active controllers */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "GetNumControllers")]
    public static extern int GetNumControllers();

    /// Return Type: int
    /* iIndex is the 0 based controller index. psInfo must be filled with controller info */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "GetControllerInfo", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetControllerInfo(int iIndex, IntPtr psInfo);

    /// Return Type: int
    /* iID is the unique controller ID. psData must be filled with controller data */
    [DllImportAttribute("EllieSpeed.Plugin.Input.dli", EntryPoint = "GetControllerData", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetControllerData(int iID, IntPtr psData);

    #endregion

    [Test]
    public void Version_ReturnsExpected()
    {
      Assert.AreEqual(Version(), 2);
    }

    [Test]
    public void Startup_ReturnsExpected()
    {
      Assert.AreEqual(Startup(), 1);
    }

    [Test]
    public void Shutdown_Completes()
    {
      Startup();

      Shutdown();
    }

    [Test]
    public void Update_Completes()
    {
      Update();
    }

    [Test]
    public void Reset_Completes()
    {
      Reset();
    }

    [Test]
    public void GetNumControllers_ReturnsExpected()
    {
      Assert.AreEqual(GetNumControllers(), 1);
    }

    [Test]
    public void GetControllerInfo_ReturnsExpected()
    {
      var info = new SControllerInfo_t
                      {
                        AxisRange = new short[18],
                        SliderRange = new short[6],
                        DialRange = new byte[8]
                      };
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(info));
      Marshal.StructureToPtr(info, ptr, true);

      GetControllerInfo(0, ptr);

      // TODO   verify
    }

    [Test]
    public void GetControllerData_ReturnsExpected()
    {
      Startup();
      var data = new SControllerData_t
                      {
                        Axis = new short[6],
                        Slider = new short[6],
                        Button = new byte[32],
                        POV = new byte[2],
                        Dial = new byte[8]
                      };
      var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
      Marshal.StructureToPtr(data, ptr, true);

      GetControllerData(DataReceiver.ControllerID, ptr);

      // TODO   verify
    }
  }
}
