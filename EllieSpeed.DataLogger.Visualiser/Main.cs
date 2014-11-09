//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.IO;
using System.Linq;
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

      var title = Path.GetFileNameWithoutExtension(FileOpenDlg.FileName);
      var logger = new DataLogger(SQLiteLogger.GetConnectionString(FileOpenDlg.FileName));

      var track = new Track(title, logger)
                 {
                   MdiParent = this
                 };
      var trackMenuItem = ViewTrackMenuItem.DropDownItems.Add(title, track.Icon.ToBitmap());
      trackMenuItem.Tag = track;
      trackMenuItem.Click += DataMenuItem_Click<Track>;
      track.FormClosed += (s, ev) => Form_Closed(ViewTrackMenuItem, track);
      track.Show();

      var vis = new Visualiser(title, logger)
                    {
                      MdiParent = this
                    };
      var dataMenuItem = ViewDataMenuItem.DropDownItems.Add(title, vis.Icon.ToBitmap());
      dataMenuItem.Tag = vis;
      dataMenuItem.Click += DataMenuItem_Click<Visualiser>;
      vis.FormClosed += (s, ev) => Form_Closed(ViewDataMenuItem, vis);
      vis.Show();

      BtnClose.Enabled = true;
    }

    private void Form_Closed(ToolStripMenuItem mi, DataForm form)
    {
      var tsi = mi.DropDownItems.Cast<ToolStripItem>().Single(x => x.Tag == form);
      mi.DropDownItems.Remove(tsi);

      // child form is still in list at this point,
      // so disable button if this is the last child which is about to close
      BtnClose.Enabled = MdiChildren.Count() != 1;
    }

    private void DataMenuItem_Click<T>(object sender, EventArgs e) where T : DataForm
    {
      var menuItem = (ToolStripItem)sender;
      var vis = (T)menuItem.Tag;
      vis.Activate();
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

    private void CloseAllMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var child in MdiChildren)
      {
        child.Close();
      }
    }
  }
}
