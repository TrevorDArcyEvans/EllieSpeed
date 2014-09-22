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

namespace EllieSpeed.DataLogger.Visualiser.Properties
{
  public partial class Track : Form
  {
    private readonly DataLogger mLogger;

    public Track()
    {
      InitializeComponent();
    }

    public Track(string title, DataLogger logger) :
      this()
    {
      mLogger = logger;
      Text = title;
    }

    private void Track_Load(object sender, EventArgs e)
    {
      // TODO   load track data
    }
  }
}
