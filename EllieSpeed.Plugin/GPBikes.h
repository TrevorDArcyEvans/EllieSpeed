
/*
  Based on GPB reference plugin
*/

/*
If compiled as C++, extern "C" must be added to declaration of functions to export

X+ is right, Y+ is top and Z+ is forward.
*/

typedef struct
{
  char  m_szRiderName[100];
  char  m_szBikeID[100];
  char  m_szBikeName[100];
  int   m_iNumberOfGears;
  float m_fMaxRPM;
  float m_fLimiter;
  float m_fShiftRPM;
  float m_fEngineOptTemperature;          /*  degrees Celsius  */
  float m_afEngineTemperatureAlarm[2];    /*  degrees Celsius. Lower and upper limits  */
  float m_fMaxFuel;                       /*  liters  */
  char  m_szCategory[100];
  char  m_szTrackID[100];
  char  m_szTrackName[100];
  float m_fTrackLength;                   /*  centerline length. meters  */
} SPluginsBikeEvent_t;

typedef struct
{
  int   m_iSession;                       /*  0 = testing; 1 = practice; 2 = qualify; 3 = warmup; 4 = race  */
  int   m_iConditions;                    /*  0 = sunny; 1 = cloudy; 2 = rainy  */
  float m_fAirTemperature;                /*  degrees Celsius  */
  float m_fTrackTemperature;              /*  degrees Celsius  */
  char  m_szSetupFileName[100];
} SPluginsBikeSession_t;

typedef struct
{
  float m_fRPM;                           /*  engine rpm  */
  float m_fEngineTemperature;             /*  degrees Celsius  */
  float m_fWaterTemperature;              /*  degrees Celsius  */
  int   m_iGear;                          /*  0 = Neutral  */
  float m_fFuel;                          /*  liters  */
  float m_fSpeedometer;                   /*  meters/second  */
  float m_fPosX,
        m_fPosY,
        m_fPosZ;                          /*  world position of a reference point attached to chassis ( not CG )  */
  float m_fVelocityX,
        m_fVelocityY,
        m_fVelocityZ;                     /*  velocity of CG  in world coordinates. meters/second  */
  float m_fAccelerationX,
        m_fAccelerationY,
        m_fAccelerationZ;                 /*  acceleration of CG  local to chassis rotation expressed in G ( 9.81 m/s2 ) and averaged over the latest 10ms  */
  float m_aafRot[3][3];                   /*  rotation  matrix of the chassis.  It incorporates lean and wheeling  */
  float m_fYaw,
        m_fPitch,
        m_fRoll;                          /*  degrees,  -180  to  180  */
  float m_fYawVelocity,
        m_fPitchVelocity,
        m_fRollVelocity;                  /*  degress / second  */
  float m_afSuspNormLength[2];            /*  normalized suspensions length.  0 = front; 1 = rear  */
  int   m_iCrashed;                       /*  1 = rider is detached from bike  */
  float m_fSteer;                         /*  degrees.  Negative = right  */
  float m_fThrottle;                      /*  0  to  1  */
  float m_fFrontBrake;                    /*  0  to  1  */
  float m_fRearBrake;                     /*  0  to  1  */
  float m_fClutch;                        /*  0  to  1.  0 =  Fully engaged  */
  float m_afWheelSpeed[2];                /*  meters/second.  0 = front; 1  = rear  */
  int   m_iPitLimiter;                    /*  1 = pit limiter is activated  */
  char  m_szEngineMapping[100];
} SPluginsBikeData_t;

typedef struct
{
  int  m_iLapTime;                        /*  milliseconds  */
  int  m_iBest;                           /*  1 = best lap  */
  int  m_iLapNum;
} SPluginsBikeLap_t;

typedef struct
{
  int  m_iSplit;
  int  m_iSplitTime;                     /*  milliseconds  */
  int  m_iBestDiff;                      /*  milliseconds.  Difference with best lap  */
} SPluginsBikeSplit_t;

typedef struct
{
  float m_aafPos[4][2];                 /* 0,0 -> top left. 1,1 -> bottom right. counter-clockwise */
  int   m_iSprite;                      /* 1 based index in SpriteName buffer. 0 = fill with m_ulColor */
  unsigned long m_ulColor;              /* ABGR */
} SPluginQuad_t;

typedef struct
{
  char  m_szString[100];
  float m_afPos[2];                     /* 0,0 -> top left. 1,1 -> bottom right */
  int   m_iFont;                        /* 1 based index in FontName buffer */
  float m_fSize;
  int   m_iJustify;                     /* 0 = left; 1 = center; 2 = right */
  unsigned long m_ulColor;              /* ABGR */
} SPluginString_t;

typedef struct
{
  int m_iType;
  float m_fLength;
  float m_fRadius;
  float m_fAngle;
  float m_afStart[2];
} SPluginsTrackSegment_t;
