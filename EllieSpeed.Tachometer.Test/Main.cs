//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Windows.Forms;
using EllieSpeed.Broadcast;
using EllieSpeed.Test.Utilties;

namespace EllieSpeed.Tachometer.Test
{
  public partial class Main : Form
  {
    private readonly Broadcaster mBroadcaster = new Broadcaster();

    public Main()
    {
      InitializeComponent();
    }

    private void RPM_ValueChanged(object sender, EventArgs e)
    {
      var evData = TestUtils.CreateBikeEvent();
      evData.MaxRPM = (float)MaxRPM.Value;
      evData.ShiftRPM = (float)ShiftRPM.Value;

      mBroadcaster.OnEventInit(evData);

      var bikeData = TestUtils.CreateBikeDataEx();
      bikeData.BikeData.RPM = (float)CurrentRPM.Value;

      mBroadcaster.OnRunTelemetry(bikeData);
    }
  }
}
