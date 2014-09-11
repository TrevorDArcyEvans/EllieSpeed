//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.IO;
using System.Reflection;
using EllieSpeed.Broadcast;

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger : BaseLogger
  {
    private readonly string mDatabaseFilePath;

    public SQLiteLogger(string filePath)
    {
      mDatabaseFilePath = filePath;
      if (!File.Exists(mDatabaseFilePath))
      {
        var assyPath = Assembly.GetExecutingAssembly().Location;
        var assyDir = Path.GetDirectoryName(assyPath);
        var baseDataFilePath = Path.Combine(assyDir, "EllieSpeed.DataLogger.sqlite3");
        File.Copy(baseDataFilePath, mDatabaseFilePath);
      }

      if (Directory.Exists(mDatabaseFilePath))
      {
        throw new ArgumentException(mDatabaseFilePath + " is a directory");
      }

      Initialise();
    }

    internal SQLiteLogger(string filePath, IBroadcaster broadcaster) :
      this(filePath)
    {
      mRecLog = broadcaster;
    }

    protected override sealed string ConnectionString
    {
      get
      {
        return string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                            "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", mDatabaseFilePath);
      }
    }
  }
}
