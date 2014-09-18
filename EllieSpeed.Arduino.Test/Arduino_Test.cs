//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Threading;
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
    private const string ReceivePort = "COM7";
    private const string SendPort = "COM8";

    [Test]
    public void ReceiverConstructor_Completes()
    {
      using (new Receiver(ReceivePort))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void SecondReceiverConstructor_ThrowsException()
    {
      using (new Receiver(ReceivePort))
      {
        using (new Receiver(ReceivePort))
        {
        }
      }
    }

    [Test]
    public void SenderConstructor_Completes()
    {
      using (new Sender(SendPort))
      {
      }
    }

    [Test]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void SecondSenderConstructor_ThrowsException()
    {
      using (new Sender(SendPort))
      {
        using (new Sender(SendPort))
        {
        }
      }
    }

    [Test]
    [Timeout(5000)]
    public void SenderSend_Completes()
    {
      using (var send = new Sender(SendPort))
      {
        send.Send("Hello, world!");
      }
    }

    [Test]
    [Timeout(5000)]
    public void ReceiverReceivesMessage()
    {
      const string Message = "Hello, world!";

      var msgReceived = false;
      using (var rec = new Receiver(ReceivePort))
      {
        rec.OnDataReceived += (sender, args) =>
        {
          msgReceived = true;
          Assert.AreEqual(args.Data, Message);
        };
        using (var send = new Sender(SendPort))
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
