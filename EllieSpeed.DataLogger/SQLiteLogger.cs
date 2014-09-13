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

namespace EllieSpeed.DataLogger
{
  public class SQLiteLogger : BaseLogger
  {
    private readonly string mDataFilePath;

    public SQLiteLogger(string filePath) :
      base(new DataLogger(GetConnectionString(filePath)))
    {
      mDataFilePath = filePath;
      if (!File.Exists(mDataFilePath))
      {
        var assy = Assembly.GetExecutingAssembly();
        var strm = assy.GetManifestResourceStream("EllieSpeed.DataLogger.SQLite.sqlite3");

        using (var fileStream = File.Create(mDataFilePath, (int)strm.Length))
        {
          // Initialize the bytes array with the stream length and then fill it with data
          var bytesInStream = new byte[strm.Length];
          strm.Read(bytesInStream, 0, bytesInStream.Length);
          // Use write method to write to the file specified above
          fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        }
      }

      if (Directory.Exists(mDataFilePath))
      {
        throw new ArgumentException(mDataFilePath + " is a directory");
      }
    }

    protected override string ConnectionString
    {
      get
      {
        return GetConnectionString(mDataFilePath);
      }
    }

    public static string GetConnectionString(string filePath)
    {
      return string.Format("metadata=res://*/DataLogger.csdl|res://*/DataLogger.ssdl|res://*/DataLogger.msl;" +
                          "provider=System.Data.SQLite;provider connection string=\"data source={0}\";", filePath);
    }
  }
}
