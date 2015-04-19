#include <Arduino.h>

const uint8_t BuzzerPin = 9;
const uint8_t Xaxis = A0;
const uint8_t Yaxis = A1;

void setup()
{
	Serial.begin(9600);

	// initialize the digital pin as an output.
	// Pin 13 has an LED connected on most Arduino boards:
	pinMode(13, OUTPUT);
}

void loop()
{
  int xVal = analogRead(Xaxis);
  int yVal = analogRead(Yaxis);
  Serial.print(xVal, DEC);
  Serial.print(" , ");
  Serial.print(yVal, DEC);
  Serial.println();

  int freq = 100 + xVal + (512 - yVal);

  digitalWrite(13, HIGH);
	tone(BuzzerPin, freq);
	delay(100);
  digitalWrite(13, LOW);
	noTone(BuzzerPin);
	delay(100);
}
