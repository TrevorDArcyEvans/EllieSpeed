//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

#include "stdafx.h"
#include "GPBikes.h"

/*
If compiled as C++, extern "C" must be added to declaration of functions to export

X+ is right, Y+ is top and Z+ is forward.
*/
#define EXTERN_DLL_EXPORT extern "C" __declspec(dllexport)

EXTERN_DLL_EXPORT char* GetModID()
{
  return "gpbikes";
}

extern "C" int __declspec(dllexport) GetModDataVersion()
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
  /*
    return value is requested rate
    0 = 100hz; 1 = 50hz; 2 = 20hz; 3 = 10hz; -1 = disable
  */
  return 3;
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
}

/* called when event is initialized */
EXTERN_DLL_EXPORT void EventInit(void *pData, int _iDataSize)
{
  SPluginsBikeEvent_t *psEventData = static_cast<SPluginsBikeEvent_t*>(pData);
}

/* called when bike goes to track */
EXTERN_DLL_EXPORT void RunInit(void *pData, int _iDataSize)
{
  SPluginsBikeSession_t *psSessionData = static_cast<SPluginsBikeSession_t*>(pData);
}

/* called when bike leaves the track */
EXTERN_DLL_EXPORT void RunDeinit()
{
}

/* called when simulation is started / resumed */
EXTERN_DLL_EXPORT void RunStart()
{
}

/* called when simulation is paused */
EXTERN_DLL_EXPORT void RunStop()
{
}

/* called when a new lap is recorded. This function is optional */
EXTERN_DLL_EXPORT void RunLap(void *pData, int _iDataSize)
{
  SPluginsBikeLap_t *psLapData = static_cast<SPluginsBikeLap_t*>(pData);
}

/* called when a split is crossed. This function is optional */
EXTERN_DLL_EXPORT void RunSplit(void *pData, int _iDataSize)
{
  SPluginsBikeSplit_t *psSplitData = static_cast<SPluginsBikeSplit_t*>(pData);
}

/* _fTime is the ontrack time, in seconds. _fPos is the position on centerline, from 0 to 1 */
EXTERN_DLL_EXPORT void RunTelemetry(void *pData, int _iDataSize, float _fTime, float _fPos)
{
  SPluginsBikeData_t *psBikeData = static_cast<SPluginsBikeData_t*>(pData);
}

/* This function is optional */
EXTERN_DLL_EXPORT void TrackCenterline(int _iNumSegments, SPluginsTrackSegment_t *pasSegment, void *pRaceData)
{
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

