//  http://bildr.org/2011/03/high-power-control-with-arduino-and-tip120/
// PWM pin connected to base of transistor
const int RPMpin = 9;

// the setup routine runs once when you press reset:
void setup()
{
  // initialize the digital pin as an output.
  pinMode(RPMpin, OUTPUT);
}

const int MinAngle = 10;

// the loop routine runs over and over again forever
void loop()
{
  for (int angle = MinAngle; angle < 90; angle++)
  {    
    // convert to [0, 255]
    analogWrite(RPMpin, sin(angle / 90.0 * PI / 2.0) * 225);

    delay(10);
  }
  
  delay(50);
  analogWrite(RPMpin, MinAngle);
  delay(500);
}
