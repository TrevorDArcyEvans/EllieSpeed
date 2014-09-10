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

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger
  {
    private readonly DataLogger mLogger;

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
    }
  }
}
