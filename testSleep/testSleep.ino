#include <avr/power.h>
#include <avr/sleep.h>

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete

void setup()
{
    pinMode(LED_BUILTIN, OUTPUT);
    Serial.begin(9600);
    inputString.reserve(200);
}

void loop()
{
    if (stringComplete) 
    {
      stringComplete = false;
      //go to sleep
      if (inputString == "Sleep")
      {
        inputString = "";
        sleepNow();
      }
      inputString = "";
    }
    digitalWrite(LED_BUILTIN, HIGH);
    delay(500);
    digitalWrite(LED_BUILTIN, LOW);
    delay(500);
    
}

void serialEvent() 
{
   while (Serial.available()) 
   {
      // get the new byte:
      char inChar = (char)Serial.read();
      
      // if the incoming character is a newline, set a flag
      // so the main loop can do something about it:
      if (inChar == '\n') 
      {
          stringComplete = true;
      }
      else 
      {
        // add it to the inputString:
        inputString += inChar;
      }
  }
}

void sleepNow()
{
  set_sleep_mode(SLEEP_MODE_IDLE);

  sleep_enable();

  power_adc_disable();
  power_spi_disable();
  power_timer0_disable();
  power_timer1_disable();
  power_timer2_disable();
  power_twi_disable();

  sleep_mode();

  sleep_disable();

  power_all_enable();
}

