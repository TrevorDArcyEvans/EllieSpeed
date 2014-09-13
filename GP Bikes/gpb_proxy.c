

#include <stdio.h>
#include <direct.h>		// _getcwd
#include <Windows.h>

typedef struct
{
	char m_szRiderName[100];
	char m_szBikeID[100];
	char m_szBikeName[100];
	int m_iNumberOfGears;
	float m_fMaxRPM;
	float m_fLimiter;
	float m_fShiftRPM;
	float m_fEngineOptTemperature;
	float m_afEngineTemperatureAlarm[2];
	float m_fMaxFuel;
	char m_szCategory[100];
	char m_szTrackID[100];
	char m_szTrackName[100];
	float m_fTrackLength;
} SPluginsBikeEvent_t;

typedef struct
{
	int m_iSession;
	int m_iConditions;
	float m_fAirTemperature;
	float m_fTrackTemperature;
	char m_szSetupFileName[100];
} SPluginsBikeSession_t;

typedef struct
{
	float m_fRPM;
	float m_fEngineTemperature;
	float m_fWaterTemperature;
	int m_iGear;
	float m_fFuel;
	float m_fSpeedometer;
	float m_fPosX,m_fPosY,m_fPosZ;
	float m_fVelocityX,m_fVelocityY,m_fVelocityZ;
	float m_fAccelerationX,m_fAccelerationY,m_fAccelerationZ;
	float m_aafRot[3][3];
	float m_fYaw,m_fPitch,m_fRoll;
	float m_fYawVelocity,m_fPitchVelocity,m_fRollVelocity;
	float m_afSuspNormLength[2];
	int m_iCrashed;
	float m_fSteer;
	float m_fThrottle;
	float m_fFrontBrake;
	float m_fRearBrake;
	float m_fClutch;
	float m_afWheelSpeed[2];
	int m_iPitLimiter;
	char m_szEngineMapping[100];
} SPluginsBikeData_t;

typedef struct
{
	int m_iLapTime;
	int m_iBest;
	int m_iLapNum;
} SPluginsBikeLap_t;

typedef struct
{
	int m_iSplit;
	int m_iSplitTime;
	int m_iBestDiff;
} SPluginsBikeSplit_t;

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

HANDLE g_hMapFile;
void *g_pBuffer;

typedef struct
{
	int m_iVersion;
	int m_iState;		/* -1: software not running; 0: software running; 1: on-track, simulation paused; 2: on-track, simulation running */
	SPluginsBikeEvent_t m_sEvent;
	SPluginsBikeSession_t m_sSession;
	SPluginsBikeLap_t m_sLap;
	int m_iSplit;
	SPluginsBikeSplit_t m_sSplit;
	SPluginsBikeData_t m_sData;
	float m_fTime;
	float m_fPos;
} SProxyData_t;

SProxyData_t g_sData;

__declspec(dllexport) int Startup(char *_szSavePath)
{
	char szCurrentDir[_MAX_PATH];
	char szIniFilePath[_MAX_PATH];
	int iDisable;
	int iSampleRate;
	int iRet;

	_getcwd(szCurrentDir,_MAX_PATH);
	sprintf(szIniFilePath,"%s\\%s",szCurrentDir,"proxy.ini");

	iDisable = GetPrivateProfileInt("params","disable",0,szIniFilePath);
	if (iDisable)
	{
		return -1;
	}

	iSampleRate = GetPrivateProfileInt("params","sample_rate",10,szIniFilePath);
	switch (iSampleRate)
	{
	case 50:
		iRet = 1;
		break;
	case 20:
		iRet = 2;
		break;
	case 10:
		iRet = 3;
		break;
	default:
		iRet = 0;
		break;
	}

	/* initialize data */
	memset(&g_sData,0,sizeof(SProxyData_t));

	g_sData.m_iVersion = 1;

	g_hMapFile = CreateFileMapping(INVALID_HANDLE_VALUE,NULL,PAGE_READWRITE,0,sizeof(SProxyData_t),"Local\\GPBProxyObject");
	if (g_hMapFile)
	{
		g_pBuffer = MapViewOfFile(g_hMapFile,FILE_MAP_WRITE,0,0,sizeof(SProxyData_t));
	}

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));

	return iRet;
}

__declspec(dllexport) void Shutdown()
{
	g_sData.m_iState = -1;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));

	if (g_hMapFile)
	{
		UnmapViewOfFile(g_pBuffer);
		g_pBuffer = NULL;

		CloseHandle(g_hMapFile);
		g_hMapFile = NULL;
	}
}

__declspec(dllexport) void EventInit(void *_pData,int _iDataSize)
{
	SPluginsBikeEvent_t *psEventData;

	psEventData = (SPluginsBikeEvent_t*)_pData;

	memcpy(&g_sData.m_sEvent,psEventData,sizeof(SPluginsBikeEvent_t));

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunInit(void *_pData,int _iDataSize)
{
	SPluginsBikeSession_t *psSessionData;

	psSessionData = (SPluginsBikeSession_t*)_pData;

	memcpy(&g_sData.m_sSession,psSessionData,sizeof(SPluginsBikeSession_t));

	memset(&g_sData.m_sLap,0,sizeof(SPluginsBikeLap_t));

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunDeinit()
{
	g_sData.m_iState = 0;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunStart()
{

}

__declspec(dllexport) void RunStop()
{
	g_sData.m_iState = 1;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunLap(void *_pData,int _iDataSize)
{
	SPluginsBikeLap_t *psLapData;

	psLapData = (SPluginsBikeLap_t*)_pData;

	memcpy(&g_sData.m_sLap,psLapData,sizeof(SPluginsBikeLap_t));

	g_sData.m_iSplit = 0;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunSplit(void *_pData,int _iDataSize)
{
	SPluginsBikeSplit_t *psSplitData;

	psSplitData = (SPluginsBikeSplit_t*)_pData;

	memcpy(&g_sData.m_sSplit,psSplitData,sizeof(SPluginsBikeSplit_t));

	g_sData.m_iSplit = 1;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}

__declspec(dllexport) void RunTelemetry(void *_pData,int _iDataSize,float _fTime,float _fPos)
{
	SPluginsBikeData_t *psBikeData;

	psBikeData = (SPluginsBikeData_t*)_pData;

	g_sData.m_iState = 2;

	memcpy(&g_sData.m_sData,psBikeData,sizeof(SPluginsBikeData_t));

	g_sData.m_fTime = _fTime;
	g_sData.m_fPos = _fPos;

	if (g_pBuffer)
		memcpy(g_pBuffer,&g_sData,sizeof(SProxyData_t));
}
