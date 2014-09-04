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

extern "C" char *GetModID()
{
  return "gpbikes";
}

extern "C" int GetModDataVersion()
{
  return 2;
}

extern "C" int GetInterfaceVersion()
{
  return 8;
}

/* called when software is started */
extern "C" int Startup(char *_szSavePath)
{
  /*
    return value is requested rate
    0 = 100hz; 1 = 50hz; 2 = 20hz; 3 = 10hz; -1 = disable
  */
  return 3;
}

/* called when software is closed */
extern "C" void Shutdown()
{
}

/* called when event is initialized */
extern "C" void EventInit(void *_pData, int _iDataSize)
{
  SPluginsBikeEvent_t *psEventData = static_cast<SPluginsBikeEvent_t*>(_pData);
}

/* called when bike goes to track */
extern "C" void RunInit(void *_pData, int _iDataSize)
{
  SPluginsBikeSession_t *psSessionData = static_cast<SPluginsBikeSession_t*>(_pData);
}

/* called when bike leaves the track */
extern "C" void RunDeinit()
{
}

/* called when simulation is started / resumed */
extern "C" void RunStart()
{
}

/* called when simulation is paused */
extern "C" void RunStop()
{
}

/* called when a new lap is recorded. This function is optional */
extern "C" void RunLap(void *_pData, int _iDataSize)
{
  SPluginsBikeLap_t *psLapData = static_cast<SPluginsBikeLap_t*>(_pData);
}

/* called when a split is crossed. This function is optional */
extern "C" void RunSplit(void *_pData, int _iDataSize)
{
  SPluginsBikeSplit_t *psSplitData = static_cast<SPluginsBikeSplit_t*>(_pData);
}

/* _fTime is the ontrack time, in seconds. _fPos is the position on centerline, from 0 to 1 */
extern "C" void RunTelemetry(void *_pData, int _iDataSize, float _fTime, float _fPos)
{
  SPluginsBikeData_t *psBikeData = static_cast<SPluginsBikeData_t*>(_pData);
}

#if 0

/* called when software is started. This function is optional */
extern "C" int DrawInit(int *_piNumSprites, char **_pszSpriteName, int *_piNumFonts, char **_pszFontName)
{
  /*
    return 0 if pointers are set
  */
  return -1;
}

/* This function is optional */
extern "C" void TrackCenterline(int _iNumSegments, SPluginsTrackSegment_t *_pasSegment, void *_pRaceData)
{
}

/* This function is optional */
extern "C" void Draw(int *_piNumQuads, void **_ppQuad, int *_piNumString, void **_ppString)
{
  *_piNumQuads = 0;
  *_ppQuad = NULL;
  *_piNumString = 0;
  *_ppString = NULL;
}

#endif

