/********************************************************************************
 *    Bibliothek: default.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Jonathan Morrice Mult.Com GmbH
 *
 *    Datum: 18.09.2012   
 *
 *    Beschreibung: Enthält die Funktionsheader aus default.c .
 *                   
 *    
 ********************************************************************************/
/****** Strukturen ******/

struct flags
{
  unsigned char timer;
  
};

enum LED_STATUS { LED_ON, LED_FAST, LED_SLOW, LED_OFF};  

extern struct flags SYSTEM_FLAGS;
  

/****** Header ******/

void clock_init (void);

void io_init(void);

void LED1 (enum LED_STATUS state);

void LED2 (enum LED_STATUS state);
void LED2_BLINK(char count);

void timer_init(void);

void led_timer1_init(void);

