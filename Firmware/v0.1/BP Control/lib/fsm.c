/********************************************************************************
 *    Bibliothek: fsm.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012 
 *
 *    Beschreibung: Status Maschine der Zelle.
 *                   
 *    
 ********************************************************************************/

#include "incl.h" 

enum CELL_STAT CELL_STATUS= INIT_ARM;
unsigned char TEMP_OKAY = 1, PRESS_OKAY = 1, CAM_OKAY = 1;

int firmware_major, firmware_minor;
/*************************************************************************
 *    Funktion: void fsm()
 *
 *    Beschreibung: Steuert die Zelle je nach Zellen Status. 
 * 
 **************************************************************************/
void fsm()
{
  firmware_major=FIRMWARE_MAJOR;
  firmware_minor=FIRMWARE_MINOR;
  switch(CELL_STATUS)
  {
    //Status: INIT
    //Basisinitialisierung (Takt, IO, PWM, LEDs, I2C)
    case INIT_ARM:
      
      pwm_init();  
      PWM1_DISABLE();
      PWM2_DISABLE();
      PWM3_DISABLE();      
      timer_init();
      CELL_STATUS = INIT_EXT;
      break;
      
    case INIT_EXT:
      //Status: INIT_EXT
      //Interner I2C Bus Initialisierung (Temperatur-, Drucksensor, Kamera)
      LED1(LED_SLOW);
      LED2(LED_SLOW);
      
      i2c_init();
      sm5822_init();
      clock_output_init();
      cam_init();
      
      //Test I2C modules
      if(!TEMP_OKAY || !PRESS_OKAY || !CAM_OKAY)
        LED2(LED_FAST);

      CELL_STATUS = WAIT_OP;
      break;

    case WAIT_OP:
      //Status: WAIT_OP
      //Warte auf Betriebsfreigabe
      LED1(LED_OFF);     
      if(!TEMP_OKAY || !PRESS_OKAY || !CAM_OKAY)
        LED2(LED_FAST);
      else      
        LED2(LED_OFF);
      
      //Change to OP
      CELL_STATUS = OP;

      LED1(LED_ON);
      LED2(LED_OFF);
      break;

    case OP:
      //Hauptprogramm
      user_program();
      break;

    default:
      CELL_STATUS = INIT_ARM;
  }
}