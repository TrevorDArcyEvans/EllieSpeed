//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"
#include "DataReceiver.h"
#using <mscorlib.dll>

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

public ref class Globals
{
public:
  static DataReceiver^ DataReceiver;
};

Reflection::Assembly^ LoadFromSameFolder(Object^ sender, ResolveEventArgs^ args)
{
  String^ folderPath = Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
  AssemblyName^ assyName = gcnew AssemblyName(args->Name);
  String^ assemblyPath = Path::Combine(folderPath, assyName->Name + ".dll");
  if (File::Exists(assemblyPath) == false)
  {
    assemblyPath = Path::Combine(folderPath, assyName->Name + ".exe");
    if (File::Exists(assemblyPath) == false)
    {
      return nullptr;
    }
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
  Globals::DataReceiver = gcnew DataReceiver(EllieSpeed::Arduino::ArduinoReceiver::ArduinoPort);

  return 0;
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
  Globals::DataReceiver = nullptr;
}

/* called every rendering frame. This function is optional */
EXTERN_DLL_EXPORT void Update()
{
  Globals::DataReceiver->Update();
}

/* called when a control is queried */
EXTERN_DLL_EXPORT void Reset()
{
  Globals::DataReceiver->Reset();
}

/* called every few seconds to support hot plugging. The return value is the number of active controllers */
EXTERN_DLL_EXPORT int GetNumControllers()
{
  return Globals::DataReceiver->GetNumControllers();
}

/* _iIndex is the 0 based controller index. _psInfo must be filled with controller info */
EXTERN_DLL_EXPORT int GetControllerInfo(int iIndex, SControllerInfo_t* psInfo)
{
  return Globals::DataReceiver->GetControllerInfo(iIndex, psInfo);
}

/* _iID is the unique controller ID. _psData must be filled with controller data */
EXTERN_DLL_EXPORT int GetControllerData(int iID, SControllerData_t* psData)
{
  return Globals::DataReceiver->GetControllerData(iID, psData);
}
