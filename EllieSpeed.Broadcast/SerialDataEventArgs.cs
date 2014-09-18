//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.Receive
{
  public class SerialDataEventArgs : DataEventArgs<string>
  {
    public SerialDataEventArgs(string msg) :
      base(msg)
    {
    }
  }
}
