//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using EllieSpeed.Broadcast;

namespace EllieSpeed.Receive
{
  public class SerialDataReceiver : ReceiverBase
  {
    public event EventHandler<SerialDataEventArgs> OnSerialDataReceived;

    public SerialDataReceiver(int port) :
      base (port)
    {
    }

    protected override void ProcessMessage(byte[] msgBytes)
    {
      // got an object but which one?
      var obj = ByteArrayToObject(msgBytes);

      if (obj is SerialDataEventArgs && OnSerialDataReceived != null)
      {
        OnSerialDataReceived(this, (SerialDataEventArgs)obj);
        return;
      }
    }
  }
}
