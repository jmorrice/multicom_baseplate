/********************************************************************************
 *    Bibliothek: flash.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Entält alle Funktionen die für das IAP benötigt werden.
 *                       
 *    
 ********************************************************************************/
#include "incl.h"


struct iap IAP_CNTRL;

IAP iap_entry;

/*************************************************************************
 *    Funktion: iap_prepare_sector()
 *
 *    Beschreibung: Bereitet die übergebenen Sektoren zur bearbeitung vor.
 *                 
 *    Hinweis: Als Paramter werden nur die Sektorennummern (0-29) übergeben, keine Addressen!
 *
 **************************************************************************/

int iap_prepare_sector(unsigned char start_sector, unsigned char end_sector)
{
  __disable_interrupt();
  iap_entry=(IAP) IAP_LOCATION;
  
  IAP_CNTRL.command[0]=CMD_PREPARE_SECTORS;
  IAP_CNTRL.command[1]= start_sector;
  IAP_CNTRL.command[2]= end_sector;
  
  iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
  __enable_interrupt();
  if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
  {
    return IAP_CNTRL.result[0];
  }
  return IAP_CNTRL.result[0];
}
/*************************************************************************
 *    Funktion: iap_write_flash()
 *
 *    Beschreibung: Kopiert Programmcode aus dem Ram in den Flash.
 *                 
 *    Hinweis: flash_address: Addresse im Flashspeicher.
 *             ram_address: Array im Ram.
 *             size: 256 | 512 | 1024 | 4096.
 *             clk_speed: Prozessortakt in KHz.
 *
 **************************************************************************/

int iap_write_flash(unsigned long flash_address,unsigned char *ram_address, unsigned long size, unsigned long clk_speed)
{ 
    __disable_interrupt();
    IAP_CNTRL.command[0]= CMD_COPY_RAM_TO_FLASH;
    IAP_CNTRL.command[1]= (unsigned int)flash_address;
    IAP_CNTRL.command[2]= (unsigned int) ram_address;
    IAP_CNTRL.command[3]= size;
    IAP_CNTRL.command[4]= clk_speed;
    
    iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
  {
    return IAP_CNTRL.result[0];
  }
  return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: iap_erase_sector()
 *
 *    Beschreibung: Löscht die angegebenen Sektoren.
 *                 
 *    Hinweis: Als Paramter werden nur die Sektorennummern (0-29) übergeben, keine Addressen!
 *             clk_speed: Prozessortakt in KHz.
 *
 **************************************************************************/

int iap_erase_sector(unsigned char start_sector, unsigned char end_sector, unsigned long clk_speed)
{
    __disable_interrupt();
    IAP_CNTRL.command[0]=CMD_ERASE_SECTORS;
    IAP_CNTRL.command[1]= start_sector;
    IAP_CNTRL.command[2]= end_sector;
    IAP_CNTRL.command[3]= clk_speed;
    
    

  iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
  {
    return IAP_CNTRL.result[0];
  }
  return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: iap_check_blank()
 *
 *    Beschreibung: Überprüft, ob die Sektoren leer sind.
 *                 
 *    Hinweis: Als Paramter werden nur die Sektorennummern (0-29) übergeben, keine Addressen!
 *
 **************************************************************************/

int iap_check_blank(unsigned char start_sector, unsigned char end_sector)
{
    __disable_interrupt();
    iap_entry=(IAP) IAP_LOCATION;
    
    IAP_CNTRL.command[0] = CMD_BLANK_CHECK_SECTORS;
    IAP_CNTRL.command[1] = start_sector;
    IAP_CNTRL.command[2] = end_sector;
  
    iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
    {
      return IAP_CNTRL.result[0];
    }
    return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: iap_boot_code_version()
 *
 *    Beschreibung: Liest die aktuelle Versionsnummer des Bootcodes.
 *                 
 *    Hinweis: Der Rückgabewert besteht aus 2 Bytes im Ascii format <Byte1(Major)>.<byte0(Minor)>.
 *
 **************************************************************************/
int iap_boot_code_version(void)
{
    __disable_interrupt();
    iap_entry=(IAP) IAP_LOCATION;
    
    IAP_CNTRL.command[0] = CMD_READ_BOOT_CODE_VERSION;
    
    iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
    {
      return IAP_CNTRL.result[0];
    }
    return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: iap_part_ID()
 *
 *    Beschreibung: Liest die part identification number.
 *                 
 *    Hinweis: Über die part ID lässt sich eindeutig sagen, um was für ein LPC17xx Device es sich handelt.
 *             Eine genaue Tabelle der Part IDs ist im Datenblatt zu finden.
 * 
 **************************************************************************/

int iap_part_ID(void)
{
    __disable_interrupt();
    iap_entry=(IAP) IAP_LOCATION;
    
    IAP_CNTRL.command[0] = CMD_READ_PART_ID;
    
    iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
    {
      return IAP_CNTRL.result[0];
    }
    return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: iap_enter_isp()
 *
 *    Beschreibung: Startet den ISP Bootloader.
 *                 
 **************************************************************************/

void iap_enter_isp(void)
{
  iap_entry=(IAP) IAP_LOCATION;
  
  IAP_CNTRL.command[0] = CMD_ENTER_ISP;
  
  iap_entry (IAP_CNTRL.command, IAP_CNTRL.result); 
}

/*************************************************************************
 *    Funktion: iap_serial_number()
 *
 *    Beschreibung: Liest die Seriennummer des Controllers. 
 *                 
 **************************************************************************/

int iap_serial_number(void)
{
  __disable_interrupt();
  iap_entry=(IAP) IAP_LOCATION;
    
    IAP_CNTRL.command[0] = CMD_SERIAL_NUMBER;
    
    iap_entry (IAP_CNTRL.command, IAP_CNTRL.result);
    __enable_interrupt();
    if(IAP_CNTRL.result[0] != STATUS_CMD_SUCCESS)
    {
      return IAP_CNTRL.result[0];
    }
    return IAP_CNTRL.result[0];
}

/*************************************************************************
 *    Funktion: Flash()
 *
 *    Beschreibung: Wickelt das Flashen des Controllers ab (under construction !). 
 *                 
 **************************************************************************/

void Flash(void)
{
  
  //Dies ist nur eine Testfunktion die einen Dummycode in den Sektor X schreibt!
  iap_prepare_sector(15,16);
  iap_erase_sector(15,16,100000);
  if(iap_check_blank(15,16)==STATUS_SECTOR_NOT_BLANK) while(1);
  iap_prepare_sector(15,16);
  iap_write_flash(0x0000F000,&I2C0_CNTRL.ReceiveBuffer[2],256,100000);
}
 