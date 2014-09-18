//
//  Copyright (C) 2013 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System;
using System.Windows.Forms;

namespace EllieSpeed.Utilities
{
  public class AutoCursor : IDisposable
  {
    public bool Disposed { get; private set; }

    private readonly Cursor mOldCursor;

    public AutoCursor()
    {
      mOldCursor = Cursor.Current;
    }

    public AutoCursor(Cursor newCursor) :
      this()
    {
      Cursor.Current = newCursor;
    }

    public void Dispose()
    {
      if (Disposed)
      {
        return;
      }

      Cursor.Current = mOldCursor;
      Disposed = true;
    }
  }
}
