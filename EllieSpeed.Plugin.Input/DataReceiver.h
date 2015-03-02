//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#pragma once

#include "stdafx.h"
#using <mscorlib.dll>

using namespace System;

public ref class DataReceiver : EllieSpeed::Broadcast::ISerialDataBroadcaster
{
public:
  DataReceiver(String^ portName);
  virtual ~DataReceiver();
  virtual void OnSerialData(EllieSpeed::Broadcast::SerialDataEventArgs^ data);

  void Update();
  void Reset();
  int GetNumControllers();
  int GetControllerInfo(int iIndex, SControllerInfo_t* psInfo);
  int GetControllerData(int iID, SControllerData_t* psData);

private:
  Object^ mLock;
  EllieSpeed::Arduino::ArduinoReceiver^ mReceiver;
  String^ mLastData;
};

