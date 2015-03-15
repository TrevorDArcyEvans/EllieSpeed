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
  public class ArduinoTest
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
      using (new ArduinoMessenger(ReceiveCOMPort))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void SecondReceiverConstructor_ThrowsException()
    {
      using (new ArduinoMessenger(ReceiveCOMPort))
      {
        using (new ArduinoMessenger(ReceiveCOMPort))
        {
        }
      }
    }

    [Test]
    public void SenderConstructor_Completes()
    {
      using (new ArduinoMessenger(SendCOMPort))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void SecondSenderConstructor_ThrowsException()
    {
      using (new ArduinoMessenger(SendCOMPort))
      {
        using (new ArduinoMessenger(SendCOMPort))
        {
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void SenderSend_Completes()
    {
      using (var send = new ArduinoMessenger(SendCOMPort))
      {
        send.Send("Hello, world!");
      }
    }

    [Test]
    [Timeout(5000)]
    public void ReceiverReceivesMessage()
    {
      const string Message = "Hello, world!";

      using (var rec = new ArduinoMessenger(ReceiveCOMPort))
      {
        var msgReceived = false;
        rec.OnSerialData += (sender, args) =>
        {
          msgReceived = true;
          Assert.AreEqual(args.Data, Message);
        };

        using (var send = new ArduinoMessenger(SendCOMPort))
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
