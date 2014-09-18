//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO.Ports;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino
{
  public class Receiver : IDisposable
  {
    public event EventHandler<SerialDataEventArgs> OnDataReceived;
    public bool Disposed { get; private set; }

    private readonly SerialPort mPort;

    public Receiver(string portName)
    {
      mPort = new SerialPort(portName, 9600);
      mPort.DataReceived += Port_DataReceived;
      mPort.Open();
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      if (OnDataReceived != null && mPort.IsOpen)
      {
        OnDataReceived(this, new SerialDataEventArgs(mPort.ReadExisting()));
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
