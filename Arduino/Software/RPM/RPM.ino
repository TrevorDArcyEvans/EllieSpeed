//  http://bildr.org/2011/03/high-power-control-with-arduino-and-tip120/
// PWM pin connected to base of transistor
const int RPMpin = 9;

// the setup routine runs once when you press reset:
void setup()
{
  // initialize the digital pin as an output.
  pinMode(RPMpin, OUTPUT);
}

// the loop routine runs over and over again forever
void loop()
{
  // read the input on analog pin 0
  // gives a value [0, 1023] ie 10 bit resolution
  int rpm = analogRead(A0);

  Serial.println(rpm);

  // convert to [0, 255]
  analogWrite(RPMpin, rpm / 4);

  delay(1);
}
