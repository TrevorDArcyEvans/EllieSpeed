//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.Receive.Console
{
  class Program
  {
    static void Main()
    {
      using (var rec = new BikeDataReceiver(Broadcast.Broadcaster.BroadcastPort))
      {
        rec.OnStartup += (s, e) => System.Console.WriteLine("OnStartup");
        rec.OnShutdown += (s, e) => System.Console.WriteLine("OnShutdown");
        rec.OnEventInit += (s, e) => System.Console.WriteLine("OnEventInit");
        rec.OnRunInit += (s, e) => System.Console.WriteLine("OnRunInit");
        rec.OnRunDeinit += (s, e) => System.Console.WriteLine("OnRunDeinit");
        rec.OnRunStart += (s, e) => System.Console.WriteLine("OnRunStart");
        rec.OnRunStop += (s, e) => System.Console.WriteLine("OnRunStop");
        rec.OnRunLap += (s, e) => System.Console.WriteLine("OnRunLap");
        rec.OnRunSplit += (s, e) => System.Console.WriteLine("OnRunSplit");
        rec.OnRunTelemetry += (s, e) => System.Console.WriteLine("OnRunTelemetry");
        rec.OnTrackCenterline += (s, e) => System.Console.WriteLine("OnTrackCenterline");

        System.Console.WriteLine(@"Listening for data on port " + Broadcast.Broadcaster.BroadcastPort);
        System.Console.WriteLine();
        System.Console.WriteLine(@"Press any key to exit");
        System.Console.ReadKey();
      }
    }
  }
}
