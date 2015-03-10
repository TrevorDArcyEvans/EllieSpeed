//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Windows;
using EllieSpeed.Receive;

namespace EllieSpeed.Tachometer
{
  public partial class MainWindow : IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly BikeDataReceiver mReceiver = new BikeDataReceiver(Broadcast.Broadcaster.BroadcastPort);

    public MainWindow()
    {
      InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      mReceiver.OnEventInit += Receiver_OnEventInit;
      mReceiver.OnRunTelemetry += Receiver_OnRunTelemetry;
    }

    void Receiver_OnEventInit(object sender, Broadcast.DataEventArgs<GPBikes.SPluginsBikeEvent_t> e)
    {
      Dispatcher.Invoke((Action)(() =>
      {
        Tachometer.MajorDivisionsCount = Tachometer.MaxValue = (int)(e.Data.MaxRPM / 1000d) + 1;
        Tachometer.OptimalRangeEndValue = e.Data.ShiftRPM / 1000d;

        // gauge is setup up so that ShiftRPM points to sky:
        //    13500RPM --> 45 degrees
        Tachometer.ScaleStartAngle = 45d + (13500 - e.Data.ShiftRPM) / 30;

        Tachometer.RefreshScale();
      }));
    }

    void Receiver_OnRunTelemetry(object sender, Broadcast.DataEventArgs<GPBikes.SPluginsBikeDataEx_t> e)
    {
      Dispatcher.Invoke((Action)(() =>
      {
        Tachometer.CurrentValue = e.Data.BikeData.RPM / 1000d;
      }));
    }

    public void Dispose()
    {
      if (!Disposed)
      {
        mReceiver.Dispose();
      }
      Disposed = true;
    }
  }
}
