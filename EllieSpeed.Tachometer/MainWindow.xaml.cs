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

    void Receiver_OnEventInit(object sender, Broadcast.DataEventArgs<Broadcast.GPBikes.SPluginsBikeEvent_t> e)
    {
      Dispatcher.Invoke((Action)(() =>
      {
        Tachometer.MajorDivisionsCount = (int)(e.Data.MaxRPM / 1000d) + 1;
        Tachometer.MaxValue = e.Data.MaxRPM / 1000d;
        Tachometer.OptimalRangeEndValue = e.Data.ShiftRPM / 1000d;
      }));
    }

    void Receiver_OnRunTelemetry(object sender, Broadcast.DataEventArgs<Broadcast.GPBikes.SPluginsBikeDataEx_t> e)
    {
      Dispatcher.Invoke((Action)(() =>
      {
        Tachometer.CurrentValue = e.Data.BikeData.RPM;
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
