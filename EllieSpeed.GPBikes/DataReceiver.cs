//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using EllieSpeed.Arduino;
using EllieSpeed.Common;

namespace EllieSpeed.GPBikes
{
  public class DataReceiver
  {
    private readonly ArduinoReceiver mReceiver;
    private readonly SControllerData_t mLastData = GetDummyControllerData();
    private readonly object mLock = new object();

    public const int ControllerID = 20060220;

    public DataReceiver()
    {
      mReceiver = new ArduinoReceiver(ArduinoReceiver.ArduinoPort);
      mReceiver.OnSerialData += OnSerialData;
    }

    // public for unit testing
    public void OnSerialData(object sender, SerialDataEventArgs e)
    {
      var AxisOffset = 0;
      var SliderOffset = AxisOffset + mLastData.Axis.Length;
      var ButtonOffset = SliderOffset + mLastData.Slider.Length;
      var POVOffset = ButtonOffset + mLastData.Button.Length;
      var DialOffset = POVOffset + mLastData.POV.Length;

      lock (mLock)
      {
        var data = e.Data.Split(new[] { ArduinoReceiver.RS, ArduinoReceiver.ETX }, StringSplitOptions.RemoveEmptyEntries);
        if (data.Length != mLastData.Axis.Length + mLastData.Slider.Length + mLastData.Button.Length + mLastData.POV.Length + mLastData.Dial.Length)
        {
          // incomplete data read
          return;
        }

        // crack data and convert to input
        // Axis x6
        for (var i = 0; i < mLastData.Axis.Length; i++)
        {
          mLastData.Axis[i] = short.Parse(data[AxisOffset + i]);
        }

        // Slider x6
        for (var i = 0; i < mLastData.Slider.Length; i++)
        {
          mLastData.Slider[i] = short.Parse(data[SliderOffset + i]);
        }

        // Button x32
        for (var i = 0; i < mLastData.Button.Length; i++)
        {
          mLastData.Button[i] = ToByte(int.Parse(data[ButtonOffset + i]));
        }

        // POV x2
        for (var i = 0; i < mLastData.POV.Length; i++)
        {
          mLastData.POV[i] = ToByte(int.Parse(data[POVOffset + i]));
        }

        // Dial x8
        for (var i = 0; i < mLastData.Dial.Length; i++)
        {
          mLastData.Dial[i] = ToByte(int.Parse(data[DialOffset + i]));
        }
      }
    }

    /* called when software is started. If return value is not 0, the plugin is disabled */
    public int Startup()
    {
      return 0;
    }

    /* called when software is closed */
    public void Shutdown()
    {
      mReceiver.Dispose();
    }

    /* called every rendering frame. This function is optional */
    public void Update()
    {
    }

    /* called when a control is queried */
    public void Reset()
    {
    }

    /* called every few seconds to support hot plugging. The return value is the number of active controllers */
    public int GetNumControllers()
    {
      return 1;
    }

    /* iIndex is the 0 based controller index. psInfo must be filled with controller info */
    public SControllerInfo_t GetControllerInfo(int iIndex)
    {
      if (iIndex != 0)
      {
        throw new ArgumentOutOfRangeException(string.Format("Controller Index = {0}", iIndex));
      }

      return GetDefaultControllerInfo();
    }

    // public for unit testing
    public static SControllerInfo_t GetDefaultControllerInfo()
    {
      var retval = new SControllerInfo_t
      {
        Name = "EllieSpeed Bike Simulator",
        UUID = "EllieSpeed BS001",
        ID = ControllerID,

        // max number of axes = 6
        NumAxis = 1,
        AxisRange = new short[18]
        {
          // min, max and center value of each axis
          0, 1023, 512,
          0, 1023, 512,
          0, 1023, 512,
          0, 1023, 512,
          0, 1023, 512,
          0, 1023, 512
        },

        // max number of sliders = 6
        NumSliders = 0,
        SliderRange = new short[6]
        {
          // max value of each slider
          // Arduino is 10 bit analog input [0,1023]
          1023, 1023, 1023, 1023, 1023, 1023
        },

        // max number of buttons = 32
        NumButtons = 0,

        // max POV = 2
        NumPOV = 0,

        // max number of dials = 8
        NumDials = 0,
        DialRange = new byte[8]
        {
          // max value of dials
          // Arduino is 10 bit analog input [0,1023]
          // but is mapped to [0, 255] in ToByte()
          255, 255, 255, 255, 255, 255, 255, 255
        }
      };
      return retval;
    }

    /* iID is the unique controller ID. psData must be filled with controller data */
    public SControllerData_t GetControllerData(int iID)
    {
      if (iID != ControllerID)
      {
        throw new ArgumentOutOfRangeException(string.Format("Controller ID = {0}", iID));
      }

      lock (mLock)
      {
        return mLastData;
      }
    }

    // public for unit testing
    public static SControllerData_t GetDummyControllerData()
    {
      var retval = new SControllerData_t
      {
        Axis = new short[6]
                    {
                      0, 1, 2, 3, 4, 5
                    },
        Slider = new short[6]
                      {
                        0, 1, 2, 3, 4, 5
                      },
        Button = new byte[32]
                      {
                         0,  1,  2,  3,  4,  5,  6,  7,
                         8,  9, 10, 11, 12, 13, 14, 15,
                        16, 17, 18, 19, 20, 21, 22, 23,
                        24, 25, 26, 27, 28, 29, 30, 31
                      },
        POV = new byte[2]
                    {
                      0, 1
                    },
        Dial = new byte[8]
                    {
                      0, 1, 2, 3, 4, 5, 6, 7
                    },
      };
      return retval;
    }

    public static byte ToByte(int arduinoAnalogRead)
    {
      return (byte)((arduinoAnalogRead - 0) / (1023 - 0) * (255 - 0) + 0);
    }
  }
}
