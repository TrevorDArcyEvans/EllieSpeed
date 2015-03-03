//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO.Ports;
using System.Text;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino
{
  public class ArduinoReceiver : IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly SerialPort mPort;
    private readonly ISerialDataBroadcaster mBroadcaster;

    public ArduinoReceiver(string portName, ISerialDataBroadcaster broadcaster)
    {
      mPort = new SerialPort(portName, 9600)
                    {
                      Encoding = Encoding.Default
                    };
      mPort.DataReceived += Port_DataReceived;
      mPort.Open();
      mBroadcaster = broadcaster;
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      mBroadcaster.OnSerialData(new SerialDataEventArgs(mPort.ReadLine()));
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
