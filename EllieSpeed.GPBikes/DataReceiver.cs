//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Collections.Generic;
using System.Text;
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
      lock (mLock)
      {
        var data = e.Data.Split(new[] { ArduinoReceiver.RS, ArduinoReceiver.ETX }, StringSplitOptions.RemoveEmptyEntries);

        // TODO   crack data and convert to input
      }
    }

    public int Startup()
    {
      return 1;
    }

    public void Shutdown()
    {
      mReceiver.Dispose();
    }

    public void Update()
    {
    }

    public void Reset()
    {
    }

    public int GetNumControllers()
    {
      return 1;
    }

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
        NumAxis = 0,
        AxisRange = new short[18]
        {
          // min, max and center value of each axis
          0, 0, 0,
          0, 0, 0,
          0, 0, 0,
          0, 0, 0,
          0, 0, 0,
          0, 0, 0
        },

        // max number of sliders = 6
        NumSliders = 1,
        SliderRange = new short[6]
        {
          // max value of each slider
          1024, 0, 0, 0, 0, 0
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
          0, 0, 0, 0, 0, 0, 0, 0
        }
      };
      return retval;
    }

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
  }
}
