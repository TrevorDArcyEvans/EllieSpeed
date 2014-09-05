//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
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
