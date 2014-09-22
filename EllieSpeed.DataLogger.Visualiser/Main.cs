//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
using System.Windows.Forms;
using EllieSpeed.DataLogger.Visualiser.Properties;

namespace EllieSpeed.DataLogger.Visualiser
{
  public partial class Main : Form
  {
    public Main()
    {
      InitializeComponent();
    }

    private void FileOpen_Click(object sender, EventArgs e)
    {
      if (FileOpenDlg.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      var title = Path.GetFileNameWithoutExtension(FileOpenDlg.FileName);
      var logger = new DataLogger(SQLiteLogger.GetConnectionString(FileOpenDlg.FileName));
      var vis = new Visualiser(title, logger)
                    {
                      MdiParent = this
                    };
      vis.Show();
      var track = new Track(title, logger)
                 {
                   MdiParent = this
                 };
      track.Show();

      ViewDataMenuItem.DropDownItems.Add(title);
      ViewTrackMenuItem.DropDownItems.Add(title);
    }

    private void FileClose_Click(object sender, EventArgs e)
    {
      // still have to do a check as can be called via shortcut
      if (ActiveMdiChild == null)
      {
        return;
      }
      ActiveMdiChild.Close();
    }

    private void FileExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void WindowCascade_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.Cascade);
    }

    private void WindowTileHorizontal_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.TileHorizontal);
    }

    private void WindowTileVertical_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.TileVertical);
    }

    private void WindowArrangeIcons_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.ArrangeIcons);
    }

    private void HelpAbout_Click(object sender, EventArgs e)
    {
      var frm = new AboutBox();
      frm.ShowDialog();
    }

    private void FileMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      FileCloseMenuItem.Enabled = ActiveMdiChild != null;
    }
  }
}
