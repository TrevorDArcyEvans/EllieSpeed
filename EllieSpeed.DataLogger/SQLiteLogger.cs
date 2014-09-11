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

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger : BaseLogger
  {
    private readonly string mDataFilePath;

    public SQLiteLogger(string filePath)
    {
      mDataFilePath = filePath;
      if (!File.Exists(mDataFilePath))
      {
        var assyPath = Assembly.GetExecutingAssembly().Location;
        var assyDir = Path.GetDirectoryName(assyPath);
        var baseDataFilePath = Path.Combine(assyDir, "EllieSpeed.DataLogger.sqlite3");
        File.Copy(baseDataFilePath, mDataFilePath);
      }

      if (Directory.Exists(mDataFilePath))
      {
        throw new ArgumentException(mDataFilePath + " is a directory");
      }

      Initialise();
    }

    protected override string ConnectionString
    {
      get
      {
        return string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                            "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", mDataFilePath);
      }
    }
  }
}
