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

    [OneTimeTearDown]
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
        return SQLiteLogger.GetConnectionString(mDataFilePath);
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
      var data = TestUtils.CreateBikeEvent();
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnEventInit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeEvents.Count());

        var recData = logger.BikeEvents.Single();
        TestUtils.AssertAreEqual(recData, data);
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunInit()
    {
      var data = TestUtils.CreateBikeSession();
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunInit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeSessions.Count());

        var recData = logger.BikeSessions.Single();
        TestUtils.AssertAreEqual(recData, data);
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
      var data = TestUtils.CreateBikeLap();
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunLap(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeLaps.Count());

        var recData = logger.BikeLaps.Single();
        TestUtils.AssertAreEqual(recData, data);
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunSplit()
    {
      var data = TestUtils.CreateBikeSplit();
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunSplit(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeSplits.Count());

        var recData = logger.BikeSplits.Single();
        TestUtils.AssertAreEqual(recData, data);
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnRunTelemetry()
    {
      var data = TestUtils.CreateBikeDataEx();
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnRunTelemetry(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(1, logger.BikeDatas.Count());

        var recData = logger.BikeDatas.Single();
        TestUtils.AssertAreEqual(recData, data);
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }

    [Test]
    public void OnTrackCenterline()
    {
      var data = new[]
                      {
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment()
                      };
      using (new SQLiteLogger(mDataFilePath))
      {
        mBroadcaster.OnTrackCenterline(data);
        Thread.Sleep(5000);
      }

      using (var logger = new DataLogger(ConnectionString))
      {
        Assert.AreEqual(data.Length, logger.TrackSegments.Count());

        var recData = logger.TrackSegments.ToList();
        for (var i = 0; i < data.Length; i++)
        {
          TestUtils.AssertAreEqual(recData[i], data[i]);
        }
      }

      // force DataLogger to release lock on SQLite file
      GC.Collect();
    }
  }
}
