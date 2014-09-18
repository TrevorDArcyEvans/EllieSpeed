//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//


namespace EllieSpeed.Broadcast
{
  public interface ISerialDataBroadcaster
  {
    void OnSerialData(SerialDataEventArgs data);
  }
}
