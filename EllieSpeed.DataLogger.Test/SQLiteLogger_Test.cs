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
using EllieSpeed.Test.Utilties;
using EllieSpeed.Utilities;
using Moq;
using NUnit.Framework;

namespace EllieSpeed.DataLogger.Test
{
  [TestFixture]
  public class SQLiteLogger_Test
  {
    private string mDataFilePath;
    private Broadcaster mBroadcaster;
    private Mock<IBroadcaster> mMockBroadcaster = new Mock<IBroadcaster>();

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
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnStartup();
        Thread.Sleep(100);

        mMockBroadcaster.Verify(m => m.OnStartup(), Times.Once());
      }
    }

    [Test]
    public void OnShutdown()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnShutdown();
        Thread.Sleep(100);

        mMockBroadcaster.Verify(m => m.OnShutdown(), Times.Once());
      }
    }

    [Test]
    public void OnEventInit()
    {
      var data = TestUtils.CreateBikeEvent();
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnEventInit(data);
        Thread.Sleep(5000);
      }

      mMockBroadcaster.Verify(m => m.OnEventInit(It.IsAny<GPBikes.SPluginsBikeEvent_t>()), Times.Once());
    }

    [Test]
    public void OnRunInit()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        var data = TestUtils.CreateBikeSession();
        mBroadcaster.OnRunInit(data);
        Thread.Sleep(5000);

        mMockBroadcaster.Verify(m => m.OnRunInit(It.IsAny<GPBikes.SPluginsBikeSession_t>()), Times.Once());
      }
    }

    [Test]
    public void OnRunDeinit()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnRunDeinit();
        Thread.Sleep(100);

        mMockBroadcaster.Verify(m => m.OnRunDeinit(), Times.Once());
      }
    }

    [Test]
    public void OnRunStart()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnRunStart();
        Thread.Sleep(100);

        mMockBroadcaster.Verify(m => m.OnRunStart(), Times.Once());
      }
    }

    [Test]
    public void OnRunStop()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        mBroadcaster.OnRunStop();
        Thread.Sleep(100);

        mMockBroadcaster.Verify(m => m.OnRunStop(), Times.Once());
      }
    }

    [Test]
    public void OnRunLap()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        var data = TestUtils.CreateBikeLap();
        mBroadcaster.OnRunLap(data);
        Thread.Sleep(5000);

        mMockBroadcaster.Verify(m => m.OnRunLap(It.IsAny<GPBikes.SPluginsBikeLap_t>()), Times.Once());
      }
    }

    [Test]
    public void OnRunSplit()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        var data = TestUtils.CreateBikeSplit();
        mBroadcaster.OnRunSplit(data);
        Thread.Sleep(5000);

        mMockBroadcaster.Verify(m => m.OnRunSplit(It.IsAny<GPBikes.SPluginsBikeSplit_t>()), Times.Once());
      }
    }

    [Test]
    public void OnRunTelemetry()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        var data = TestUtils.CreateBikeData();
        mBroadcaster.OnRunTelemetry(data);
        Thread.Sleep(500);

        mMockBroadcaster.Verify(m => m.OnRunTelemetry(It.IsAny<GPBikes.SPluginsBikeData_t>()), Times.Once());
      }
    }

    [Test]
    public void OnTrackCenterline()
    {
      using (new SQLiteLogger(mDataFilePath, mMockBroadcaster.Object))
      {
        var data = new[]
                      {
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment(),
                        TestUtils.CreateTrackSegment()
                      };
        mBroadcaster.OnTrackCenterline(data);
        Thread.Sleep(500);

        mMockBroadcaster.Verify(m => m.OnTrackCenterline(It.IsAny<GPBikes.SPluginsTrackSegment_t []>()), Times.Once());
      }
    }
  }
}
