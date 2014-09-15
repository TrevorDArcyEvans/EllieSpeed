//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EllieSpeed.Utilities;
using ZedGraph;

namespace EllieSpeed.DataLogger.Visualiser
{
  public partial class Visualiser : Form
  {
    private readonly DataLogger mLogger;

    public Visualiser()
    {
      InitializeComponent();
    }

    public Visualiser(string title, DataLogger logger) :
      this()
    {
      mLogger = logger;
      Text = title;
    }

    private void Visualiser_Load(object sender, EventArgs e)
    {
      using (new AutoWaitCursor())
      {
        var pane = ZedGraph.GraphPane;

        // Set the titles and axis labels
        pane.XAxis.MajorGrid.IsZeroLine = pane.X2Axis.MajorGrid.IsZeroLine = false;
        pane.XAxis.Title.Text = "Time (s)";
        pane.XAxis.Title.FontSpec.Size = pane.XAxis.Scale.FontSpec.Size = 8f;
        pane.YAxis.IsVisible = pane.Y2Axis.IsVisible = false;
        pane.Title.IsVisible = false;

        // Show the x axis grid
        pane.XAxis.MajorGrid.IsVisible = true;

        // Fill the axis background with a gradient
        pane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

        // Add a text box with instructions
        var text = new TextObj("Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
                        0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
        text.FontSpec.StringAlignment = StringAlignment.Near;
        pane.GraphObjList.Add(text);

        // Enable scrollbars if needed
        ZedGraph.IsShowHScrollBar = true;
        ZedGraph.IsShowVScrollBar = true;
        ZedGraph.IsAutoScrollRange = true;


        var rpm = (from bd in mLogger.BikeDatas.OrderBy(bd => bd.ID) select bd.RPM).ToList();
        AddTrace(pane, "RPM", rpm, Color.Blue, SymbolType.Diamond);

        var wt = (from bd in mLogger.BikeDatas.OrderBy(bd => bd.ID) select bd.WaterTemperature).ToList();
        AddTrace(pane, "Water Temp", wt, Color.Green, SymbolType.Square);

        //var spd = (from bd in mLogger.BikeDatas.OrderBy(bd => bd.ID) select bd.Speedometer * 3.6).ToList();
        //AddTrace(pane, "Speed", spd, Color.Indigo, SymbolType.Triangle);


        // OPTIONAL: Show tooltips when the mouse hovers over a point
        ZedGraph.IsShowPointValues = true;
        ZedGraph.PointValueEvent += PointValueHandler;

        // OPTIONAL: Add a custom context menu item
        ZedGraph.ContextMenuBuilder += OnContextMenuBuilder;

        // OPTIONAL: Handle the Zoom Event
        ZedGraph.ZoomEvent += OnZoomEvent;

        // Tell ZedGraph to calculate the axis ranges
        // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
        // up the proper scrolling parameters
        ZedGraph.AxisChange();

        // Make sure the Graph gets redrawn
        ZedGraph.Invalidate();
      }
    }

    private void AddTrace(GraphPane pane, string title, IList<double> yPts, Color clr, SymbolType symbol)
    {
      const double DataFrequency = 0.1;

      var pts = new PointPairList();
      for (var i = 0; i < yPts.Count(); i++)
      {
        pts.Add(i * DataFrequency, yPts[i]);
      }
      var curve = pane.AddCurve(title, pts, clr, symbol);

      // Fill the symbols with white
      curve.Symbol.Fill = new Fill(Color.White);

      var yaxis = new YAxis(title);
      yaxis.Scale.IsUseTenPower = false;
      yaxis.Scale.MagAuto = false;
      yaxis.Color = clr;
      yaxis.MajorGrid.IsZeroLine = false;
      yaxis.Scale.FontSpec.FontColor = yaxis.Title.FontSpec.FontColor = clr;
      yaxis.Scale.FontSpec.Size = yaxis.Title.FontSpec.Size = 8f;
      pane.YAxisList.Add(yaxis);

      curve.YAxisIndex = pane.YAxisList.IndexOf(title);
    }

    /// <summary>
    /// Display customized tooltips when the mouse hovers over a point
    /// </summary>
    private string PointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
    {
      // Get the PointPair that is under the mouse
      PointPair pt = curve[iPt];

      return curve.Label.Text + " = " + pt.Y.ToString("f2") + " @ " + pt.X.ToString("f1") + " s";
    }

    /// <summary>
    /// Customize the context menu by adding a new item to the end of the menu
    /// </summary>
    private void OnContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      //ToolStripMenuItem item = new ToolStripMenuItem();
      //item.Name = "add-beta";
      //item.Tag = "add-beta";
      //item.Text = "Add a new Beta Point";
      //item.Click += new System.EventHandler(AddBetaPoint);

      //menuStrip.Items.Add(item);
    }

    // Respond to a Zoom Event
    private void OnZoomEvent(ZedGraphControl control, ZoomState oldState, ZoomState newState)
    {
      // Here we get notification everytime the user zooms or pans
    }
  }
}
