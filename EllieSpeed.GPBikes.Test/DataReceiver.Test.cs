//
//  Copyright (C) 2015 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using EllieSpeed.Common;
using NUnit.Framework;

namespace EllieSpeed.GPBikes.Test
{
  [TestFixture]
  public class DataReceiver_Test
  {
    private const string Data =
      // Axis x6
      "1$2$3$4$5$6$" +

      // Slider x6
      "7$8$9$10$11$12$" +

      // Button x32
      "13$14$15$16$17$18$19$20$" +
      "21$22$23$24$25$26$27$28$" +
      "29$30$32$32$33$34$35$36$" +
      "37$38$39$40$41$42$42$44$" +

      // POV x2
      "45$46$" +

      // Dial x8
      "47$48$49$50$51$52$53$54$";

    [Test]
    public void OnSerialData_Completes()
    {
      var rec = new DataReceiver();

      rec.OnSerialData(this, new SerialDataEventArgs(Data));
    }

    [Test]
    public void OnSerialData_ConvertsExpected()
    {
      var rec = new DataReceiver();

      rec.OnSerialData(this, new SerialDataEventArgs(Data));
      var conData = rec.GetControllerData(DataReceiver.ControllerID);

      Assert.AreEqual(conData.Axis[0], 1);
      Assert.AreEqual(conData.Axis[1], 2);
      Assert.AreEqual(conData.Axis[2], 3);
      Assert.AreEqual(conData.Axis[3], 4);
      Assert.AreEqual(conData.Axis[4], 5);
      Assert.AreEqual(conData.Axis[5], 6);

      Assert.AreEqual(conData.Slider[0], 7);
      Assert.AreEqual(conData.Slider[1], 8);
      Assert.AreEqual(conData.Slider[2], 9);
      Assert.AreEqual(conData.Slider[3], 10);
      Assert.AreEqual(conData.Slider[4], 11);
      Assert.AreEqual(conData.Slider[5], 12);

      Assert.AreEqual(conData.Button[0], DataReceiver.ToByte(13));
      Assert.AreEqual(conData.Button[1], DataReceiver.ToByte(14));
      Assert.AreEqual(conData.Button[2], DataReceiver.ToByte(15));
      Assert.AreEqual(conData.Button[3], DataReceiver.ToByte(16));
      Assert.AreEqual(conData.Button[4], DataReceiver.ToByte(17));
      Assert.AreEqual(conData.Button[5], DataReceiver.ToByte(18));
      Assert.AreEqual(conData.Button[6], DataReceiver.ToByte(19));
      Assert.AreEqual(conData.Button[7], DataReceiver.ToByte(20));
      Assert.AreEqual(conData.Button[8], DataReceiver.ToByte(21));
      Assert.AreEqual(conData.Button[9], DataReceiver.ToByte(22));
      Assert.AreEqual(conData.Button[10], DataReceiver.ToByte(23));
      Assert.AreEqual(conData.Button[11], DataReceiver.ToByte(24));
      Assert.AreEqual(conData.Button[12], DataReceiver.ToByte(25));
      Assert.AreEqual(conData.Button[13], DataReceiver.ToByte(26));
      Assert.AreEqual(conData.Button[14], DataReceiver.ToByte(27));
      Assert.AreEqual(conData.Button[15], DataReceiver.ToByte(28));
      Assert.AreEqual(conData.Button[16], DataReceiver.ToByte(29));
      Assert.AreEqual(conData.Button[17], DataReceiver.ToByte(30));
      Assert.AreEqual(conData.Button[18], DataReceiver.ToByte(31));
      Assert.AreEqual(conData.Button[19], DataReceiver.ToByte(32));
      Assert.AreEqual(conData.Button[20], DataReceiver.ToByte(33));
      Assert.AreEqual(conData.Button[21], DataReceiver.ToByte(34));
      Assert.AreEqual(conData.Button[22], DataReceiver.ToByte(35));
      Assert.AreEqual(conData.Button[23], DataReceiver.ToByte(36));
      Assert.AreEqual(conData.Button[24], DataReceiver.ToByte(37));
      Assert.AreEqual(conData.Button[25], DataReceiver.ToByte(38));
      Assert.AreEqual(conData.Button[26], DataReceiver.ToByte(39));
      Assert.AreEqual(conData.Button[27], DataReceiver.ToByte(40));
      Assert.AreEqual(conData.Button[28], DataReceiver.ToByte(41));
      Assert.AreEqual(conData.Button[29], DataReceiver.ToByte(42));
      Assert.AreEqual(conData.Button[30], DataReceiver.ToByte(43));
      Assert.AreEqual(conData.Button[31], DataReceiver.ToByte(44));

      Assert.AreEqual(conData.POV[0], DataReceiver.ToByte(45));
      Assert.AreEqual(conData.POV[1], DataReceiver.ToByte(46));

      Assert.AreEqual(conData.Dial[0], DataReceiver.ToByte(47));
      Assert.AreEqual(conData.Dial[1], DataReceiver.ToByte(48));
      Assert.AreEqual(conData.Dial[2], DataReceiver.ToByte(49));
      Assert.AreEqual(conData.Dial[3], DataReceiver.ToByte(40));
      Assert.AreEqual(conData.Dial[4], DataReceiver.ToByte(41));
      Assert.AreEqual(conData.Dial[5], DataReceiver.ToByte(42));
      Assert.AreEqual(conData.Dial[6], DataReceiver.ToByte(43));
      Assert.AreEqual(conData.Dial[7], DataReceiver.ToByte(44));
    }

    [Test]
    public void ToByte_ReturnsExpected()
    {
      Assert.AreEqual(DataReceiver.ToByte(0), 0);
      Assert.AreEqual(DataReceiver.ToByte(1023), 255);
    }
  }
}
