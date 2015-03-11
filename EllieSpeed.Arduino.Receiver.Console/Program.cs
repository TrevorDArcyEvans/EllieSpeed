//
//  Copyright (C) 2015 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System.IO;
using System.Reflection;

namespace EllieSpeed.Arduino.Receiver.Console
{
  class Program
  {
    private static void Main(string[] args)
    {
      if (args.Length != 1 || args[0].Contains(@"?"))
      {
        Usage();
        return;
      }

      using (var rec = new ArduinoReceiver(args[0]))
      {
        rec.OnSerialData += OnSerialData;

        System.Console.WriteLine(@"Listening for Arduino data on " + args[0]);
        System.Console.WriteLine();
        System.Console.WriteLine(@"Press any key to exit");
        System.Console.ReadKey();
      }
    }

    private static void OnSerialData(object sender, Broadcast.SerialDataEventArgs e)
    {
      System.Console.WriteLine(e.Data);
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
}
