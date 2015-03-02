//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"
#using <mscorlib.dll>

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

#using "EllieSpeed.Broadcast.dll"

Reflection::Assembly^ LoadFromSameFolder(Object^ sender, ResolveEventArgs^ args)
{
  String^ folderPath = Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
  AssemblyName^ assyName = gcnew AssemblyName(args->Name);
  String^ assemblyPath = Path::Combine(folderPath, assyName->Name + ".dll");
  if (File::Exists(assemblyPath) == false)
  {
    return nullptr;
  }

  Assembly^ assembly = Assembly::LoadFrom(assemblyPath);

  return assembly;
}

void InitialiseNET()
{
  AppDomain^ currDomain = AppDomain::CurrentDomain;
  currDomain->AssemblyResolve += gcnew ResolveEventHandler(LoadFromSameFolder);
}

#define EXTERN_DLL_EXPORT extern "C" _declspec(dllexport)

EXTERN_DLL_EXPORT int Version()
{
  // have to call this in a function which does not reference any .NET code
  // as entry to such a function will fire an AppDomain.AssemblyResolve event
  InitialiseNET();

  return 2;
}

/* called when software is started. If return value is not 0, the plugin is disabled */
EXTERN_DLL_EXPORT int Startup()
{
  return 0;
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
  int i = 0;
}

/* called every rendering frame. This function is optional */
EXTERN_DLL_EXPORT void Update()
{
  int i = 0;
}

/* called when a control is queried */
EXTERN_DLL_EXPORT void Reset()
{
  int i = 0;
}

/* called every few seconds to support hot plugging. The return value is the number of active controllers */
EXTERN_DLL_EXPORT int GetNumControllers()
{
  return 1;
}

/* _iIndex is the 0 based controller index. _psInfo must be filled with controller info */
EXTERN_DLL_EXPORT int GetControllerInfo(int _iIndex, SControllerInfo_t *_psInfo)
{
  return 0;
}

/* _iID is the unique controller ID. _psData must be filled with controller data */
EXTERN_DLL_EXPORT int GetControllerData(int _iID, SControllerData_t *_psData)
{
  return 0;
}

#include <msclr\lock.h>

public ref class DataReceiver : EllieSpeed::Broadcast::ISerialDataBroadcaster
{
public:
  DataReceiver(String^ portName);
  ~DataReceiver();
  virtual void OnSerialData(EllieSpeed::Broadcast::SerialDataEventArgs^ data);

  void Reset();
  int GetNumControllers();
  int GetControllerInfo(int iIndex, SControllerInfo_t* psInfo);
  int GetControllerData(int iID, SControllerData_t* psData);

private:
  Object^ mLock;
  EllieSpeed::Arduino::ArduinoReceiver^ mReceiver;
  String^ mLastData;
};

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

void DataReceiver::Reset()
{
}

int DataReceiver::GetNumControllers()
{
  return 0;
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
