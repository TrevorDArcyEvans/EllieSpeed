//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace EllieSpeed.DataLogger.Visualiser
{
  public partial class DataForm : Form
  {
    protected readonly DataLogger Logger;

    public DataForm()
    {
      InitializeComponent();
    }

    public DataForm(string title, DataLogger logger) :
      this()
    {
      Logger = logger;
      Text = title;
    }

    private void OnLoad(object sender, EventArgs e)
    {
      OnLoadInternal(sender, e);

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

    protected virtual void OnLoadInternal(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Display customized tooltips when the mouse hovers over a point
    /// </summary>
    protected virtual string PointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
    {
      return string.Empty;
    }

    /// <summary>
    /// Customize the context menu by adding a new item to the end of the menu
    /// </summary>
    protected virtual void OnContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
    }

    // Respond to a Zoom Event
    protected virtual void OnZoomEvent(ZedGraphControl control, ZoomState oldState, ZoomState newState)
    {
      // Here we get notification everytime the user zooms or pans
    }
  }
}
