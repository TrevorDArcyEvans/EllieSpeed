/*
 HC-SR04 Ping distance sensor
 VCC to arduino 5v GND to arduino GND
 Echo to Arduino pin 13
 Trig to Arduino pin 12
 More info at: http://goo.gl/kJ8Gl
 Original code improvements to the Ping sketch sourced from Trollmaker.com
 Some code and wiring inspired by http://en.wikiversity.org/wiki/User:Dstaub/robotcar
 */

#include <Arduino.h>

const uint8_t TriggerPin = 13;
const uint8_t EchoPin = 12;

void setup()
{
  Serial.begin (9600);
  while (!Serial)
  {
    // wait for serial port to connect. Needed for Leonardo only
    NULL;
  }

  pinMode(TriggerPin, OUTPUT);
  pinMode(EchoPin, INPUT);
}

void loop()
{
  digitalWrite(TriggerPin, LOW);
  delayMicroseconds(2);

  digitalWrite(TriggerPin, HIGH);
  delayMicroseconds(10);

  digitalWrite(TriggerPin, LOW);

  // length of round trip pulse in microseconds
  long duration = pulseIn(EchoPin, HIGH);

  // The speed of sound is 340 m/s or 29.1 microseconds per centimeter
  // or 2.91 microseconds per millitmeter.
  // The ping travels out and back, so to find the distance of the
  // object we take half of the distance travelled.
  long distance = duration / 5.82;

  Serial.print(distance);
  Serial.println(" mm");
  delay(500);
}

