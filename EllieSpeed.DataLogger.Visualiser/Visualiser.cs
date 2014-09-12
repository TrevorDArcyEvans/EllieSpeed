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
        var myPane = ZedGraph.GraphPane;

        // Set the titles and axis labels
        myPane.Title.Text = "Demonstration of Dual Y Graph";
        myPane.XAxis.Title.Text = "Time, Days";
        myPane.YAxis.Title.Text = "Parameter A";
        myPane.Y2Axis.Title.Text = "Parameter B";

        // Make up some data points based on the Sine function
        PointPairList list = new PointPairList();
        PointPairList list2 = new PointPairList();
        PointPairList list3 = new PointPairList();
        PointPairList list4 = new PointPairList();
        PointPairList list5 = new PointPairList();
        PointPairList list6 = new PointPairList();
        for (int i = 0; i < 36000; i++)
        {
          double x = (double)i * 5.0;
          double y = Math.Sin((double)i * Math.PI / 15.0) * 16.0;
          double y2 = y * 13.5;
          double y3 = -y * 13.5;
          double y4 = y + 1;
          double y5 = 1.1 * y - 3;
          double y6 = 3 * y + 0.5;
          list.Add(x, y);
          list2.Add(x, y2);
          list3.Add(x, y3);
          list4.Add(x, y4);
          list5.Add(x, y5);
          list6.Add(x, y6);
        }

        // Generate a red curve with diamond symbols, and "Alpha" in the legend
        LineItem myCurve = myPane.AddCurve("Alpha", list, Color.Red, SymbolType.Diamond);
        // Fill the symbols with white
        myCurve.Symbol.Fill = new Fill(Color.White);

        // Generate a blue curve with circle symbols, and "Beta" in the legend
        myCurve = myPane.AddCurve("Beta", list2, Color.Blue, SymbolType.Circle);
        // Fill the symbols with white
        myCurve.Symbol.Fill = new Fill(Color.White);
        // Associate this curve with the Y2 axis
        myCurve.IsY2Axis = true;

        myCurve = myPane.AddCurve("Brake", list3, Color.Green, SymbolType.Square);
        myCurve.Symbol.Fill = new Fill(Color.White);
        myCurve.IsY2Axis = true;

        myCurve = myPane.AddCurve("Throttle", list4, Color.Gold, SymbolType.Star);
        myCurve.Symbol.Fill = new Fill(Color.White);
        myCurve.IsY2Axis = true;

        myCurve = myPane.AddCurve("Clutch", list5, Color.DarkOrange, SymbolType.Triangle);
        myCurve.Symbol.Fill = new Fill(Color.White);
        myCurve.IsY2Axis = true;

        myCurve = myPane.AddCurve("Lean", list6, Color.Coral, SymbolType.TriangleDown);
        myCurve.Symbol.Fill = new Fill(Color.White);
        myCurve.IsY2Axis = true;

        // Show the x axis grid
        myPane.XAxis.MajorGrid.IsVisible = true;

        // Make the Y axis scale red
        myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
        myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
        // turn off the opposite tics so the Y tics don't show up on the Y2 axis
        myPane.YAxis.MajorTic.IsOpposite = false;
        myPane.YAxis.MinorTic.IsOpposite = false;
        // Don't display the Y zero line
        myPane.YAxis.MajorGrid.IsZeroLine = false;
        // Align the Y axis labels so they are flush to the axis
        myPane.YAxis.Scale.Align = AlignP.Inside;
        // Manually set the axis range
        myPane.YAxis.Scale.Min = -30;
        myPane.YAxis.Scale.Max = 30;

        // Enable the Y2 axis display
        myPane.Y2Axis.IsVisible = true;
        // Make the Y2 axis scale blue
        myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
        myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
        // turn off the opposite tics so the Y2 tics don't show up on the Y axis
        myPane.Y2Axis.MajorTic.IsOpposite = false;
        myPane.Y2Axis.MinorTic.IsOpposite = false;
        // Display the Y2 axis grid lines
        myPane.Y2Axis.MajorGrid.IsVisible = true;
        // Align the Y2 axis labels so they are flush to the axis
        myPane.Y2Axis.Scale.Align = AlignP.Inside;

        // Fill the axis background with a gradient
        myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

        // Add a text box with instructions
        var text = new TextObj("Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
                        0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
        text.FontSpec.StringAlignment = StringAlignment.Near;
        myPane.GraphObjList.Add(text);

        // Enable scrollbars if needed
        ZedGraph.IsShowHScrollBar = true;
        ZedGraph.IsShowVScrollBar = true;
        ZedGraph.IsAutoScrollRange = true;
        ZedGraph.IsScrollY2 = true;

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

    /// <summary>
    /// Display customized tooltips when the mouse hovers over a point
    /// </summary>
    private string PointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
    {
      // Get the PointPair that is under the mouse
      PointPair pt = curve[iPt];

      return curve.Label.Text + " is " + pt.Y.ToString("f2") + " units at " + pt.X.ToString("f1") + " days";
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
      // Here we get notification everytime the user zooms
    }
  }
}
