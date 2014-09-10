//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace EllieSpeed.DataLogger.Test
{
  [TestFixture]
  public class SQLiteLogger_Test
  {
    [Test]
    public void Constructor_Completes()
    {
      var retVal = new SQLiteLogger("aaa");
    }

  }
}
