//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
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
      try
      {
        mPort.Open();
      }
      catch (IOException)
      {
        // swallow exception if Arduino not present
      }
      mBroadcaster = broadcaster;
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      const string STX = "\x02";
      const string ETX = "\x03";
      const string RS = "$";

      var data = mPort.ReadTo(ETX);
      var dataArray = data.Split(new[] { STX }, StringSplitOptions.RemoveEmptyEntries);
      foreach (var thisData in dataArray)
      {
        mBroadcaster.OnSerialData(new SerialDataEventArgs(thisData));
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
