//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using EllieSpeed.Broadcast;
using EllieSpeed.Test.Utilties;
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
      Utils.SafeDelete(mDataFilePath);
      mBroadcaster = new Broadcaster();
    }

    [TearDown]
    public void TearDown()
    {
      //Utils.SafeDelete(mDataFilePath);
      mBroadcaster.Dispose();
    }

    [TestFixtureTearDown]
    public void FixtureTearDown()
    {
      // force DataLogger to release lock on SQLite file
      GC.Collect();

      Utils.SafeDelete(mDataFilePath);
    }

    private string ConnectionString
    {
      get
      {
        return string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                            "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", mDataFilePath);
      }
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
        Thread.Sleep(100);
      }
    }

    [Test]
    public void OnShutdown()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnShutdown();
        Thread.Sleep(100);
      }
    }

    [Test]
    public void OnEventInit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = TestUtils.CreateBikeEvent();
        mBroadcaster.OnEventInit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeEvents.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunInit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = TestUtils.CreateBikeSession();
        mBroadcaster.OnRunInit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeSessions.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunDeinit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunDeinit();
        Thread.Sleep(100);
      }
    }

    [Test]
    public void OnRunStart()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunStart();
        Thread.Sleep(100);
      }
    }

    [Test]
    public void OnRunStop()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunStop();
        Thread.Sleep(100);
      }
    }

    [Test]
    public void OnRunLap()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = TestUtils.CreateBikeLap();
        mBroadcaster.OnRunLap(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeLaps.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunSplit()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = TestUtils.CreateBikeSplit();
        mBroadcaster.OnRunSplit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeSplits.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunTelemetry()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = TestUtils.CreateBikeData();
        mBroadcaster.OnRunTelemetry(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeDatas.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnTrackCenterline()
    {
      using (new SQLiteLogger(mDataFilePath))
      {
        var data = new[]
                      {
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment()
                      };
        mBroadcaster.OnTrackCenterline(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(3, logger.TrackSegments.Count());
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }
  }
}
