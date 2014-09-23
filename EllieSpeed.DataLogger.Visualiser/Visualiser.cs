//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EllieSpeed.Utilities;
using ZedGraph;

namespace EllieSpeed.DataLogger.Visualiser
{
  public partial class Visualiser : DataForm
  {
    public Visualiser()
    {
      InitializeComponent();
    }

    public Visualiser(string title, DataLogger logger) :
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
                        0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
                    {
                      FontSpec = {StringAlignment = StringAlignment.Near}
                    };
        pane.GraphObjList.Add(text);

        // Enable scrollbars if needed
        ZedGraph.IsShowHScrollBar = true;
        ZedGraph.IsShowVScrollBar = true;
        ZedGraph.IsAutoScrollRange = true;


        AddTrace(pane, PlottableDataEnum.RPM);
        AddTrace(pane, PlottableDataEnum.Speedometer);
      }
    }

    private void AddTrace(GraphPane pane, PlottableDataEnum dataName)
    {
      // TODO   read data rate from DLL plugin:
      //        EXTERN_DLL_EXPORT int Startup(char *szSavePath)
      const double DataFrequency = 0.1;

      var pts = new PointPairList();
      var yPts = GetData(dataName);
      for (var i = 0; i < yPts.Count(); i++)
      {
        pts.Add(i * DataFrequency, yPts[i]);
      }
      var title = GetDataName(dataName);
      var clr = GetDataColour(dataName);
      var symbol = GetDataSymbol(dataName);
      var curve = pane.AddCurve(title, pts, clr, symbol);

      // Fill the symbols with white
      curve.Symbol.Fill = new Fill(Color.White);

      var yaxis = new YAxis(title);
      yaxis.Scale.IsUseTenPower = false;
      yaxis.Scale.MagAuto = false;
      yaxis.Color = clr;
      yaxis.MajorGrid.IsZeroLine = false;
      yaxis.MajorTic.IsOpposite = yaxis.MinorTic.IsOpposite = false;
      yaxis.Scale.FontSpec.FontColor = yaxis.Title.FontSpec.FontColor = clr;
      yaxis.Scale.FontSpec.Size = yaxis.Title.FontSpec.Size = 8f;
      pane.YAxisList.Add(yaxis);

      AssignYAxes(pane);
    }

    private void AssignYAxes(GraphPane pane)
    {
      foreach (var crv in pane.CurveList)
      {
        crv.YAxisIndex = pane.YAxisList.IndexOf(crv.Label.Text);
      }
    }

    protected override string PointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
    {
      // Get the PointPair that is under the mouse
      var pt = curve[iPt];

      return curve.Label.Text + " = " + pt.Y.ToString("f2") + " @ " + pt.X.ToString("f1") + " s";
    }

    private enum PlottableDataEnum
    {
      RPM,
      EngineTemperature,
      WaterTemperature,
      Gear,
      Fuel,
      Speedometer,
      Yaw,
      Pitch,
      Roll,
      YawVelocity,
      PitchVelocity,
      SuspNormLengthFront,
      SuspNormLengthRear,
      Steer,
      Throttle,
      FrontBrake,
      RearBrake,
      Clutch,
      WheelSpeedFront,
      WheelSpeedRear,
    }

    protected override void OnContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      var allDataNames = Enum.GetValues(typeof(PlottableDataEnum)).Cast<PlottableDataEnum>();
      foreach (var dataName in allDataNames)
      {
        var pane = ZedGraph.GraphPane;
        var item = new ToolStripMenuItem
        {
          Tag = dataName,
          Text = GetDataName(dataName)
        };
        item.CheckState = pane.CurveList.Any(crv => crv.Label.Text == item.Text) ?
                            CheckState.Checked :
                            CheckState.Unchecked;
        item.Click += ItemOnClick;

        menuStrip.Items.Add(item);
      }
    }

    private void ItemOnClick(object sender, EventArgs eventArgs)
    {
      var pane = ZedGraph.GraphPane;
      var item = (ToolStripMenuItem)sender;
      if (pane.CurveList.Any(crv => crv.Label.Text == item.Text))
      {
        pane.CurveList.Remove(pane.CurveList.Single(crv => crv.Label.Text == item.Text));
        pane.YAxisList.Remove(pane.YAxisList.Single(ax => ax.Title.Text == item.Text));

        AssignYAxes(pane);
      }
      else
      {
        var dataNameTag = (PlottableDataEnum)item.Tag;
        AddTrace(pane, dataNameTag);
      }

      ZedGraph.AxisChange();
      ZedGraph.Invalidate();
    }

    private List<double> GetData(PlottableDataEnum dataName)
    {
      switch (dataName)
      {
        case PlottableDataEnum.RPM:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.RPM).ToList();

        case PlottableDataEnum.EngineTemperature:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.EngineTemperature).ToList();

        case PlottableDataEnum.WaterTemperature:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.WaterTemperature).ToList();

        case PlottableDataEnum.Gear:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select (double)bd.Gear).ToList();

        case PlottableDataEnum.Fuel:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Fuel).ToList();

        case PlottableDataEnum.Speedometer:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Speedometer * 3.6).ToList();

        case PlottableDataEnum.Yaw:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Yaw).ToList();

        case PlottableDataEnum.Pitch:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Pitch).ToList();

        case PlottableDataEnum.Roll:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Roll).ToList();

        case PlottableDataEnum.YawVelocity:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.YawVelocity).ToList();

        case PlottableDataEnum.PitchVelocity:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.PitchVelocity).ToList();

        case PlottableDataEnum.SuspNormLengthFront:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.SuspNormLengthFront).ToList();

        case PlottableDataEnum.SuspNormLengthRear:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.SuspNormLengthRear).ToList();

        case PlottableDataEnum.Steer:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Steer).ToList();

        case PlottableDataEnum.Throttle:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Throttle).ToList();

        case PlottableDataEnum.FrontBrake:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.FrontBrake).ToList();

        case PlottableDataEnum.RearBrake:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.RearBrake).ToList();

        case PlottableDataEnum.Clutch:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.Clutch).ToList();

        case PlottableDataEnum.WheelSpeedFront:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.WheelSpeedFront).ToList();

        case PlottableDataEnum.WheelSpeedRear:
          return (from bd in Logger.BikeDatas.OrderBy(bd => bd.ID) select bd.WheelSpeedRear).ToList();
      }

      throw new ArgumentOutOfRangeException("Unknown data: " + dataName);
    }

    private string GetDataName(PlottableDataEnum dataName)
    {
      switch (dataName)
      {
        case PlottableDataEnum.RPM:
          return "RPM";

        case PlottableDataEnum.EngineTemperature:
          return "Engine Temp";

        case PlottableDataEnum.WaterTemperature:
          return "Water Temp";

        case PlottableDataEnum.Gear:
          return "Gear";

        case PlottableDataEnum.Fuel:
          return "Fuel";

        case PlottableDataEnum.Speedometer:
          return "Speed";

        case PlottableDataEnum.Yaw:
          return "Yaw";

        case PlottableDataEnum.Pitch:
          return "Pitch";

        case PlottableDataEnum.Roll:
          return "Roll";

        case PlottableDataEnum.YawVelocity:
          return "Yaw Vel";

        case PlottableDataEnum.PitchVelocity:
          return "Pitch Vel";

        case PlottableDataEnum.SuspNormLengthFront:
          return "Susp Length (F)";

        case PlottableDataEnum.SuspNormLengthRear:
          return "Susp Length (R)";

        case PlottableDataEnum.Steer:
          return "Steer";

        case PlottableDataEnum.Throttle:
          return "Throttle";

        case PlottableDataEnum.FrontBrake:
          return "Brake (F)";

        case PlottableDataEnum.RearBrake:
          return "Brake (R)";

        case PlottableDataEnum.Clutch:
          return "Clutch";

        case PlottableDataEnum.WheelSpeedFront:
          return "Wheel Speed (F)";

        case PlottableDataEnum.WheelSpeedRear:
          return "Wheel Speed (R)";
      }

      throw new ArgumentOutOfRangeException("Unknown data: " + dataName);
    }

    private Color GetDataColour(PlottableDataEnum dataName)
    {
      switch (dataName)
      {
        case PlottableDataEnum.RPM:
          return Color.Indigo;

        case PlottableDataEnum.EngineTemperature:
          return Color.Green;

        case PlottableDataEnum.WaterTemperature:
          return Color.Blue;

        case PlottableDataEnum.Gear:
          return Color.LawnGreen;

        case PlottableDataEnum.Fuel:
          return Color.Magenta;

        case PlottableDataEnum.Speedometer:
          return Color.MediumPurple;

        case PlottableDataEnum.Yaw:
          return Color.MediumSeaGreen;

        case PlottableDataEnum.Pitch:
          return Color.MediumSpringGreen;

        case PlottableDataEnum.Roll:
          return Color.MediumTurquoise;

        case PlottableDataEnum.YawVelocity:
          return Color.Orange;

        case PlottableDataEnum.PitchVelocity:
          return Color.Orchid;

        case PlottableDataEnum.SuspNormLengthFront:
          return Color.SaddleBrown;

        case PlottableDataEnum.SuspNormLengthRear:
          return Color.SandyBrown;

        case PlottableDataEnum.Steer:
          return Color.SkyBlue;

        case PlottableDataEnum.Throttle:
          return Color.Red;

        case PlottableDataEnum.FrontBrake:
          return Color.Lime;

        case PlottableDataEnum.RearBrake:
          return Color.DarkTurquoise;

        case PlottableDataEnum.Clutch:
          return Color.DarkSeaGreen;

        case PlottableDataEnum.WheelSpeedFront:
          return Color.BurlyWood;

        case PlottableDataEnum.WheelSpeedRear:
          return Color.DeepSkyBlue;
      }

      throw new ArgumentOutOfRangeException("Unknown data: " + dataName);
    }

    private SymbolType GetDataSymbol(PlottableDataEnum dataName)
    {
      switch (dataName)
      {
        case PlottableDataEnum.RPM:
          return SymbolType.Circle;

        case PlottableDataEnum.EngineTemperature:
          return SymbolType.Diamond;

        case PlottableDataEnum.WaterTemperature:
          return SymbolType.HDash;

        case PlottableDataEnum.Gear:
          return SymbolType.Plus;

        case PlottableDataEnum.Fuel:
          return SymbolType.Square;

        case PlottableDataEnum.Speedometer:
          return SymbolType.Star;

        case PlottableDataEnum.Yaw:
          return SymbolType.Triangle;

        case PlottableDataEnum.Pitch:
          return SymbolType.TriangleDown;

        case PlottableDataEnum.Roll:
          return SymbolType.VDash;

        case PlottableDataEnum.YawVelocity:
          return SymbolType.XCross;

        case PlottableDataEnum.PitchVelocity:
          return SymbolType.Circle;

        case PlottableDataEnum.SuspNormLengthFront:
          return SymbolType.Diamond;

        case PlottableDataEnum.SuspNormLengthRear:
          return SymbolType.HDash;

        case PlottableDataEnum.Steer:
          return SymbolType.Plus;

        case PlottableDataEnum.Throttle:
          return SymbolType.Square;

        case PlottableDataEnum.FrontBrake:
          return SymbolType.Star;

        case PlottableDataEnum.RearBrake:
          return SymbolType.Triangle;

        case PlottableDataEnum.Clutch:
          return SymbolType.TriangleDown;

        case PlottableDataEnum.WheelSpeedFront:
          return SymbolType.VDash;

        case PlottableDataEnum.WheelSpeedRear:
          return SymbolType.XCross;
      }

      throw new ArgumentOutOfRangeException("Unknown data: " + dataName);
    }
  }
}
