//
//  Copyright (C) 2014 EllieWare
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

EllieSpeed::Broadcast::IBikeDataBroadcaster^ CreateBroadcaster()
{
  return gcnew EllieSpeed::Broadcast::Broadcaster();
}

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

/*
If compiled as C++, extern "C" must be added to declaration of functions to export

X+ is right, Y+ is top and Z+ is forward.
*/
#define EXTERN_DLL_EXPORT extern "C" _declspec(dllexport)

EXTERN_DLL_EXPORT int GetInterfaceVersion()
{
  // have to call this in a function which does not reference any .NET code
  // as entry to such a function will fire an AppDomain.AssemblyResolve event
  InitialiseNET();

  return 8;
}

EXTERN_DLL_EXPORT char* GetModID()
{
  // must be "gpbikes" which is probably name of simulation
  return "gpbikes";
}

EXTERN_DLL_EXPORT int GetModDataVersion()
{
  return 2;
}

/* called when software is started */
EXTERN_DLL_EXPORT int Startup(char *szSavePath)
{
  CreateBroadcaster()->OnStartup();

  /*
    return value is requested rate
    0 = 100hz; 1 = 50hz; 2 = 20hz; 3 = 10hz; -1 = disable
  */
  return 3;
}

/* called when software is closed */
EXTERN_DLL_EXPORT void Shutdown()
{
  CreateBroadcaster()->OnShutdown();
}

/* called when event is initialized */
EXTERN_DLL_EXPORT void EventInit(void *pData, int iDataSize)
{
  // native data
  SPluginsBikeEvent_t *psEventData = static_cast<SPluginsBikeEvent_t*>(pData);

  // convert to managed data
  EllieSpeed::Broadcast::GPBikes::SPluginsBikeEvent_t data;
  data.RiderName = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szRiderName);
  data.BikeID = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szBikeID);
  data.BikeName = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szBikeName);
  data.NumberOfGears = psEventData->m_iNumberOfGears;
  data.MaxRPM = psEventData->m_fMaxRPM;
  data.Limiter = psEventData->m_fLimiter;
  data.ShiftRPM = psEventData->m_fShiftRPM;
  data.EngineOptTemperature = psEventData->m_fEngineOptTemperature;
  data.EngineTemperatureAlarm = gcnew array<float>(2);
  data.EngineTemperatureAlarm[0] = psEventData->m_afEngineTemperatureAlarm[0];
  data.EngineTemperatureAlarm[1] = psEventData->m_afEngineTemperatureAlarm[1];
  data.MaxFuel = psEventData->m_fMaxFuel;
  data.Category = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szCategory);
  data.TrackID = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szTrackID);
  data.TrackName = Marshal::PtrToStringAnsi((IntPtr)psEventData->m_szTrackName);
  data.TrackLength = psEventData->m_fTrackLength;

  CreateBroadcaster()->OnEventInit(data);
}

/* called when bike goes to track */
EXTERN_DLL_EXPORT void RunInit(void *pData, int iDataSize)
{
  // native data
  SPluginsBikeSession_t *psSessionData = static_cast<SPluginsBikeSession_t*>(pData);

  // convert to managed data
  EllieSpeed::Broadcast::GPBikes::SPluginsBikeSession_t data;
  data.Session = psSessionData->m_iSession;
  data.Conditions = psSessionData->m_iConditions;
  data.AirTemperature = psSessionData->m_fAirTemperature;
  data.TrackTemperature = psSessionData->m_fTrackTemperature;
  data.SetupFileName = Marshal::PtrToStringAnsi((IntPtr)psSessionData->m_szSetupFileName);

  CreateBroadcaster()->OnRunInit(data);
}

/* called when bike leaves the track */
EXTERN_DLL_EXPORT void RunDeinit()
{
  CreateBroadcaster()->OnRunDeinit();
}

/* called when simulation is started / resumed */
EXTERN_DLL_EXPORT void RunStart()
{
  CreateBroadcaster()->OnRunStart();
}

/* called when simulation is paused */
EXTERN_DLL_EXPORT void RunStop()
{
  CreateBroadcaster()->OnRunStop();
}

/* called when a new lap is recorded. This function is optional */
EXTERN_DLL_EXPORT void RunLap(void *pData, int iDataSize)
{
  // native data
  SPluginsBikeLap_t *psLapData = static_cast<SPluginsBikeLap_t*>(pData);

  // convert to managed data
  EllieSpeed::Broadcast::GPBikes::SPluginsBikeLap_t data;
  data.LapTime = psLapData->m_iLapTime;
  data.Best = psLapData->m_iBest;
  data.LapNum = psLapData->m_iLapNum;

  CreateBroadcaster()->OnRunLap(data);
}

/* called when a split is crossed. This function is optional */
EXTERN_DLL_EXPORT void RunSplit(void *pData, int iDataSize)
{
  // native data
  SPluginsBikeSplit_t *psSplitData = static_cast<SPluginsBikeSplit_t*>(pData);

  // convert to managed data
  EllieSpeed::Broadcast::GPBikes::SPluginsBikeSplit_t data;
  data.Split = psSplitData->m_iSplit;
  data.SplitTime = psSplitData->m_iSplitTime;
  data.BestDiff = psSplitData->m_iBestDiff;

  CreateBroadcaster()->OnRunSplit(data);
}

/* fTime is the ontrack time, in seconds. fPos is the position on centerline, from 0 to 1 */
EXTERN_DLL_EXPORT void RunTelemetry(void *pData, int iDataSize, float fTime, float fPos)
{
  // native data
  SPluginsBikeData_t *psBikeData = static_cast<SPluginsBikeData_t*>(pData);

  // convert to managed data
  EllieSpeed::Broadcast::GPBikes::SPluginsBikeData_t data;
  data.TrackTime = fTime;
  data.TrackPosition = fPos;
  data.RPM = psBikeData->m_fRPM;
  data.EngineTemperature = psBikeData->m_fEngineTemperature;
  data.WaterTemperature = psBikeData->m_fWaterTemperature;
  data.Gear = psBikeData->m_iGear;
  data.Fuel = psBikeData->m_fFuel;
  data.Speedometer = psBikeData->m_fSpeedometer;
  data.PosX = psBikeData->m_fPosX;
  data.PosY = psBikeData->m_fPosY;
  data.PosZ = psBikeData->m_fPosZ;
  data.VelocityX = psBikeData->m_fVelocityX;
  data.VelocityY = psBikeData->m_fVelocityY;
  data.VelocityZ = psBikeData->m_fVelocityZ;
  data.AccelerationX = psBikeData->m_fAccelerationX;
  data.AccelerationY = psBikeData->m_fAccelerationY;
  data.AccelerationZ = psBikeData->m_fAccelerationZ;
  data.Rot = gcnew array<float>(9);
  data.Rot[0] = psBikeData->m_aafRot[0][0];
  data.Rot[1] = psBikeData->m_aafRot[0][1];
  data.Rot[2] = psBikeData->m_aafRot[0][2];
  data.Rot[3] = psBikeData->m_aafRot[1][0];
  data.Rot[4] = psBikeData->m_aafRot[1][1];
  data.Rot[5] = psBikeData->m_aafRot[1][2];
  data.Rot[6] = psBikeData->m_aafRot[2][0];
  data.Rot[7] = psBikeData->m_aafRot[2][1];
  data.Rot[8] = psBikeData->m_aafRot[2][2];
  data.Yaw = psBikeData->m_fYaw;
  data.Pitch = psBikeData->m_fPitch;
  data.Roll = psBikeData->m_fRoll;
  data.YawVelocity = psBikeData->m_fYawVelocity;
  data.PitchVelocity = psBikeData->m_fPitchVelocity;
  data.RollVelocity = psBikeData->m_fRollVelocity;
  data.SuspNormLength = gcnew array<float>(2);
  data.SuspNormLength[0] = psBikeData->m_afSuspNormLength[0];
  data.SuspNormLength[1] = psBikeData->m_afSuspNormLength[1];
  data.Crashed = psBikeData->m_iCrashed;
  data.Steer = psBikeData->m_fSteer;
  data.Throttle = psBikeData->m_fThrottle;
  data.FrontBrake = psBikeData->m_fFrontBrake;
  data.RearBrake = psBikeData->m_fRearBrake;
  data.Clutch = psBikeData->m_fClutch;
  data.WheelSpeed = gcnew array<float>(2);
  data.WheelSpeed[0] = psBikeData->m_afWheelSpeed[0];
  data.WheelSpeed[1] = psBikeData->m_afWheelSpeed[1];
  data.PitLimiter= psBikeData->m_iPitLimiter;
  data.EngineMapping = Marshal::PtrToStringAnsi((IntPtr)psBikeData->m_szEngineMapping);

  CreateBroadcaster()->OnRunTelemetry(data);
}

/* This function is optional */
/* called when a track is selected */
EXTERN_DLL_EXPORT void TrackCenterline(int iNumSegments, SPluginsTrackSegment_t *pasSegment, void *pRaceData)
{
  // convert to managed data
  array<EllieSpeed::Broadcast::GPBikes::SPluginsTrackSegment_t>^ data = gcnew array<EllieSpeed::Broadcast::GPBikes::SPluginsTrackSegment_t>(iNumSegments);

  for (int i = 0; i < iNumSegments; i++)
  {
    SPluginsTrackSegment_t currSeg = pasSegment[i];

    data[i].Type = currSeg.m_iType;
    data[i].Length =currSeg.m_fLength;
    data[i].Radius = currSeg.m_fRadius;
    data[i].Angle = currSeg.m_fAngle;
    data[i].Start = gcnew array<float>(2);
    data[i].Start[0] = currSeg.m_afStart[0];
    data[i].Start[1] = currSeg.m_afStart[1];
  }

  CreateBroadcaster()->OnTrackCenterline(data);
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

