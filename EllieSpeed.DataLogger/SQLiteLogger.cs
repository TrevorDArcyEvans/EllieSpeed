//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using EllieSpeed.Broadcast;
using EllieSpeed.Receive;

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger : IDisposable
  {
    private readonly DataLogger mLogger;
    private readonly Receiver mReceiver = new Receiver(Broadcaster.BroadcastPort);
    private readonly IBroadcaster mRecLog;

    public SQLiteLogger(string filePath)
    {
      if (File.Exists(filePath))
      {
        throw new ArgumentException(filePath + " already exists");
      }

      if (Directory.Exists(filePath))
      {
        throw new ArgumentException(filePath + " is a directory");
      }

      var assyPath = Assembly.GetExecutingAssembly().Location;
      var assyDir = Path.GetDirectoryName(assyPath);
      var baseDataFilePath = Path.Combine(assyDir, "EllieSpeed.DataLogger.sqlite3");
      File.Copy(baseDataFilePath, filePath);

      var connStr = string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                          "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", filePath);

      mLogger = new DataLogger(connStr);

      mReceiver.OnStartup += OnStartup;
      mReceiver.OnShutdown += OnShutdown;
      mReceiver.OnEventInit += OnEventInit;
      mReceiver.OnRunInit += OnRunInit;
      mReceiver.OnRunDeinit += OnRunDeinit;
      mReceiver.OnRunStart += OnRunStart;
      mReceiver.OnRunStop += OnRunStop;
      mReceiver.OnRunLap += OnRunLap;
      mReceiver.OnRunSplit += OnRunSplit;
      mReceiver.OnRunTelemetry += OnRunTelemetry;
      mReceiver.OnTrackCenterline += OnTrackCenterline;
    }

    internal SQLiteLogger(string filePath, IBroadcaster broadcaster) :
      this(filePath)
    {
      mRecLog = broadcaster;
    }

    public void OnStartup(object sender, EventArgs e)
    {
      Console.WriteLine("OnStartup");
      mRecLog.OnStartup();
    }

    void OnShutdown(object sender, EventArgs e)
    {
      Console.WriteLine("OnShutdown");
    }

    void OnEventInit(object sender, DataEventArgs<GPBikes.SPluginsBikeEvent_t> e)
    {
      Console.WriteLine("OnEventInit");
    }

    void OnRunInit(object sender, DataEventArgs<GPBikes.SPluginsBikeSession_t> e)
    {
      Console.WriteLine("OnRunInit");
    }

    void OnRunDeinit(object sender, EventArgs e)
    {
      Console.WriteLine("OnRunDeinit");
    }

    void OnRunStart(object sender, EventArgs e)
    {
      Console.WriteLine("OnRunStart");
    }

    void OnRunStop(object sender, EventArgs e)
    {
      Console.WriteLine("OnRunStop");
    }

    void OnRunLap(object sender, DataEventArgs<GPBikes.SPluginsBikeLap_t> e)
    {
      Console.WriteLine("OnRunLap");
    }

    void OnRunSplit(object sender, DataEventArgs<GPBikes.SPluginsBikeSplit_t> e)
    {
      Console.WriteLine("OnRunSplit");
    }

    void OnRunTelemetry(object sender, DataEventArgs<GPBikes.SPluginsBikeData_t> e)
    {
      Console.WriteLine("OnRunTelemetry");
    }

    void OnTrackCenterline(object sender, DataEventArgs<GPBikes.SPluginsTrackSegment_t[]> e)
    {
      Console.WriteLine("OnTrackCenterline");
    }

    #region IDisposable Members

    public void Dispose()
    {
      mLogger.Dispose();
      mReceiver.Dispose();
    }

    #endregion
  }
}
