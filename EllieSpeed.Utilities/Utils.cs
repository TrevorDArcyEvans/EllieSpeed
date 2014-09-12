//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System.IO;

namespace EllieSpeed.Utilities
{
  public class Utils
  {
    public static void SafeDelete(string filePath)
    {
      if (File.Exists(filePath))
      {
        File.Delete(filePath);
      }
    }
  }
}
