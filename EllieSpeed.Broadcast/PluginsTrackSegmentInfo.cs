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
  [Guid("323A4BA9-E942-49F8-A0E3-C57857C8158B")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("EllieSpeed.PluginsTrackSegmentInfo")]
  public class PluginsTrackSegmentInfo : IPluginsTrackSegmentInfo
  {
    public int Type { get; set; }
    public float Length { get; set; }
    public float Radius { get; set; }
    public float Angle { get; set; }
    public float Start1 { get; set; }
    public float Start2 { get; set; }
  }
}
