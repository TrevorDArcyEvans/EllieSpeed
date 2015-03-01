//
//  Copyright (C) 2015 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"
//#using <mscorlib.dll>
//
//using namespace System;
//using namespace System::IO;
//using namespace System::Reflection;
//using namespace System::Runtime::InteropServices;

#define EXTERN_DLL_EXPORT extern "C" _declspec(dllexport)

EXTERN_DLL_EXPORT int Version()
{
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
}

/* called every rendering frame. This function is optional */
EXTERN_DLL_EXPORT void Update()
{
}

/* called when a control is queried */
EXTERN_DLL_EXPORT void Reset()
{
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