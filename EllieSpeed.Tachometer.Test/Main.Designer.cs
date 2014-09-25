//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.Tachometer.Test
{
  partial class Main
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
        mBroadcaster.Dispose();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.MaxRPM = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.CurrentRPM = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.ShiftRPM = new System.Windows.Forms.NumericUpDown();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MaxRPM)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CurrentRPM)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ShiftRPM)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.MaxRPM, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.CurrentRPM, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.ShiftRPM, 1, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(206, 82);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 3);
      this.label1.Margin = new System.Windows.Forms.Padding(3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Max RPM";
      // 
      // MaxRPM
      // 
      this.MaxRPM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.MaxRPM.Location = new System.Drawing.Point(77, 3);
      this.MaxRPM.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
      this.MaxRPM.Name = "MaxRPM";
      this.MaxRPM.Size = new System.Drawing.Size(120, 20);
      this.MaxRPM.TabIndex = 1;
      this.MaxRPM.Value = new decimal(new int[] {
            13500,
            0,
            0,
            0});
      this.MaxRPM.ValueChanged += new System.EventHandler(this.RPM_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 55);
      this.label3.Margin = new System.Windows.Forms.Padding(3);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(68, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Current RPM";
      // 
      // CurrentRPM
      // 
      this.CurrentRPM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.CurrentRPM.Location = new System.Drawing.Point(77, 55);
      this.CurrentRPM.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
      this.CurrentRPM.Name = "CurrentRPM";
      this.CurrentRPM.Size = new System.Drawing.Size(120, 20);
      this.CurrentRPM.TabIndex = 6;
      this.CurrentRPM.Value = new decimal(new int[] {
            11000,
            0,
            0,
            0});
      this.CurrentRPM.ValueChanged += new System.EventHandler(this.RPM_ValueChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 29);
      this.label2.Margin = new System.Windows.Forms.Padding(3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(55, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Shift RPM";
      // 
      // ShiftRPM
      // 
      this.ShiftRPM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.ShiftRPM.Location = new System.Drawing.Point(77, 29);
      this.ShiftRPM.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
      this.ShiftRPM.Name = "ShiftRPM";
      this.ShiftRPM.Size = new System.Drawing.Size(120, 20);
      this.ShiftRPM.TabIndex = 3;
      this.ShiftRPM.Value = new decimal(new int[] {
            13000,
            0,
            0,
            0});
      this.ShiftRPM.ValueChanged += new System.EventHandler(this.RPM_ValueChanged);
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(206, 82);
      this.Controls.Add(this.tableLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "Main";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "EllieSpeed Tachometer Tester";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MaxRPM)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CurrentRPM)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ShiftRPM)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown MaxRPM;
    private System.Windows.Forms.NumericUpDown ShiftRPM;
    private System.Windows.Forms.NumericUpDown CurrentRPM;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}

