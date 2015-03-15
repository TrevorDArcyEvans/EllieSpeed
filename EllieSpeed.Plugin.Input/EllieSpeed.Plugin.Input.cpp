//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"
#include <AtlConv.h>
#include <vcclr.h>
#using <mscorlib.dll>
#using "EllieSpeed.GPBikes.dll"
#using "EllieSpeed.Common.dll"

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

public ref class Globals
{
public:
  static EllieSpeed::GPBikes::DataReceiver^ DataReceiver;
};

Reflection::Assembly^ LoadFromSameFolder(Object^ sender, ResolveEventArgs^ args)
{
  String^ folderPath = Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
  AssemblyName^ assyName = gcnew AssemblyName(args->Name);
  String^ assemblyPath = Path::Combine(folderPath, assyName->Name + ".dll");
  if (!File::Exists(assemblyPath))
  {
    assemblyPath = Path::Combine(folderPath, assyName->Name + ".exe");
    if (!File::Exists(assemblyPath))
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
  Globals::DataReceiver = gcnew EllieSpeed::GPBikes::DataReceiver();

  return Globals::DataReceiver->Startup();
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
  Globals::DataReceiver->Shutdown();
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

/* iIndex is the 0 based controller index. psInfo must be filled with controller info */
EXTERN_DLL_EXPORT int GetControllerInfo(int iIndex, SControllerInfo_t* psInfo)
{
  USES_CONVERSION;

  EllieSpeed::Common::GPBikes::SControllerInfo_t^ info = Globals::DataReceiver->GetControllerInfo(iIndex);

  pin_ptr<const wchar_t> name = PtrToStringChars(info->Name);
  const wchar_t *constName = name;
  strcpy_s(psInfo->m_szName, W2A(constName));

  pin_ptr<const wchar_t> uuid = PtrToStringChars(info->UUID);
  const wchar_t *constUuid = uuid;
  strcpy_s(psInfo->m_szUUID, W2A(constUuid));

  psInfo->m_iID = info->ID;

  psInfo->m_iNumAxis = info->NumAxis;
  for (int i = 0; i < 6; i++)
  {
    for (int j = 0; j < 3; j++)
    {
      psInfo->m_aaiAxisRange[i][j] = info->AxisRange[3*i + j];
    }
  }

  psInfo->m_iNumSliders = info->NumSliders;
  for (int i = 0; i < 6; i++)
  {
    psInfo->m_aiSliderRange[i] = info->SliderRange[i];
  }

  psInfo->m_iNumButtons = info->NumButtons;
  psInfo->m_iNumPOV = info->NumPOV;

  psInfo->m_iNumDials = info->NumDials;
  for (int i = 0; i < 8; i++)
  {
    psInfo->m_aiDialRange[i] = info->DialRange[i];
  }

  return 0;
}

/* iID is the unique controller ID. psData must be filled with controller data */
EXTERN_DLL_EXPORT int GetControllerData(int iID, SControllerData_t* psData)
{
  EllieSpeed::Common::GPBikes::SControllerData_t^ data = Globals::DataReceiver->GetControllerData(iID);

  for (int i = 0; i < 6; i++)
  {
    psData->m_aiAxis[i] = data->Axis[i];
  }

  for (int i = 0; i < 6; i++)
  {
    psData->m_aiSlider[i] = data->Slider[i];
  }

  for (int i = 0; i < 32; i++)
  {
    psData->m_aiButton[i] = data->Button[i];
  }

  for (int i = 0; i < 2; i++)
  {
    psData->m_aiPOV[i] = data->POV[i];
  }

  for (int i = 0; i < 8; i++)
  {
    psData->m_aiDial[i] = data->Dial[i];
  }

  return 0;
}

