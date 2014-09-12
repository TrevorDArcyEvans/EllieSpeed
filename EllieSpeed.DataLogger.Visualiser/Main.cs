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

      var logger = new DataLogger(SQLiteLogger.GetConnectionString(FileOpenDlg.FileName));
      var vis = new Visualiser(Path.GetFileNameWithoutExtension(FileOpenDlg.FileName), logger)
                    {
                      MdiParent = this
                    };
      vis.Show();
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
