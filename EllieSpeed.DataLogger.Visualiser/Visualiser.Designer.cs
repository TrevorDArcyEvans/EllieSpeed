//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.DataLogger.Visualiser
{
  partial class Visualiser
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ZedGraph = new ZedGraph.ZedGraphControl();
      this.SuspendLayout();
      // 
      // ZedGraph
      // 
      this.ZedGraph.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ZedGraph.IsEnableVZoom = false;
      this.ZedGraph.IsSynchronizeXAxes = true;
      this.ZedGraph.Location = new System.Drawing.Point(0, 0);
      this.ZedGraph.Name = "ZedGraph";
      this.ZedGraph.ScrollGrace = 0D;
      this.ZedGraph.ScrollMaxX = 0D;
      this.ZedGraph.ScrollMaxY = 0D;
      this.ZedGraph.ScrollMaxY2 = 0D;
      this.ZedGraph.ScrollMinX = 0D;
      this.ZedGraph.ScrollMinY = 0D;
      this.ZedGraph.ScrollMinY2 = 0D;
      this.ZedGraph.Size = new System.Drawing.Size(620, 457);
      this.ZedGraph.TabIndex = 0;
      // 
      // Visualiser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(620, 457);
      this.Controls.Add(this.ZedGraph);
      this.Name = "Visualiser";
      this.Text = "Visualiser";
      this.Load += new System.EventHandler(this.Visualiser_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private ZedGraph.ZedGraphControl ZedGraph;
  }
}