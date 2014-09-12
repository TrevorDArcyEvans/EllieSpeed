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

namespace EllieSpeed.DataLogger.SQLite
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 1 || args[0].Contains(@"?"))
      {
        Usage();
        return;
      }

      using (new SQLiteLogger(args[0]))
      {
        Console.WriteLine(@"Listening for data on port " + Broadcast.Broadcaster.BroadcastPort);
        Console.WriteLine(@"Logging data to " + args[0]);
        Console.WriteLine();
        Console.WriteLine(@"Press any key to exit");
        Console.ReadKey();
      }
    }

    private static void Usage()
    {
      var assyPath = Assembly.GetExecutingAssembly().Location;
      var exeName = Path.GetFileName(assyPath);
      Console.WriteLine(@"Usage:");
      Console.WriteLine(@"  " + exeName + " [database file name]");
      Console.WriteLine();
      Console.WriteLine(@"  Example:");
      Console.WriteLine(@"    " + exeName + " MyDataFile.sqlite3");
    }
  }
}
