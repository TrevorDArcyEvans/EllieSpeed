//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "StdAfx.h"
#include "DataReceiver.h"
#include <msclr\lock.h>

DataReceiver::DataReceiver(String^ portName)
{
  mLock = gcnew Object();
  mReceiver = gcnew EllieSpeed::Arduino::ArduinoReceiver(portName, this);
}

DataReceiver::~DataReceiver()
{
  mReceiver = nullptr;
}

void DataReceiver::OnSerialData(EllieSpeed::Broadcast::SerialDataEventArgs^ data)
{
  msclr::lock lock(mLock);

  mLastData = data->Data;
}

void DataReceiver::Update()
{
  msclr::lock lock(mLock);
}

void DataReceiver::Reset()
{
  msclr::lock lock(mLock);
}

int DataReceiver::GetNumControllers()
{
  return 1;
}

int DataReceiver::GetControllerInfo(int iIndex, SControllerInfo_t* psInfo)
{
  msclr::lock lock(mLock);

  strcpy(psInfo->m_szName, "EllieSpeed Bike Controller");
  strcpy(psInfo->m_szUUID, "EllieSpeed BC001");
  psInfo->m_iID = 42;

  return 0;
}

int DataReceiver::GetControllerData(int iID, SControllerData_t* psData)
{
  msclr::lock lock(mLock);

  return 0;
}
