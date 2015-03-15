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

// the loop routine runs over and over again forever:
void loop()
{
  // print out the value you read:
  Serial.print(STX);

  ProcessAxis();
  ProcessSlider();
  ProcessButton();
  ProcessPOV();
  ProcessDial();

  Serial.print(ETX);

  // delay in between reads for stability
  delay(1000 / UpdateRateHz);
}
