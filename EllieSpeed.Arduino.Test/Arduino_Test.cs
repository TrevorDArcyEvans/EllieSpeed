//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Threading;
using EllieSpeed.Broadcast;
using EllieSpeed.Receive;
using Moq;
using NUnit.Framework;

namespace EllieSpeed.Arduino.Test
{
  [TestFixture]
  public class Arduino_Test
  {
    // Virtual COM ports must be first setup in com0com:
    //
    //    http://com0com.sourceforge.net/
    //
    // In the com0com 'Setup Command Prompt' type:
    //
    //    install PortName=COM7 PortName=COM8<return>
    private const string ReceiveCOMPort = "COM7";
    private const string SendCOMPort = "COM8";

    [Test]
    public void ReceiverConstructor_Completes()
    {
      var mockBroadcaster = new Mock<ISerialDataBroadcaster>();
      using (new ArduinoReceiver(ReceiveCOMPort, mockBroadcaster.Object))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void SecondReceiverConstructor_ThrowsException()
    {
      var mockBroadcaster = new Mock<ISerialDataBroadcaster>();
      using (new ArduinoReceiver(ReceiveCOMPort, mockBroadcaster.Object))
      {
        using (new ArduinoReceiver(ReceiveCOMPort, mockBroadcaster.Object))
        {
        }
      }
    }

    [Test]
    public void SenderConstructor_Completes()
    {
      using (new Sender(SendCOMPort))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void SecondSenderConstructor_ThrowsException()
    {
      using (new Sender(SendCOMPort))
      {
        using (new Sender(SendCOMPort))
        {
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void SenderSend_Completes()
    {
      using (var send = new Sender(SendCOMPort))
      {
        send.Send("Hello, world!");
      }
    }

    [Test]
    [Timeout(5000)]
    public void ReceiverReceivesMessage()
    {
      const string Message = "Hello, world!";

      using (var broadcaster = new Broadcaster())
      {
        using (new ArduinoReceiver(ReceiveCOMPort, broadcaster))
        {
          var msgReceived = false;
          using (var rec = new SerialDataReceiver(Broadcaster.BroadcastPort))
          {
            rec.OnSerialDataReceived += (sender, args) =>
            {
              msgReceived = true;
              Assert.AreEqual(args.Data, Message);
            };

            using (var send = new Sender(SendCOMPort))
            {
              send.Send(Message);
            }

            while (!msgReceived)
            {
              Thread.Sleep(100);
            }
          }
        }
      }
    }
  }
}
