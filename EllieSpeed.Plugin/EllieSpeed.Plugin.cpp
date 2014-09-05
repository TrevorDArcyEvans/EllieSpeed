//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"

/*
If compiled as C++, extern "C" must be added to declaration of functions to export

X+ is right, Y+ is top and Z+ is forward.
*/
#define EXTERN_DLL_EXPORT extern "C" _declspec(dllexport)

EXTERN_DLL_EXPORT char* GetModID()
{
  return "gpbikes";
}

extern "C" int _declspec(dllexport) GetModDataVersion()
{
  return 2;
}

EXTERN_DLL_EXPORT int GetInterfaceVersion()
{
  return 8;
}

/* called when software is started */
EXTERN_DLL_EXPORT int Startup(char *szSavePath)
{
  mBroadcaster->OnStartup();

  /*
    return value is requested rate
    0 = 100hz; 1 = 50hz; 2 = 20hz; 3 = 10hz; -1 = disable
  */
  return 3;
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
  mBroadcaster->OnShutdown();
}

/* called when event is initialized */
EXTERN_DLL_EXPORT void EventInit(void *pData, int iDataSize)
{
  SPluginsBikeEvent_t *psEventData = static_cast<SPluginsBikeEvent_t*>(pData);

  mBroadcaster->OnEventInit(*psEventData);
}

/* called when bike goes to track */
EXTERN_DLL_EXPORT void RunInit(void *pData, int iDataSize)
{
  SPluginsBikeSession_t *psSessionData = static_cast<SPluginsBikeSession_t*>(pData);
  mBroadcaster->OnRunInit(*psSessionData);
}

/* called when bike leaves the track */
EXTERN_DLL_EXPORT void RunDeinit()
{
  mBroadcaster->OnRunDeinit();
}

/* called when simulation is started / resumed */
EXTERN_DLL_EXPORT void RunStart()
{
  mBroadcaster->OnRunStart();
}

/* called when simulation is paused */
EXTERN_DLL_EXPORT void RunStop()
{
  mBroadcaster->OnRunStop();
}

/* called when a new lap is recorded. This function is optional */
EXTERN_DLL_EXPORT void RunLap(void *pData, int iDataSize)
{
  SPluginsBikeLap_t *psLapData = static_cast<SPluginsBikeLap_t*>(pData);
  mBroadcaster->OnRunLap(*psLapData);
}

/* called when a split is crossed. This function is optional */
EXTERN_DLL_EXPORT void RunSplit(void *pData, int iDataSize)
{
  SPluginsBikeSplit_t *psSplitData = static_cast<SPluginsBikeSplit_t*>(pData);
  mBroadcaster->OnRunSplit(*psSplitData);
}

/* fTime is the ontrack time, in seconds. fPos is the position on centerline, from 0 to 1 */
EXTERN_DLL_EXPORT void RunTelemetry(void *pData, int iDataSize, float fTime, float fPos)
{
  SPluginsBikeData_t *psBikeData = static_cast<SPluginsBikeData_t*>(pData);
  mBroadcaster->OnRunTelemetry(*psBikeData);
}

/* This function is optional */
EXTERN_DLL_EXPORT void TrackCenterline(int iNumSegments, SPluginsTrackSegment_t *pasSegment, void *pRaceData)
{
  CComSafeArray<IUnknown*> saTrackSegs(iNumSegments);
  for (int i = 0; i < iNumSegments; i++)
  {
    SPluginsTrackSegment_t currSeg = pasSegment[i];

    CComPtr<IPluginsTrackSegmentInfo> pTrackSeg;
    pTrackSeg.CoCreateInstance(CLSID_PluginsTrackSegmentInfo);

    pTrackSeg->put_Angle(currSeg.m_fAngle);
    pTrackSeg->put_Length(currSeg.m_fLength);
    pTrackSeg->put_Radius(currSeg.m_fRadius);
    pTrackSeg->put_Start1(currSeg.m_afStart[0]);
    pTrackSeg->put_Start2(currSeg.m_afStart[1]);
    pTrackSeg->put_Type(currSeg.m_iType);

    saTrackSegs[i] = pTrackSeg.Detach();
  }

  mBroadcaster->OnTrackCenterline(saTrackSegs);
}

#if 0

/* called when software is started. This function is optional */
EXTERN_DLL_EXPORT int DrawInit(int *piNumSprites, char **pszSpriteName, int *piNumFonts, char **pszFontName)
{
  /*
    return 0 if pointers are set
  */
  return -1;
}

/* This function is optional */
EXTERN_DLL_EXPORT void Draw(int *piNumQuads, void **ppQuad, int *piNumString, void **ppString)
{
  *piNumQuads = 0;
  *ppQuad = NULL;
  *piNumString = 0;
  *ppString = NULL;
}

#endif

