// EllieSpeed.Plugin.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "GPBikes.h"

/*
If compiled as C++, extern "C" must be added to declaration of functions to export

X+ is right, Y+ is top and Z+ is forward.
*/

__declspec(dllexport) char *GetModID()
{
  return "gpbikes";
}

__declspec(dllexport) int GetModDataVersion()
{
  return 2;
}

__declspec(dllexport) int GetInterfaceVersion()
{
  return 8;
}

FILE *g_hTestLog;

/* called when software is started */
__declspec(dllexport) int Startup(char *_szSavePath)
{
  g_hTestLog = fopen("gpbikes_log.txt","wt");
  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Startup\n");
  }

  /*
    return value is requested rate
    0 = 100hz; 1 = 50hz; 2 = 20hz; 3 = 10hz; -1 = disable
  */
  return 3;
}

/* called when software is closed */
__declspec(dllexport) void Shutdown()
{
  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Shutdown\n");
    fclose(g_hTestLog);
    g_hTestLog = NULL;
  }
}

/* called when event is initialized */
__declspec(dllexport) void EventInit(void *_pData, int _iDataSize)
{
  SPluginsBikeEvent_t *psEventData;

  psEventData = (SPluginsBikeEvent_t*)_pData;

  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Event init\n");
    fprintf(g_hTestLog,"Bike %s\n",psEventData->m_szBikeName);
    if (psEventData->m_fLimiter > 0)
    {
      fprintf(g_hTestLog,"Limiter: %d\n",(int)psEventData->m_fLimiter);
    }
    fprintf(g_hTestLog,"Max RPM: %d\n",(int)psEventData->m_fMaxRPM);
    fprintf(g_hTestLog,"Category: %s\n",psEventData->m_szCategory);
    fprintf(g_hTestLog,"Track: %s\n",psEventData->m_szTrackName);
    fprintf(g_hTestLog,"Track Length: %d\n",(int)psEventData->m_fTrackLength);
  }
}

/* called when bike goes to track */
__declspec(dllexport) void RunInit(void *_pData, int _iDataSize)
{
  SPluginsBikeSession_t *psSessionData;

  psSessionData = (SPluginsBikeSession_t*)_pData;

  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Run init\n");

    switch (psSessionData->m_iSession)
    {
    case 0:
      fprintf(g_hTestLog,"Testing\n");
      break;
    case 1:
      fprintf(g_hTestLog,"Practice\n");
      break;
    case 2:
      fprintf(g_hTestLog,"Qualify\n");
      break;
    case 3:
      fprintf(g_hTestLog,"Warmup\n");
      break;
    case 4:
      fprintf(g_hTestLog,"Race\n");
      break;
    }

    switch (psSessionData->m_iConditions)
    {
    case 0:
      fprintf(g_hTestLog,"Sunny\n");
      break;
    case 1:
      fprintf(g_hTestLog,"Cloudy\n");
      break;
    case 2:
      fprintf(g_hTestLog,"Rainy\n");
      break;
    }

    fprintf(g_hTestLog,"Air temp: %f; Track temp: %f\n",psSessionData->m_fAirTemperature,psSessionData->m_fTrackTemperature);
  }
}

/* called when bike leaves the track */
__declspec(dllexport) void RunDeinit()
{
  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Run deinit\n");
  }
}

/* called when simulation is started / resumed */
__declspec(dllexport) void RunStart()
{
  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Run start\n");
  }
}

/* called when simulation is paused */
__declspec(dllexport) void RunStop()
{
  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Run stop\n");
  }
}

/* called when a new lap is recorded. This function is optional */
__declspec(dllexport) void RunLap(void *_pData, int _iDataSize)
{
  SPluginsBikeLap_t *psLapData;

  psLapData = (SPluginsBikeLap_t*)_pData;

  if (g_hTestLog)
  {
    int iMinutes;
    int iSeconds;
    int iMilliseconds;

    iMinutes = psLapData->m_iLapTime / 60000;
    iSeconds = (psLapData->m_iLapTime / 1000) % 60;
    iMilliseconds = psLapData->m_iLapTime % 1000;
    if (iMinutes)
    {
      fprintf(g_hTestLog,"New lap: %d'%02d.%03d\n",iMinutes,iSeconds,iMilliseconds);
    }
    else
    {
      fprintf(g_hTestLog,"New lap: %d.%03d\n",iSeconds,iMilliseconds);
    }
  }
}

/* called when a split is crossed. This function is optional */
__declspec(dllexport) void RunSplit(void *_pData, int _iDataSize)
{
  SPluginsBikeSplit_t *psSplitData;

  psSplitData = (SPluginsBikeSplit_t*)_pData;

  if (g_hTestLog)
  {
    int iMinutes;
    int iSeconds;
    int iMilliseconds;

    iMinutes = psSplitData->m_iSplitTime / 60000;
    iSeconds = (psSplitData->m_iSplitTime / 1000) % 60;
    iMilliseconds = psSplitData->m_iSplitTime % 1000;
    if (iMinutes)
    {
      fprintf(g_hTestLog,"Split %d: %d'%02d.%03d\n",psSplitData->m_iSplit + 1,iMinutes,iSeconds,iMilliseconds);
    }
    else
    {
      fprintf(g_hTestLog,"Split %d: %d.%03d\n",psSplitData->m_iSplit + 1,iSeconds,iMilliseconds);
    }
  }
}

/* _fTime is the ontrack time, in seconds. _fPos is the position on centerline, from 0 to 1 */
__declspec(dllexport) void RunTelemetry(void *_pData, int _iDataSize, float _fTime, float _fPos)
{
  SPluginsBikeData_t *psBikeData;

  psBikeData = (SPluginsBikeData_t*)_pData;

  if (g_hTestLog)
  {
    fprintf(g_hTestLog,"Position: %f%%\n",_fPos * 100);

    fprintf(g_hTestLog,"RPM: %d\n",(int)psBikeData->m_fRPM);
    fprintf(g_hTestLog,"Gear: %d\n",psBikeData->m_iGear);

    fprintf(g_hTestLog,"Throttle: %d%%\n",(int)(psBikeData->m_fThrottle * 100));
    fprintf(g_hTestLog,"Front brake: %d%%\n",(int)(psBikeData->m_fFrontBrake * 100));
    fprintf(g_hTestLog,"Rear brake: %d%%\n",(int)(psBikeData->m_fRearBrake * 100));
    fprintf(g_hTestLog,"Clutch: %d%%\n",(int)(psBikeData->m_fClutch * 100));

    fprintf(g_hTestLog,"Front susp: %f\n",psBikeData->m_afSuspNormLength[0]);
    fprintf(g_hTestLog,"Rear susp: %f\n",psBikeData->m_afSuspNormLength[1]);
    fprintf(g_hTestLog,"Yaw: %f Pitch: %f Roll: %f\n",psBikeData->m_fYaw,psBikeData->m_fPitch,psBikeData->m_fRoll);

    fprintf(g_hTestLog,"Speed: %d km/h\n",(int)(psBikeData->m_fSpeedometer * 3.6));
  }
}

/* called when software is started. This function is optional */
__declspec(dllexport) int DrawInit(int *_piNumSprites, char **_pszSpriteName, int *_piNumFonts, char **_pszFontName)
{
  /*
    return 0 if pointers are set
  */
  return -1;
}

/* This function is optional */
__declspec(dllexport) void TrackCenterline(int _iNumSegments, SPluginsTrackSegment_t *_pasSegment, void *_pRaceData)
{
}

/* This function is optional */
__declspec(dllexport) void Draw(int *_piNumQuads, void **_ppQuad, int *_piNumString, void **_ppString)
{
  *_piNumQuads = 0;
  *_ppQuad = NULL;
  *_piNumString = 0;
  *_ppString = NULL;
}


