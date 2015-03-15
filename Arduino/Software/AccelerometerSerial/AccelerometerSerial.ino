
#include <Arduino.h>

// analog read pins
const int xPin = 3;
const int yPin = 2;
const int zPin = 1;

// The minimum and maximum values that came from
// the accelerometer while standing still
// You very well may need to change these
const int minVal = 263;
const int maxVal = 402;

void setup()
{
  Serial.begin (9600);
  while (!Serial)
  {
    // wait for serial port to connect. Needed for Leonardo only
    NULL;
  }
}

void loop()
{
  // read the analog values from the accelerometer
  int xRead = analogRead(xPin);
  int yRead = analogRead(yPin);
  int zRead = analogRead(zPin);

  Serial.print("x: ");
  Serial.print(xRead);
  Serial.print(" | y: ");
  Serial.print(yRead);
  Serial.print(" | z: ");
  Serial.println(zRead);

  delay(500);
}
