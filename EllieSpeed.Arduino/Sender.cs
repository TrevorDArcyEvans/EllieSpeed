//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO.Ports;

namespace EllieSpeed.Arduino
{
  public class ArduinoSender : IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly SerialPort mPort;

    public ArduinoSender(string portName)
    {
      mPort = new SerialPort(portName, 9600);
      mPort.Open();
    }

    public void Send(string msg)
    {
      if (mPort.IsOpen)
      {
        mPort.Write(msg);
      }
    }

    public void Dispose()
    {
      if (Disposed)
      {
        return;
      }

      mPort.Close();
      mPort.Dispose();
      Disposed = true;
    }
  }
}
