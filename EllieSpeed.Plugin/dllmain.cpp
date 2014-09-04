//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"

CComPtr<IBroadcaster> mBroadcaster;

void Initialise()
{
  CoInitialize(NULL);
  mBroadcaster.CoCreateInstance(CLSID_Broadcaster);
}

void Uninitialise()
{
  mBroadcaster = NULL;
  CoUninitialize();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
  switch (ul_reason_for_call)
  {
  case DLL_PROCESS_ATTACH:
    Initialise();
    break;

  case DLL_THREAD_ATTACH:
    break;

  case DLL_THREAD_DETACH:
    break;

  case DLL_PROCESS_DETACH:
    Uninitialise();
    break;
  }
  return TRUE;
}

