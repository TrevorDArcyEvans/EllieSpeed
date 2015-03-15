//
//  Copyright (C) 2015 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System.IO;
using System.Reflection;
using EllieSpeed.Common;

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

      using (var msg = new Messenger(args[0]))
      {
        msg.OnSerialData += OnSerialData;

        System.Console.WriteLine(@"Listening for Arduino data on " + args[0]);
        System.Console.WriteLine();
        System.Console.WriteLine(@"Press any key to exit");
        System.Console.ReadKey();
      }
    }

    private static void OnSerialData(object sender, SerialDataEventArgs e)
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
