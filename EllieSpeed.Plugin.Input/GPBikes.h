/*
If compiled as C++, extern "C" must be added to declaration of functions to export
*/

typedef struct
{
  char m_szName[100];
  char m_szUUID[37];            /* universally unique identifier */
  int m_iID;                    /* internal unique ID */
  char m_iNumAxis;              /* number of axis */
  short m_aaiAxisRange[6][3];   /* min, max and center value of each axis */
  char m_iNumSliders;           /* number of sliders */
  short m_aiSliderRange[6];     /* max value of each slider */
  char m_iNumButtons;           /* number of buttons */
  char m_iNumPOV;               /* number of POVs */
  char m_iNumDials;             /* number of dials */
  char m_aiDialRange[8];        /* max value of dials */
} SControllerInfo_t;

typedef struct
{
  short m_aiAxis[6];
  short m_aiSlider[6];
  char m_aiButton[32];
  char m_aiPOV[2];
  char m_aiDial[8];
} SControllerData_t;
