//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Drawing;
using System.Linq;
using EllieSpeed.Utilities;
using ZedGraph;

namespace EllieSpeed.DataLogger.Visualiser
{
  public partial class Track : DataForm
  {
    public Track()
    {
      InitializeComponent();
    }

    public Track(string title, DataLogger logger) :
      base(title, logger)
    {
      InitializeComponent();
    }

    protected override void OnLoadInternal(object sender, EventArgs e)
    {
      using (new AutoWaitCursor())
      {
        var pane = ZedGraph.GraphPane;

        // Set the titles and axis labels
        pane.XAxis.MajorGrid.IsZeroLine = pane.X2Axis.MajorGrid.IsZeroLine = false;
        pane.XAxis.MajorGrid.IsVisible = pane.YAxis.MajorGrid.IsVisible = false;
        pane.XAxis.IsVisible = pane.YAxis.IsVisible = false;
        pane.XAxis.Title.IsVisible = pane.YAxis.Title.IsVisible = false;
        pane.YAxis.IsVisible = pane.Y2Axis.IsVisible = false;
        pane.Title.IsVisible = true;
        pane.Title.Text = Logger.BikeEvents.FirstOrDefault() != null ? Logger.BikeEvents.First().TrackName : string.Empty;

        // Fill the axis background with a gradient
        pane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

        // Add a text box with instructions
        var text = new TextObj("Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
                        0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
                    {
                      FontSpec = {StringAlignment = StringAlignment.Near}
                    };
        pane.GraphObjList.Add(text);

        // Enable scrollbars if needed
        ZedGraph.IsShowHScrollBar = true;
        ZedGraph.IsShowVScrollBar = true;
        ZedGraph.IsAutoScrollRange = true;

        var startPts1 = (from seg in Logger.TrackSegments select seg.Start1).ToList();
        var startPts2 = (from seg in Logger.TrackSegments select seg.Start2).ToList();

        // add start point to end so it all joins up
        startPts1.Add(startPts1[0]);
        startPts2.Add(startPts2[0]);

        var curve = pane.AddCurve(string.Empty, startPts1.ToArray(), startPts2.ToArray(), Color.BlueViolet, SymbolType.Square);

        // Fill the symbols with white
        curve.Symbol.Fill = new Fill(Color.White);
      }
    }

    protected override string PointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
    {
      // Get the PointPair that is under the mouse
      var pt = curve[iPt];

      return "(" + pt.Y.ToString("f2") + ", " + pt.X.ToString("f1") + ")";
    }
  }
}
