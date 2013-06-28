/********************************************************************************
 *    Bibliothek: nvic.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Jonathan Morrice, Multi.Com GmbH, IAR Systems
 *
 *    Datum: 18.09.2012 
 *
 *    Beschreibung: Enthält Funktion zur Steuerung des Nest Vector Interrupt Controllers (NVIC).
 *                   
 *    
 ********************************************************************************/

#include "incl.h"
#include <intrinsics.h>

void NVIC_IntEnable(Int32U IntNumber);
void NVIC_IntDisable(Int32U IntNumber);
void NVIC_ClrPend(Int32U IntNumber);
void NVIC_IntPri(Int32U IntNumber, Int8U Priority);

/*************************************************************************
 *    Funktion: NVIC_IntEnable(Int32U IntNumber)
 *
 *    Beschreibung: - Aktiviert einen Interrupt, der als Parameter übergeben wird.
 *
 **************************************************************************/
void NVIC_IntEnable(Int32U IntNumber)
{
  volatile unsigned long * pNVIC_SetEn = &SETENA0;

  assert((NVIC_WDT <= IntNumber) && (NVIC_PLL1 >= IntNumber));
  IntNumber -= NVIC_WDT;

  pNVIC_SetEn += IntNumber/32;
  *pNVIC_SetEn = (1UL<<(IntNumber%32));
}

/*************************************************************************
 * Function Name: NVIC_IntDisable
 * Parameters: IntNumber - Interrup number
 * Return: void
 *
 * Description: Disables interrup at NVIC
 *
 *
 *************************************************************************/
void NVIC_IntDisable(Int32U IntNumber)
{
volatile unsigned long * pNVIC_ClrEn = &CLRENA0;

  assert((NVIC_WDT <= IntNumber) && (NVIC_PLL1 >= IntNumber));
  IntNumber -= NVIC_WDT;

  pNVIC_ClrEn += IntNumber/32;
  *pNVIC_ClrEn = (1UL<<(IntNumber%32));

}

/*************************************************************************
 *    Funktion: void NVIC_ClrPend(Int32U IntNumber)
 *
 *    Beschreibung: - Beendet einen Interrupt, der als Parameter übergeben wird.
 *
 **************************************************************************/
void NVIC_ClrPend(Int32U IntNumber)
{
  volatile unsigned long * pNVIC_ClrPend = &CLRPEND0;

  assert((NVIC_WDT <= IntNumber) && (NVIC_PLL1 >= IntNumber));
  IntNumber -= NVIC_WDT;

  pNVIC_ClrPend += IntNumber/32;
  *pNVIC_ClrPend = (1UL<<(IntNumber%32));
}

/*************************************************************************
 * Function Name: NVIC_IntPri
 * Parameters: IntNumber - Interrup number, Interrupt Priority
 * Return: void
 *
 * Description:Sets Interrupt priority
 *
 *
 *************************************************************************/
void NVIC_IntPri(Int32U IntNumber, Int8U Priority)
{
volatile Int8U * pNVIC_IntPri = (Int8U *)&IP0;

  assert((NVIC_WDT <= IntNumber) && (NVIC_PLL1 >= IntNumber));
  IntNumber -= NVIC_WDT;
  pNVIC_IntPri += IntNumber;
  *pNVIC_IntPri = Priority;
}