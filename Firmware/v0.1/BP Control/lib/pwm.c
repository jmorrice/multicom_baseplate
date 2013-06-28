/********************************************************************************
 *    Bibliothek: pwm.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012 
 *
 *    Beschreibung: Enthält alle Funktionen zur Steuerung des PWM Ausgangs.
 *                   
 *    
 ********************************************************************************/

#include "incl.h"

/*************************************************************************
 *    Funktion: void pwm_init()
 *
 *    Beschreibung: - Initialisiert das PWM Modul.
 *
 **************************************************************************/
void pwm_init()
{
  PCLKSEL0_bit.PCLK_PWM1 = 3;          //PCLK an PWM auf 12,5MHz
  PINSEL4_bit.P2_3 = 1;                //PWM1.4 on pin 2.3
  PINSEL4_bit.P2_4 = 1;                //PWM1.5 on pin 2.4
  PINSEL4_bit.P2_5 = 1;                //PWM1.6 on pin 2.5
  PINMODE4_bit.P2_3 = 2;                //neither pull up nor pull down
  PINMODE4_bit.P2_4 = 2;                //neither pull up nor pull down
  PINMODE4_bit.P2_5 = 2;                //neither pull up nor pull down
  FIO2DIR_bit.P2_3 = 1;
  FIO2DIR_bit.P2_4 = 1;
  FIO2DIR_bit.P2_5 = 1;
  
  PWM1MR0 = 100;                        //PWM Freq. 12,5MHz / 100 = 125kHz
  PWM1MR4 = 100;
  PWM1MR5 = 100;
  PWM1MR6 = 100;
  PWM1LER_bit.EM0L = 1;             //latch new values
  PWM1LER_bit.EM4L = 1;             //latch new values
  PWM1LER_bit.EM5L = 1;             //latch new values
  PWM1LER_bit.EM6L = 1;             //latch new values  
  PWM1PCR_bit.PWMENA4 = 0;             //Disable PWM 1.4 output
  PWM1PCR_bit.PWMENA5 = 0;             //Disable PWM 1.5 output
  PWM1PCR_bit.PWMENA6 = 0;             //Disable PWM 1.6 output  

  PWM1TCR_bit.CE = 1;                   //enable counter
  PWM1TCR_bit.CR = 1;                   //reset counter
  PWM1TCR_bit.CR = 0;                   //clear reset
  PWM1TCR_bit.PWMEN = 1;                //Select PWM Mode
  PWM1MCR_bit.PWMMR0R = 1;             //reset tc on mr0 match   
}

/*************************************************************************
 *    Funktion: void PWM_HELLIGKEIT(unsigned int duty)
 *
 *    Beschreibung: - Steuert die Helligkeit der LEDs über das als Parameter angegebene Taktverhältniss.
 *
 **************************************************************************/
void PWM_HELLIGKEIT(unsigned int duty)
{
  PWM1MR4 = duty;
  PWM1MR5 = duty;
  PWM1MR6 = duty;
  PWM1LER_bit.EM4L = 1;             //latch new values
  PWM1LER_bit.EM5L = 1;             //latch new values
  PWM1LER_bit.EM6L = 1;             //latch new values   
}

/*************************************************************************
 *    Funktion: void PWM1/2/3_EN/DISABLE()y)
 *
 *    Beschreibung: - Aktiviert und Deaktiviert das entsprechende PWM Signal
 *
 **************************************************************************/
void PWM1_ENABLE()
{
  PWM1PCR_bit.PWMENA4 = 1;             //Enable PWM 1.4 output
  PINSEL4_bit.P2_3 = 1;                //PWM1.4 on pin 2.3   
}

void PWM2_ENABLE()
{
  PWM1PCR_bit.PWMENA5 = 1;             //Enable PWM 1.5 output
  PINSEL4_bit.P2_4 = 1;                //PWM1.4 on pin 2.3   
}

void PWM3_ENABLE()
{
  PWM1PCR_bit.PWMENA6 = 1;             //Enable PWM 1.6 output
  PINSEL4_bit.P2_5 = 1;                //PWM1.4 on pin 2.3   
}

void PWM1_DISABLE()
{
  PWM1PCR_bit.PWMENA4 = 0;             //Disable PWM 1.4 output
  PINSEL4_bit.P2_3 = 0;                //PWM1.4 on pin 2.3 
}

void PWM2_DISABLE()
{
  PWM1PCR_bit.PWMENA5 = 0;             //Disable PWM 1.5 output
  PINSEL4_bit.P2_4 = 0;                //PWM1.5 on pin 2.4
}

void PWM3_DISABLE()
{
  PWM1PCR_bit.PWMENA6 = 0;             //Disable PWM 1.6 output 
  PINSEL4_bit.P2_5 = 0;                //PWM1.6 on pin 2.5     
}