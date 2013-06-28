/********************************************************************************
 *    Bibliothek: default.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012 
 *
 *    Beschreibung: Enthält alle Funktionen, die für das System wichtig sind.
 *                   
 *    
 ********************************************************************************/

#include "incl.h"

struct flags SYSTEM_FLAGS;
unsigned char LED1_WAIT = 0, LED2_WAIT = 0, LED1_COUNT = 0, LED2_COUNT = 0, LED2_BLINK_COUNT_IS = 0,LED2_BLINK_COUNT_SET = 0 ,LED2_BLINK_ENABLE=0;

/*************************************************************************
 *    Funktion: clock_init()
 *
 *    Beschreibung: Stellt den Takt des Controllers auf 100 MHz.
 *                  Dafür wird der interne Oszillator (4MHz) verwendet. 
 * 
 **************************************************************************/

void clock_init (void)  
{                               //PLL Interrupt aktivieren
   SCS_bit.OSCEN=0;
   PLL0CON_bit.PLLC = 0;        //PLL abkoppeln
   PLL0FEED = 0xAA;
   PLL0FEED = 0x55;
   
   PLL0CON_bit.PLLE = 0;        //PLL ausschalten
   PLL0FEED = 0xAA;
   PLL0FEED = 0x55;
   
   CLKSRCSEL_bit.CLKSRC=0;      // internen Oszillator auswählen
   
   
   PLL0CFG_bit.MSEL = 75-1;     //M=75,N=2
   PLL0CFG_bit.NSEL = 2-1;
   PLL0FEED = 0xAA;
   PLL0FEED = 0x55;
   
   PLL0CON_bit.PLLE = 1;        //PLL wieder einschalten
   PLL0FEED = 0xAA;
   PLL0FEED = 0x55;
   
  while(!PLL0STAT_bit.PLOCK);   //warten bis PLL fertig ist
   
   CCLKCFG   = 3-1;            //CCLK= Fcco/3
   
   PLL0CON_bit.PLLC = 1;       // PLL ankoppeln
   PLL0FEED = 0xAA;
   PLL0FEED = 0x55;
   
}

/*************************************************************************
 *    Funktion: void led_timer1_init()
 *
 *    Beschreibung: Initialisiert den Timer für die Status LEDs 
 * 
 **************************************************************************/
void led_timer1_init()
{
  PCLKSEL0_bit.PCLK_TIMER1 = 3;                   //PCLK an TIMERx auf 12,5MH

  // Enable TIM1 clocks
  PCONP_bit.PCTIM1 = 1; // enable clock 

  // Init Timer1  
  T1TCR_bit.CE = 0;     // counting  disable
  T1TCR_bit.CR = 1;     // set reset
  T1TCR_bit.CR = 0;     // release reset
  T1CTCR_bit.CTM = 0;   // Timer Mode: every rising PCLK edge
  T1MCR_bit.MR0I = 1;   // Enable Interrupt on MR0
  T1MCR_bit.MR0R = 1;   // Enable reset on MR0
  T1MCR_bit.MR0S = 0;   // Disable stop on MR0
  
  T1PR = 1000;
  T1MR0 = 500;
  
  // Init timer 1 interrupt
  T1IR_bit.MR0INT = 1;  // clear pending interrupt
  T1TCR_bit.CE = 1;     // counting Enable
  
 //Enable NVIC TMR0 Interrupt
  NVIC_IntPri(NVIC_TIMER1, 3);
  NVIC_IntEnable(NVIC_TIMER1);
  __enable_interrupt();
}

/*************************************************************************
 *    Funktion: io_init()
 *
 *    Beschreibung: Initialisiert alle grundsätzlichen I/O Pins. 
 * 
 **************************************************************************/

void io_init(void)
{
    FIO2DIR_bit.P2_0 = 1;       // Grüne User LED = Output
    FIO2DIR_bit.P2_1 = 1;       // Red User LED = Output
    FIO0DIR_bit.P0_2 = 0;       // AddrIn = Input
    FIO0DIR_bit.P0_3 = 1;       // AddrOut = Output

    FIO2PIN_bit.P2_0 = 0;       // Grüne User LED
    FIO2PIN_bit.P2_1 = 0;       // Red User LED
    FIO0PIN_bit.P0_3 = 0;       // AddrOut = 0
}

/*************************************************************************
 *    Funktion: LED1/2(enum LED_STATUS state)
 *
 *    Beschreibung: Setzt die LED in den angegeben Status
 * 
 **************************************************************************/
void LED1 (enum LED_STATUS state)
{
  switch(state)
  {
    case LED_ON:
      LED1_WAIT = 0;
      FIO2PIN_bit.P2_0 = 1;
      break;
      
    case LED_OFF:
      LED1_WAIT = 0;
      FIO2PIN_bit.P2_0 = 0;
      break;
      
    case LED_FAST:
      LED1_WAIT = 1;
      NVIC_IntEnable(NVIC_TIMER1);
      break;
      
    case LED_SLOW:
      LED1_WAIT = 7;
      NVIC_IntEnable(NVIC_TIMER1);
      break;      
  }
}

void LED2 (enum LED_STATUS state)
{
  switch(state)
  {
    case LED_ON:
      LED2_WAIT = 0;
      FIO2PIN_bit.P2_1 = 1;
      break;
      
    case LED_OFF:
      LED2_WAIT = 0;
      FIO2PIN_bit.P2_1 = 0;
      break;
      
    case LED_FAST:
      LED2_WAIT = 1;
      NVIC_IntEnable(NVIC_TIMER1);
      break;
      
    case LED_SLOW:
      LED2_WAIT = 7;
      NVIC_IntEnable(NVIC_TIMER1);
      break;

  }  
}

void LED2_BLINK(char count)
{
  unsigned long WAIT;
  for(int i=0; i<(count*2);i++)
  {
    
    FIO2PIN_bit.P2_1=~FIO2PIN_bit.P2_1;
    for(WAIT=0;WAIT<3000000;WAIT++);
  }
}

/*************************************************************************
 *    Funktion: timer_init(int PWM_freq)
 *
 *    Beschreibung: - Initialisert Timer0
 *					- Richtet die Interrupt Counter so ein, dass die
 *					  gewünschte PWM Frequenz erreicht wird.
 *					- Startet den Interrupt
 *
 **************************************************************************/ 

void timer_init(void)
{  
  PCLKSEL0_bit.PCLK_TIMER0 = 3;                   //PCLK an TIMERx auf 12,5MHz

  // Enable TIM0 clocks
  PCONP_bit.PCTIM0 = 1; // enable clock 

  // Init Time0  
  T0TCR_bit.CE = 0;     // counting  disable
  T0TCR_bit.CR = 1;     // set reset
  T0TCR_bit.CR = 0;     // release reset
  T0CTCR_bit.CTM = 0;   // Timer Mode: every rising PCLK edge
  T0MCR_bit.MR0I = 1;   // Enable Interrupt on MR0
  T0MCR_bit.MR0R = 1;   // Enable reset on MR0
  T0MCR_bit.MR0S = 0;   // Disable stop on MR0
  
  T0PR = 4000;   // set prescale timer period  
  T0MR0 = 4000;  // set timer 0 period
  
  // Init timer 0 interrupt
  T0IR_bit.MR0INT = 1;  // clear pending interrupt
  T0TCR_bit.CE = 1;     // counting Enable
  
  //Enable NVIC TMR0 Interrupt
 SETENA0_bit.SETENA1=0;
  SYSTEM_FLAGS.timer=1;
}

/*************************************************************************
 *    Funktion: void TMR0_IRQHandler (void)
 *
 *    Beschreibung: TMR0 Interrupt Handler
 * 
 **************************************************************************/
void TMR0_IRQHandler (void)
{
  T0IR_bit.MR0INT = 1;
  CLRPEND0_bit.CLRPEND1=1;
}

/*************************************************************************
 *    Funktion: void TMR1_IRQHandler (void)
 *
 *    Beschreibung: TMR1 Interrupt Handler. Toggelt die LEDs je nach Status.
 * 
 *************************************************************************/
void TMR1_IRQHandler (void)
{
  if(LED1_WAIT == 0);
  else if(LED1_COUNT != LED1_WAIT)
    LED1_COUNT++;
  else
  {
    FIO2PIN_bit.P2_0=~FIO2PIN_bit.P2_0;
    LED1_COUNT = 0;
  }
  
  if(LED2_WAIT == 0);
  else if(LED2_COUNT != LED2_WAIT)
    LED2_COUNT++;
  else
  {
    FIO2PIN_bit.P2_1=~FIO2PIN_bit.P2_1;  
    LED2_COUNT = 0;
  }
  
  T1IR_bit.MR0INT = 1;
  NVIC_ClrPend(NVIC_TIMER1);
}


