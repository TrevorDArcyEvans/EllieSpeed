#include <Wire.h>
#include <LiquidCrystal.h>

// F Malpartida's NewLiquidCrystal library
#include <LiquidCrystal_I2C.h>

// i2c Address for controller
const uint8_t I2C_ADDR = 0x27;

const uint8_t BACKLIGHT_PIN = 7;
const uint8_t En_pin = 2;
const uint8_t Rw_pin = 1;
const uint8_t Rs_pin = 0;
const uint8_t D4_pin = 4;
const uint8_t D5_pin = 5;
const uint8_t D6_pin = 6;
const uint8_t D7_pin = 7;
const uint8_t BL_PIN = 3;
const t_backlighPol BL_POL = POSITIVE;

const uint8_t LCD_COLS = 16;
const uint8_t LCD_ROWS = 2;

const uint8_t LED_OFF = 0;
const uint8_t LED_ON = 1;

// create LCD with all the correct pin mappings, courtesy of i2cLCDguesser
LiquidCrystal_I2C  lcd(I2C_ADDR, En_pin, Rw_pin, Rs_pin, D4_pin, D5_pin, D6_pin, D7_pin, BL_PIN, BL_POL);

void setup()
{
  // initialize the lcd
  lcd.begin(LCD_COLS, LCD_ROWS);

  // Switch on the backlight
  lcd.setBacklight(LED_ON);
}

void loop()
{
  // reset the display
  lcd.clear();
  delay(250);
  lcd.home();

  // read the input on analog pin 0:
  int sensorValue = analogRead(A0);

  // print on the LCD
  lcd.backlight();
  lcd.setCursor(0, 0);
  lcd.print("Value = ");
  lcd.print(sensorValue);
  delay(750);
}
