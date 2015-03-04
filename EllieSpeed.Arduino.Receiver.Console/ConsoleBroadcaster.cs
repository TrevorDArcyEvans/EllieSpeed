using System;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino.Receiver.Console
{
  internal class ConsoleBroadcaster : ISerialDataBroadcaster, IDisposable
  {
    public void OnSerialData(SerialDataEventArgs data)
    {
      if (string.IsNullOrEmpty(data.Data))
      {
        return;
      }

      System.Console.WriteLine(data.Data);
    }

    public void Dispose()
    {
    }
  }
}