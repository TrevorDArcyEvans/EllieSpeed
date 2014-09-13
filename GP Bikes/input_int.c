
/*
If compiled as C++, extern "C" must be added to declaration of functions to export
*/

typedef struct
{
	char m_szName[100];
	char m_szUUID[37];						/* universally unique identifier */
	int m_iID;								/* internal unique ID */
	char m_iNumAxis;						/* number of axis */
	short m_aaiAxisRange[6][3];				/* min, max and center value of each axis */
	char m_iNumSliders;						/* number of sliders */
	short m_aiSliderRange[6];				/* max value of each slider */
	char m_iNumButtons;						/* number of buttons */
	char m_iNumPOV;							/* number of POVs */
	char m_iNumDials;						/* number of dials */
	char m_aiDialRange[8];					/* max value of dials */
} SControllerInfo_t;

typedef struct
{
	short m_aiAxis[6];
	short m_aiSlider[6];
	char m_aiButton[32];
	char m_aiPOV[2];
	char m_aiDial[8];
} SControllerData_t;

__declspec(dllexport) int Version()
{
	return 2;
}

/* called when software is started. If return value is not 0, the plugin is disabled */
__declspec(dllexport) int Startup()
{
	return 0;	
}

/* called when software is closed */
__declspec(dllexport) void Shutdown()
{
}

/* called every rendering frame. This function is optional */
__declspec(dllexport) void Update()
{

}

/* called when a control is queried */
__declspec(dllexport) void Reset()
{
}

/* called every few seconds to support hot plugging. The return value is the number of active controllers */
__declspec(dllexport) int GetNumControllers()
{
	return 0;
}

/* _iIndex is the 0 based controller index. _psInfo must be filled with controller info */
__declspec(dllexport) int GetControllerInfo(int _iIndex,SControllerInfo_t *_psInfo)
{

	return 0;
}

/* _iID is the unique controller ID. _psData must be filled with controller data */
__declspec(dllexport) int GetControllerData(int _iID,SControllerData_t *_psData)
{

	return 0;
}