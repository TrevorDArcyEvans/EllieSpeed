//
//  Copyright (C) 2014 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

using System;
using System.Runtime.InteropServices;

namespace EllieSpeed.Broadcast
{
  public class GPBikes
  {
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeEvent_t
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string RiderName;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string BikeID;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string BikeName;

      public int NumberOfGears;

      public float MaxRPM;

      public float Limiter;

      public float ShiftRPM;

      /*  degrees Celsius  */
      public float EngineOptTemperature;

      /*  degrees Celsius. Lower and upper limits  */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] EngineTemperatureAlarm;

      /*  liters  */
      public float MaxFuel;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string Category;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string TrackID;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string TrackName;

      /*  centerline length. meters  */
      public float TrackLength;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeSession_t
    {
      /*  0 = testing; 1 = practice; 2 = qualify; 3 = warmup; 4 = race  */
      public int Session;

      /*  0 = sunny; 1 = cloudy; 2 = rainy  */
      public int Conditions;

      /*  degrees Celsius  */
      public float AirTemperature;

      /*  degrees Celsius  */
      public float TrackTemperature;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string SetupFileName;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeData_t
    {
      /*  engine rpm  */
      public float RPM;

      /*  degrees Celsius  */
      public float EngineTemperature;

      /*  degrees Celsius  */
      public float WaterTemperature;

      /*  0 = Neutral  */
      public int Gear;

      /*  liters  */
      public float Fuel;

      /*  meters/second  */
      public float Speedometer;

      /*  world position of a reference point attached to chassis ( not CG )  */
      public float PosX;
      public float PosY;
      public float PosZ;

      /*  velocity of CG  in world coordinates. meters/second  */
      public float VelocityX;
      public float VelocityY;
      public float VelocityZ;

      /*  acceleration of CG  local to chassis rotation expressed in G ( 9.81 m/s2 ) and averaged over the latest 10ms  */
      public float AccelerationX;
      public float AccelerationY;
      public float AccelerationZ;

      /*  rotation  matrix of the chassis.  It incorporates lean and wheeling  */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.R4)]
      public float[] Rot;

      /*  degrees,  -180  to  180  */
      public float Yaw;
      public float Pitch;
      public float Roll;

      /*  degress / second  */
      public float YawVelocity;
      public float PitchVelocity;
      public float RollVelocity;

      /*  normalized suspensions length.  0 = front; 1 = rear  */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] SuspNormLength;

      /*  1 = rider is detached from bike  */
      public int Crashed;

      /*  degrees.  Negative = right  */
      public float Steer;

      /*  0  to  1  */
      public float Throttle;

      /*  0  to  1  */
      public float FrontBrake;

      /*  0  to  1  */
      public float RearBrake;

      /*  0  to  1.  0 =  Fully engaged  */
      public float Clutch;

      /*  meters/second.  0 = front; 1  = rear  */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] WheelSpeed;

      /*  1 = pit limiter is activated  */
      public int PitLimiter;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string EngineMapping;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginsBikeDataEx_t
    {
      /*  TrackTime is the ontrack time, in seconds  */
      public float TrackTime;

      /*  Position on centerline, from 0 to 1  */
      public float TrackPosition;

      public SPluginsBikeData_t BikeData;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SPluginsBikeLap_t
    {
      /*  milliseconds  */
      public int LapTime;

      /*  1 = best lap  */
      public int Best;

      public int LapNum;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SPluginsBikeSplit_t
    {
      public int Split;

      /*  milliseconds  */
      public int SplitTime;

      /*  milliseconds.  Difference with best lap  */
      public int BestDiff;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SPluginQuad_t
    {
      /* 0,0 -> top left. 1,1 -> bottom right. counter-clockwise */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.R4)]
      public float[] Pos;

      /* 1 based index in SpriteName buffer. 0 = fill with Color */
      public int Sprite;

      /* ABGR */
      public uint Color;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SPluginString_t
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string String;

      /* 0,0 -> top left. 1,1 -> bottom right */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] Pos;

      /* 1 based index in FontName buffer */
      public int Font;

      public float Size;

      /* 0 = left; 1 = center; 2 = right */
      public int Justify;

      /* ABGR */
      public uint Color;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SPluginsTrackSegment_t
    {
      public int Type;
      public float Length;
      public float Radius;
      public float Angle;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R4)]
      public float[] Start;
    }
  }
}
