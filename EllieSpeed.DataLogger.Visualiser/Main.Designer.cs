//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

namespace EllieSpeed.DataLogger.Visualiser
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
      System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.WindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.WindowCascadeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.WindowTileHorizontalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.WindowTileVerticalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.WindowArrangeIconsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ViewDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ViewTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.HelpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.FileOpenDlg = new System.Windows.Forms.OpenFileDialog();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripMenuItem1
      // 
      toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOpenMenuItem,
            this.FileCloseMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
      toolStripMenuItem1.Text = "&File";
      toolStripMenuItem1.DropDownOpening += new System.EventHandler(this.FileMenuItem_DropDownOpening);
      // 
      // FileOpenMenuItem
      // 
      this.FileOpenMenuItem.Image = global::EllieSpeed.DataLogger.Visualiser.Properties.Resources.folder_document_32x32;
      this.FileOpenMenuItem.Name = "FileOpenMenuItem";
      this.FileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.FileOpenMenuItem.Size = new System.Drawing.Size(148, 22);
      this.FileOpenMenuItem.Text = "&Open";
      this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpen_Click);
      // 
      // FileCloseMenuItem
      // 
      this.FileCloseMenuItem.Image = global::EllieSpeed.DataLogger.Visualiser.Properties.Resources.document_delete_32x32;
      this.FileCloseMenuItem.Name = "FileCloseMenuItem";
      this.FileCloseMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
      this.FileCloseMenuItem.Size = new System.Drawing.Size(148, 22);
      this.FileCloseMenuItem.Text = "&Close";
      this.FileCloseMenuItem.Click += new System.EventHandler(this.FileClose_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Image = global::EllieSpeed.DataLogger.Visualiser.Properties.Resources.exit_32x32;
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.FileExit_Click);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripMenuItem1,
            this.WindowMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.MdiWindowListItem = this.WindowMenuItem;
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(721, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // WindowMenuItem
      // 
      this.WindowMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WindowCascadeMenuItem,
            this.WindowTileHorizontalMenuItem,
            this.WindowTileVerticalMenuItem,
            this.WindowArrangeIconsMenuItem});
      this.WindowMenuItem.Name = "WindowMenuItem";
      this.WindowMenuItem.Size = new System.Drawing.Size(63, 20);
      this.WindowMenuItem.Text = "&Window";
      // 
      // WindowCascadeMenuItem
      // 
      this.WindowCascadeMenuItem.Name = "WindowCascadeMenuItem";
      this.WindowCascadeMenuItem.Size = new System.Drawing.Size(151, 22);
      this.WindowCascadeMenuItem.Text = "&Cascade";
      this.WindowCascadeMenuItem.Click += new System.EventHandler(this.WindowCascade_Click);
      // 
      // WindowTileHorizontalMenuItem
      // 
      this.WindowTileHorizontalMenuItem.Name = "WindowTileHorizontalMenuItem";
      this.WindowTileHorizontalMenuItem.Size = new System.Drawing.Size(151, 22);
      this.WindowTileHorizontalMenuItem.Text = "Tile &Horizontal";
      this.WindowTileHorizontalMenuItem.Click += new System.EventHandler(this.WindowTileHorizontal_Click);
      // 
      // WindowTileVerticalMenuItem
      // 
      this.WindowTileVerticalMenuItem.Name = "WindowTileVerticalMenuItem";
      this.WindowTileVerticalMenuItem.Size = new System.Drawing.Size(151, 22);
      this.WindowTileVerticalMenuItem.Text = "Tile &Vertical";
      this.WindowTileVerticalMenuItem.Click += new System.EventHandler(this.WindowTileVertical_Click);
      // 
      // WindowArrangeIconsMenuItem
      // 
      this.WindowArrangeIconsMenuItem.Name = "WindowArrangeIconsMenuItem";
      this.WindowArrangeIconsMenuItem.Size = new System.Drawing.Size(151, 22);
      this.WindowArrangeIconsMenuItem.Text = "&Arrange Icons";
      this.WindowArrangeIconsMenuItem.Click += new System.EventHandler(this.WindowArrangeIcons_Click);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewDataMenuItem,
            this.ViewTrackMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "&View";
      // 
      // ViewDataMenuItem
      // 
      this.ViewDataMenuItem.Name = "ViewDataMenuItem";
      this.ViewDataMenuItem.Size = new System.Drawing.Size(152, 22);
      this.ViewDataMenuItem.Text = "&Data";
      // 
      // ViewTrackMenuItem
      // 
      this.ViewTrackMenuItem.Name = "ViewTrackMenuItem";
      this.ViewTrackMenuItem.Size = new System.Drawing.Size(152, 22);
      this.ViewTrackMenuItem.Text = "&Track";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAboutMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // HelpAboutMenuItem
      // 
      this.HelpAboutMenuItem.Name = "HelpAboutMenuItem";
      this.HelpAboutMenuItem.Size = new System.Drawing.Size(116, 22);
      this.HelpAboutMenuItem.Text = "&About...";
      this.HelpAboutMenuItem.Click += new System.EventHandler(this.HelpAbout_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 461);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(721, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // FileOpenDlg
      // 
      this.FileOpenDlg.DefaultExt = "sqlite3";
      this.FileOpenDlg.Filter = "Data files (*.sqlite3)|*.sqlite3|All files (*.*)|*.*";
      this.FileOpenDlg.Title = "Open data file";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(721, 25);
      this.toolStrip1.TabIndex = 4;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::EllieSpeed.DataLogger.Visualiser.Properties.Resources.folder_document_32x32;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "toolStripButton1";
      this.toolStripButton1.Click += new System.EventHandler(this.FileOpen_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::EllieSpeed.DataLogger.Visualiser.Properties.Resources.document_delete_32x32;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "toolStripButton2";
      this.toolStripButton2.Click += new System.EventHandler(this.FileClose_Click);
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(721, 483);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Main";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.Text = "EllieSpeed Data Visualiser";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog FileOpenDlg;
    private System.Windows.Forms.ToolStripMenuItem WindowMenuItem;
    private System.Windows.Forms.ToolStripMenuItem WindowCascadeMenuItem;
    private System.Windows.Forms.ToolStripMenuItem WindowTileHorizontalMenuItem;
    private System.Windows.Forms.ToolStripMenuItem WindowTileVerticalMenuItem;
    private System.Windows.Forms.ToolStripMenuItem WindowArrangeIconsMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem HelpAboutMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewDataMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewTrackMenuItem;
  }
}

