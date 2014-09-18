//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.Broadcast
{
  public class SerialDataEventArgs : DataEventArgs<string>
  {
    public SerialDataEventArgs(string msg) :
      base(msg)
    {
    }
  }
}
