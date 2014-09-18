﻿//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;

namespace EllieSpeed.Broadcast
{
  [Serializable]
  public class SerialDataEventArgs : DataEventArgs<string>
  {
    public SerialDataEventArgs(string msg) :
      base(msg)
    {
    }
  }
}
