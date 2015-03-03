//
//  Copyright (C) 2015 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino.Receiver.Console
{
  class Program
  {
    static readonly Mutex ArduinoMutex = new Mutex(true, "{D79F57F2-CDEF-4CB2-A25F-DC7BF0CBAE3F}");

    private static void Main(string[] args)
    {
      if (args.Length != 1 || args[0].Contains(@"?"))
      {
        Usage();
        return;
      }

      if (ArduinoMutex.WaitOne(TimeSpan.Zero, true))
      {
        using (var broadcast = new ConsoleBroadcaster())
        {
          using (new ArduinoReceiver(args[0], broadcast))
          {
            System.Console.WriteLine(@"Listening for Arduino data on " + args[0]);
            System.Console.WriteLine();
            System.Console.WriteLine(@"Press any key to exit");
            System.Console.ReadKey();
          }
        }

        ArduinoMutex.ReleaseMutex();
      }
      else
      {
        System.Console.WriteLine("Only one instance at a time!");
      }
    }

    private static void Usage()
    {
      var assyPath = Assembly.GetExecutingAssembly().Location;
      var exeName = Path.GetFileName(assyPath);
      System.Console.WriteLine(@"Usage:");
      System.Console.WriteLine(@"  " + exeName + " [COM port]");
      System.Console.WriteLine();
      System.Console.WriteLine(@"  Example:");
      System.Console.WriteLine(@"    " + exeName + " COM4");
    }
  }

  internal class ConsoleBroadcaster : ISerialDataBroadcaster, IDisposable
  {
    private int mLastValue = -1;

    public void OnSerialData(SerialDataEventArgs data)
    {
      if (string.IsNullOrEmpty(data.Data))
      {
        return;
      }

      var value = int.Parse(data.Data);
      if (Math.Abs(mLastValue - value) > 2)
      {
        System.Console.WriteLine(@"{0:0000}", value);
        System.Console.Out.Flush();
        //Debug.WriteLine(@"{0:0000}", value);
        mLastValue = value;
      }
    }

    public void Dispose()
    {
    }
  }
}
