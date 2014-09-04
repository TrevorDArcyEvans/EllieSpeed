//
//  Copyright (C) 2014 EllieWare
//
//  All rights reserved
//
//  www.EllieWare.com
//

using System.Runtime.InteropServices;

namespace EllieSpeed.Interfaces
{
  public class GPBikes
  {
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeEvent_t
    {
      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szRiderName;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szBikeID;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szBikeName;

      public int m_iNumberOfGears;

      public float m_fMaxRPM;

      public float m_fLimiter;

      public float m_fShiftRPM;

      /*  degrees Celsius  */
      public float m_fEngineOptTemperature;

      /*  degrees Celsius. Lower and upper limits  */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] m_afEngineTemperatureAlarm;

      /*  liters  */
      public float m_fMaxFuel;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szCategory;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szTrackID;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szTrackName;

      /*  centerline length. meters  */
      public float m_fTrackLength;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeSession_t
    {
      /*  0 = testing; 1 = practice; 2 = qualify; 3 = warmup; 4 = race  */
      public int m_iSession;

      /*  0 = sunny; 1 = cloudy; 2 = rainy  */
      public int m_iConditions;

      /*  degrees Celsius  */
      public float m_fAirTemperature;

      /*  degrees Celsius  */
      public float m_fTrackTemperature;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szSetupFileName;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeData_t
    {
      /*  engine rpm  */
      public float m_fRPM;

      /*  degrees Celsius  */
      public float m_fEngineTemperature;

      /*  degrees Celsius  */
      public float m_fWaterTemperature;

      /*  0 = Neutral  */
      public int m_iGear;

     /*  liters  */
      public float m_fFuel;

      /*  meters/second  */
      public float m_fSpeedometer;

      /*  world position of a reference point attached to chassis ( not CG )  */
      public float m_fPosX;
      public float m_fPosY;
      public float m_fPosZ;

      /*  velocity of CG  in world coordinates. meters/second  */
      public float m_fVelocityX;
      public float m_fVelocityY;
      public float m_fVelocityZ;

      /*  acceleration of CG  local to chassis rotation expressed in G ( 9.81 m/s2 ) and averaged over the latest 10ms  */
      public float m_fAccelerationX;
      public float m_fAccelerationY;
      public float m_fAccelerationZ;

      /*  rotation  matrix of the chassis.  It incorporates lean and wheeling  */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.R4)]
      public float[] m_aafRot;

      /*  degrees,  -180  to  180  */
      public float m_fYaw;
      public float m_fPitch;
      public float m_fRoll;

      /*  degress / second  */
      public float m_fYawVelocity;
      public float m_fPitchVelocity;
      public float m_fRollVelocity;

      /*  normalized suspensions length.  0 = front; 1 = rear  */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] m_afSuspNormLength;

      /*  1 = rider is detached from bike  */
      public int m_iCrashed;

      /*  degrees.  Negative = right  */
      public float m_fSteer;

      /*  0  to  1  */
      public float m_fThrottle;

      /*  0  to  1  */
      public float m_fFrontBrake;

      /*  0  to  1  */
      public float m_fRearBrake;

      /*  0  to  1.  0 =  Fully engaged  */
      public float m_fClutch;

      /*  meters/second.  0 = front; 1  = rear  */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] m_afWheelSpeed;

      /*  1 = pit limiter is activated  */
      public int m_iPitLimiter;

      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szEngineMapping;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SPluginsBikeLap_t
    {
      /*  milliseconds  */
      public int m_iLapTime;

      /*  1 = best lap  */
      public int m_iBest;

      public int m_iLapNum;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SPluginsBikeSplit_t
    {
      public int m_iSplit;

      /*  milliseconds  */
      public int m_iSplitTime;

      /*  milliseconds.  Difference with best lap  */
      public int m_iBestDiff;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SPluginQuad_t
    {
      /* 0,0 -> top left. 1,1 -> bottom right. counter-clockwise */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.R4)]
      public float[] m_aafPos;

      /* 1 based index in SpriteName buffer. 0 = fill with m_ulColor */
      public int m_iSprite;

      /* ABGR */
      public uint m_ulColor;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginString_t
    {
      [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string m_szString;

      /* 0,0 -> top left. 1,1 -> bottom right */
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] m_afPos;

      /* 1 based index in FontName buffer */
      public int m_iFont;

      public float m_fSize;

      /* 0 = left; 1 = center; 2 = right */
      public int m_iJustify;

      /* ABGR */
      public uint m_ulColor;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SPluginsTrackSegment_t
    {
      public int m_iType;
      public float m_fLength;
      public float m_fRadius;
      public float m_fAngle;
      [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] m_afStart;
    }
  }
}
