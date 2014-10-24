//  http://bildr.org/2011/03/high-power-control-with-arduino-and-tip120/
// PWM pin connected to base of transistor
const int RpmPin = 9;

// TODO  wire shift light
const int ShiftPin = 10;

// TODO  wire neutral light
const int NeutralGearPin = 11;

// the setup routine runs once when you press reset
void setup()
{
  // pen serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial)
  {
    // wait for serial port to connect. Needed for Leonardo only
    NULL;
  }
  
  // initialize the digital pins as an output.
  pinMode(RpmPin, OUTPUT);
  pinMode(ShiftPin, OUTPUT);
  pinMode(NeutralGearPin, OUTPUT);
}

// the loop routine runs over and over again forever
void loop()
{
  if (Serial.available() > 0)
  {
    ProcessSerial();
  }
  
  delay(1);
}

// format of string:
//    RPM,shift,gear
//    [int],[bool],[int]
//
//  RPM = engine RPM [0, 15500]
//  shift = true (1) to turn on shift light
//  gear = current gear, 0 to turn on neutral light
// eg:
//    9250,1,3
void ProcessSerial()
{
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

  // TODO  calibrate RPM --> [0, 255]
  // convert to [0, 255]
  analogWrite(RpmPin, rpm);
  
  digitalWrite(ShiftPin, shift ? HIGH : LOW);
  digitalWrite(NeutralGearPin, !gear ? HIGH : LOW);
}

