//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;

namespace EllieSpeed.Receive
{
  public class DataEventArgs<T> : EventArgs
  {
    public T Data { get; private set; }

    public DataEventArgs(T data)
    {
      Data = data;
    }
  }
}
