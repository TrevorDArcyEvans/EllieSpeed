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
      mReceiver.OnRunTelemetry += Receiver_OnRunTelemetry;
    }

    void Receiver_OnRunTelemetry(object sender, Broadcast.DataEventArgs<Broadcast.GPBikes.SPluginsBikeData_t> e)
    {
      Dispatcher.Invoke((Action)(() =>
      {
        Tachometer.CurrentValue = e.Data.RPM;
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
