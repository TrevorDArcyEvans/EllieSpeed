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
using System.Reflection;
using System.Text;
using System.Threading;
using EllieSpeed.Broadcast;
using EllieSpeed.Utilities;
using NUnit.Framework;

namespace EllieSpeed.DataLogger.Test
{
  [TestFixture]
  public class SQLiteLogger_Test
  {
    private string mDataFilePath;
    private Broadcaster mBroadcaster;

    [SetUp]
    public void Setup()
    {
      var assyPath = Assembly.GetExecutingAssembly().Location;
      var assyDir = Path.GetDirectoryName(assyPath);
      Utils.SafeDelete(mDataFilePath);

      mDataFilePath = Path.Combine(assyDir, "DelMe.sqlite3");
      mBroadcaster = new Broadcaster();
    }

    [TearDown]
    public void TearDown()
    {
      Utils.SafeDelete(mDataFilePath);
      mBroadcaster.Dispose();
    }

    [Test]
    public void Constructor_Completes()
    {
      using (var logger = new SQLiteLogger(mDataFilePath))
      {
        Assert.IsNotNull(logger);
      }
    }

    [Test]
    public void OnStartup()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnStartup();
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnShutdown()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnShutdown();
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnEventInit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnEventInit(new GPBikes.SPluginsBikeEvent_t());
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunInit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunInit(new GPBikes.SPluginsBikeSession_t());
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunDeinit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunDeinit();
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunStart()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunStart();
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunStop()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunStop();
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunLap()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunLap(new GPBikes.SPluginsBikeLap_t());
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunSplit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunSplit(new GPBikes.SPluginsBikeSplit_t());
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnRunTelemetry()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunTelemetry(new GPBikes.SPluginsBikeData_t());
        Thread.Sleep(500);
      }
    }

    [Test]
    public void OnTrackCenterline()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnTrackCenterline(new GPBikes.SPluginsTrackSegment_t[2]);
        Thread.Sleep(500);
      }
    }
  }
}
