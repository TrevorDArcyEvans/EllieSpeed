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
using EllieSpeed.Utilities;
using NUnit.Framework;

namespace EllieSpeed.DataLogger.Test
{
  [TestFixture]
  public class SQLiteLogger_Test
  {
    [Test]
    public void Constructor_Completes()
    {
      var assyPath = Assembly.GetExecutingAssembly().Location;
      var assyDir = Path.GetDirectoryName(assyPath);
      var dataFilePath = Path.Combine(assyDir, "DelMe.sqlite3");
      Utils.SafeDelete(dataFilePath);

      try
      {
        var logger = new SQLiteLogger(dataFilePath);

        Assert.IsNotNull(logger);
      }
      finally
      {
        Utils.SafeDelete(dataFilePath);
      }
    }
  }
}
