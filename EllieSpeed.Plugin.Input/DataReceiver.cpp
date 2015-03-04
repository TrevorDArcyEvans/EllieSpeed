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
}

void DataReceiver::Reset()
{
}

int DataReceiver::GetNumControllers()
{
  return 1;
}

int DataReceiver::GetControllerInfo(int iIndex, SControllerInfo_t* psInfo)
{
  strcpy_s(psInfo->m_szName, "EllieSpeed Bike Controller");
  strcpy_s(psInfo->m_szUUID, "EllieSpeed BC001");
  psInfo->m_iID = ControllerID;

  // Arduino has 10 bit analog input [0, 1023]

  if (false)
  {
    // axis 0
    psInfo->m_iNumAxis = 1;
    psInfo->m_aaiAxisRange[0][0] = 0;     // min
    psInfo->m_aaiAxisRange[0][1] = 1023;  // max
    psInfo->m_aaiAxisRange[0][2] = 512;   // center
  }

  psInfo->m_iNumSliders = 1;
  psInfo->m_aiSliderRange[0] = 1023;

  psInfo->m_iNumButtons = 0;
  psInfo->m_iNumPOV = 0;
  psInfo->m_iNumDials = 0;

  return 0;
}

int DataReceiver::GetControllerData(int iID, SControllerData_t* psData)
{
  if (iID != ControllerID)
  {
    return -1;
  }

  msclr::lock lock(mLock);

  if (String::IsNullOrEmpty(mLastData))
  {
    return 0;
  }

  if (false)
  {
    psData->m_aiAxis[0] = int::Parse(mLastData);
  }

  psData->m_aiSlider[0] = int::Parse(mLastData);

  return 0;
}
