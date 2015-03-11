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
//using EllieSpeed.Arduino;

namespace EllieSpeed.GPBikes
{
  public class DataReceiver
  {
    //private readonly ArduinoReceiver mReceiver;

    public DataReceiver()
    {
    //EllieSpeed::Arduino::ArduinoReceiver::ArduinoPort
      
    }

    public int Startup() 
    {
      return 1;
    }

    public void Shutdown()
    {
      
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

    public int GetControllerInfo(int iIndex, SControllerInfo_t psInfo)
    {
      return 0;
    }

    public int GetControllerData(int iID, SControllerData_t psData)
    {
      return 0;
    }
  }
}
