/********************************************************************************
 *    Bibliothek: main.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012 
 *
 *    Beschreibung: Main Funktion.
 *                   
 *    
 ********************************************************************************/

#include "incl.h"

int main()
{
  bootloader_major=BOOTLOADER_MAJOR;
  bootloader_minor=BOOTLOADER_MINOR;
  boot_core();  
  LED1(LED_SLOW);
  LED2(LED_OFF);
  //Wait for address
  get_address(0);

  LED2(LED_ON);
  while(1)
  {
    if(COMMAND_CNTRL.command_received)
    {
      switch(COMMAND_CNTRL.Kommando)
      {
        //Kommando:           start_system
        //Nummer:             2
        //Beschreibung:       Startet den weiteren Bootvorgang
        case start_system:
          clear_command();
          LED2(LED_OFF);
          while(1) fsm();
          break;
        //Kommando:           prepare_flash
        //Nummer:             0
        //Beschreibung:       Bereitet die Sektoren fürs flashen vor
        case prepare_flash:
          clear_command();
          Prepare_Flash();
          break;
        //Kommando:           get_code
        //Nummer:             1
        //Beschreibung:       empfämgt die neue firmware
        case get_code:
          clear_command();
          receive_firmware();
         break;
        //Kommando:           boot_status
        //Nummer:             31
        //Beschreibung:       Gibt den aktuellen Status des Bootloader aus
        case boot_status:
          clear_command();
          send_boot_status();
         break;
         
        //Kommando:           flash
        //Nummer:             32
        //Beschreibung:       flasht den Prozessor
        case flash:
          clear_command();
          update_firmware();
          LED2(LED_ON);
          break;
          
        //Kommando:           flash
        //Nummer:             32
        //Beschreibung:       flasht den Prozessor
        case bootloader_version:
          clear_command();
          send_bootloader_version();
          break;
          
      }
    }
  }
}