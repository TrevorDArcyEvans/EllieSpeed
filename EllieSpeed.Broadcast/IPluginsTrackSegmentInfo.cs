//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System.Runtime.InteropServices;

namespace EllieSpeed.Broadcast
{
  [ComVisible(true)]
  [Guid("F1A38223-A517-4E15-B276-DEE997B0DAD1")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  public interface IPluginsTrackSegmentInfo
  {
      int Type { get; set; }
      float Length { get; set; }
      float Radius { get; set; }
      float Angle { get; set; }
      float Start1 { get; set; }
      float Start2 { get; set; }
  }
}
