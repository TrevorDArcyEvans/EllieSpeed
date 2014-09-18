//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Arduino
{
  class Program
  {
    static readonly Mutex ArduinoMutex = new Mutex(true, "{C9FD569F-09AE-4B89-B2DD-01831B279A4C}");

    private static void Main(string[] args)
    {
      if (args.Length != 1 || args[0].Contains(@"?"))
      {
        Usage();
        return;
      }

      if (ArduinoMutex.WaitOne(TimeSpan.Zero, true))
      {
        using (var broadcast = new Broadcaster())
        {
          using (new ArduinoReceiver(args[0], broadcast))
          {
            Console.WriteLine(@"Listening for Arduino data on " + args[0]);
            Console.WriteLine(@"Broadcasting data on port " + Broadcaster.BroadcastPort);
            Console.WriteLine();
            Console.WriteLine(@"Press any key to exit");
            Console.ReadKey();
          }
        }

        ArduinoMutex.ReleaseMutex();
      }
      else
      {
        Console.WriteLine("Only one instance at a time!");
      }
    }

    private static void Usage()
    {
      var assyPath = Assembly.GetExecutingAssembly().Location;
      var exeName = Path.GetFileName(assyPath);
      Console.WriteLine(@"Usage:");
      Console.WriteLine(@"  " + exeName + " [COM port]");
      Console.WriteLine();
      Console.WriteLine(@"  Example:");
      Console.WriteLine(@"    " + exeName + " COM4");
    }
  }
}
