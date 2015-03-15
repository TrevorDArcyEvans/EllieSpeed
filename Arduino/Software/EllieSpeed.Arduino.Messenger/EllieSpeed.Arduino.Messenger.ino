//
//  Copyright (C) 2015 EllieSpeed
//
//  All rights reserved
//
//  www.EllieSpeed.com
//

#include <Arduino.h>

//ASCII-Code 02, text representation of the STX code
const String STX = "\x02";

//ASCII-Code 03, text representation of the ETX code
const String ETX = "\x03";

//Used as RS code
const String RS = "$";

const int UpdateRateHz = 20;

// PWM pin connected to base of transistor
const int RpmPin = 9;

// TODO  wire shift light
const int ShiftPin = 10;

// TODO  wire neutral light
const int NeutralGearPin = 11;

// the setup routine runs once when you press reset:
void setup()
{
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  while (!Serial)
  {
    // wait for serial port to connect. Needed for Leonardo only
    NULL;
  }

  // initialize the digital pins as an output
  pinMode(RpmPin, OUTPUT);
  pinMode(ShiftPin, OUTPUT);
  pinMode(NeutralGearPin, OUTPUT);
}

void ProcessAxis()
{
  // Axis x6 short
  Serial.print(analogRead(A0)); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
}

void ProcessSlider()
{
  // Slider x6 short
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
}

void ProcessButton()
{
  // Button x32 byte
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
}

void ProcessPOV()
{
  // POV x2 byte
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
}

void ProcessDial()
{
  // Dial x8 byte
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
  Serial.print(0); Serial.print(RS);
}

void ProcessSerialOutput()
{
  // print out the value you read:
  Serial.print(STX);

  ProcessAxis();
  ProcessSlider();
  ProcessButton();
  ProcessPOV();
  ProcessDial();

  Serial.print(ETX);
}

void ProcessSerialInput()
{
  // format of string:
  //    RPMval,shift,gear
  //    [int],[bool],[int]
  //
  //  RPMval = engine RPM as a value [0, 255]
  //            This will be sent directly to the tachometer
  //            via the PWM pin.  The host PC is responsible
  //            for mapping engine RPM to [0, 255].
  //  shift  = true (1) to turn on shift light
  //  gear   = current gear [0, 6]
  //            0 to turn on neutral light
  // eg:
  //    180,1,3

  const char Separator = ',';
  const char Terminator = '\n';

  String bikeInfo = Serial.readStringUntil(Terminator);

  int firstSepIdx = bikeInfo.indexOf(Separator);
  int secondSepIdx = bikeInfo.indexOf(Separator, firstSepIdx + 1);

  String rpmStr = bikeInfo.substring(0, firstSepIdx);
  String shiftStr = bikeInfo.substring(firstSepIdx + 1, secondSepIdx);
  String gearStr = bikeInfo.substring(secondSepIdx + 1); // To the end of the string

  int rpm = rpmStr.toInt();
  boolean shift = shiftStr.toInt();
  int gear = gearStr.toInt();

  Serial.println(rpm);
  Serial.println(shift);
  Serial.println(gear);

  analogWrite(RpmPin, rpm);
  digitalWrite(ShiftPin, shift ? HIGH : LOW);
  digitalWrite(NeutralGearPin, !gear ? HIGH : LOW);
}

// the loop routine runs over and over again forever:
void loop()
{
  if (Serial.available() > 0)
  {
    ProcessSerialInput();
  }

  ProcessSerialOutput();

  // delay in between reads for stability
  delay(1000 / UpdateRateHz);
}
