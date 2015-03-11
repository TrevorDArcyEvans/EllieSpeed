//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino
{
  public class ArduinoReceiver : IDisposable
  {
    public const string STX = "\x02";
    public const string ETX = "\x03";
    public const string RS = "$";

    public event EventHandler<SerialDataEventArgs> OnSerialData;

    public bool Disposed { get; private set; }

    private const string ArduinoMutexRoot = "{D79F57F2-CDEF-4CB2-A25F-DC7BF0CBAE3F}";

    private readonly SerialPort mPort;
    private readonly Mutex mArduinoMutex;

    public ArduinoReceiver(string portName)
    {
      mArduinoMutex = new Mutex(true, ArduinoMutexRoot + portName);
      if (mArduinoMutex.WaitOne(TimeSpan.Zero, true))
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
      }
      else
      {
        throw new ArgumentException("Instance already running on: {0}", portName);
      }
    }

    private ArduinoReceiver()
    {
    }

    public static string ArduinoPort
    {
      get
      {
        var cfg = ConfigurationManager.OpenExeConfiguration(new ArduinoReceiver().GetType().Assembly.Location);
        var appSettings = (AppSettingsSection)cfg.GetSection("appSettings");

        return appSettings.Settings["ArduinoPort"].Value;
      }
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      if (OnSerialData == null)
      {
        return;
      }

      var data = mPort.ReadTo(ETX);
      var dataArray = data.Split(new[] { STX }, StringSplitOptions.RemoveEmptyEntries);
      foreach (var thisData in dataArray)
      {
        OnSerialData(this, new SerialDataEventArgs(thisData));
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
      mArduinoMutex.ReleaseMutex();

      Disposed = true;
    }
  }
}
