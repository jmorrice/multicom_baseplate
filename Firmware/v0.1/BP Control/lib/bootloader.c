/********************************************************************************
 *    Bibliothek: bootloader.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 19.12.2012   
 *
 *    Beschreibung: Enthält funktionen zum flashen der Firmware 
 *                  und zum Booten der Core funktionen.
 *                      
 *
 ********************************************************************************/

#include "incl.h"
int bootloader_major, bootloader_minor;
 struct boot BOOTLOADER_CNTRL;  
   
void boot_core(void)
{
  clock_init();
  io_init();
  led_timer1_init();     
  LED1(LED_FAST);
  LED2(LED_OFF);
  i2c_slave_init();
}

/*************************************************************************
 *    Funktion: sector()
 *
 *    Beschreibung: Ermittelt aus der übergebenen addresse den passenden Sektor. 
 *                 
 **************************************************************************/

unsigned int sector(unsigned long addr)
{
  unsigned long sector_addr=0x00;
  unsigned long add=0x1000;
  for(int i=0;i<16;i++)         //alle 4k Blöcke durchgehen
  {
    if(addr >= sector_addr && addr <=(sector_addr+0xFFF)) return(i);
    sector_addr+=add;
  }
  add=0x7FFF;     
  for(int i=16;i<30;i++)       // alle 32k Blöcke durchgehen
  {
    if(addr >= sector_addr && addr <=(sector_addr+0x7FFF)) return(i);
    sector_addr+=add;
  }
  
  return 0;
}
       
/*************************************************************************
 *    Funktion: prepare_flash()
 *
 *    Beschreibung: Bereitet alles für das flashen vor. 
 *                 
 **************************************************************************/

       
void Prepare_Flash(void)
{
        clear_command();
        BOOTLOADER_CNTRL.finished_sectors=0;
        BOOTLOADER_CNTRL.received_packages=0;
        BOOTLOADER_CNTRL.package_id_old=0;
        BOOTLOADER_CNTRL.array_pos=0;
        
        //save commands
        BOOTLOADER_CNTRL.byte_count=((COMMAND_CNTRL.param[0]<<8)|COMMAND_CNTRL.param[1]);
        BOOTLOADER_CNTRL.start_address=((COMMAND_CNTRL.param[2]<<16)|(COMMAND_CNTRL.param[3]<<8)|COMMAND_CNTRL.param[4]);
        BOOTLOADER_CNTRL.start_address_const=BOOTLOADER_CNTRL.start_address;
        BOOTLOADER_CNTRL.checksum_received=COMMAND_CNTRL.param[5];
        //calculate checksum
        BOOTLOADER_CNTRL.checksum_calculated=COMMAND_CNTRL.param[0]^COMMAND_CNTRL.param[1]^COMMAND_CNTRL.param[2]^COMMAND_CNTRL.param[3]^COMMAND_CNTRL.param[4];
        
        if(BOOTLOADER_CNTRL.checksum_calculated == BOOTLOADER_CNTRL.checksum_received)
        {  
          
          BOOTLOADER_CNTRL.sector_count= (BOOTLOADER_CNTRL.byte_count/SECTOR_SIZE)+1;
          BOOTLOADER_CNTRL.package_count= (BOOTLOADER_CNTRL.byte_count/PACKAGE_SIZE)+1;
          
          BOOTLOADER_CNTRL.sector=sector(BOOTLOADER_CNTRL.start_address);
          
          // calculate bytes per sector
          for(int i=0;i<BOOTLOADER_CNTRL.sector_count;i++)
          {
            if(i==BOOTLOADER_CNTRL.sector_count-1)
            {
              BOOTLOADER_CNTRL.bytes_per_sector[i]=(BOOTLOADER_CNTRL.byte_count%SECTOR_SIZE);
            }
            else
            {
              BOOTLOADER_CNTRL.bytes_per_sector[i]=SECTOR_SIZE;
            }
          }
          //calculate bytes per package
          for(int i=0;i<BOOTLOADER_CNTRL.package_count;i++)
          {
            if(i==BOOTLOADER_CNTRL.package_count-1)
            {
              BOOTLOADER_CNTRL.bytes_per_package[i]=(BOOTLOADER_CNTRL.byte_count%PACKAGE_SIZE);
            }
            else
            {
              BOOTLOADER_CNTRL.bytes_per_package[i]=PACKAGE_SIZE;
            }
          }
          
          
          
          
        }
           
        else
        {
          BOOTLOADER_CNTRL.flash_status=CHECKSUM_ERR;
        }       
 }
 
 
/*************************************************************************
 *    Funktion: update_firmware()
 *
 *    Beschreibung: Wickelt das flashen der neuen Firmware ab. 
 *                 
 **************************************************************************/
 
void receive_firmware(void)
 {
   BOOTLOADER_CNTRL.package_id=(I2C0_CNTRL.ReceiveBuffer[1]<<8)|I2C0_CNTRL.ReceiveBuffer[2];                    //parameter speichern
   BOOTLOADER_CNTRL.checksum_received=(I2C0_CNTRL.ReceiveBuffer[3]<<8)|I2C0_CNTRL.ReceiveBuffer[4];             
   
   
   // Checksumme berechnen
     BOOTLOADER_CNTRL.checksum_calculated= I2C0_CNTRL.ReceiveBuffer[5];
     for(int i =5;i< BOOTLOADER_CNTRL.bytes_per_package[BOOTLOADER_CNTRL.package_id]+5;i++)
     {
       BOOTLOADER_CNTRL.checksum_calculated+=I2C0_CNTRL.ReceiveBuffer[i+1];
     }

   //package id kontrollieren
   if( BOOTLOADER_CNTRL.package_id==BOOTLOADER_CNTRL.package_id_old+1 || BOOTLOADER_CNTRL.package_id==0 )   //gültige package id?
   {
     
     if(BOOTLOADER_CNTRL.checksum_calculated==BOOTLOADER_CNTRL.checksum_received)
     {
       // wegspeichern
       int array_pos;
       array_pos=BOOTLOADER_CNTRL.package_id*PACKAGE_SIZE;
       for(int i=0;i<BOOTLOADER_CNTRL.bytes_per_package[BOOTLOADER_CNTRL.package_id];i++)
       {   
         CAM_CNTRL.Picture[i+array_pos]=I2C0_CNTRL.ReceiveBuffer[i+5];
       }
       BOOTLOADER_CNTRL.received_packages++;
       BOOTLOADER_CNTRL.package_id_old=BOOTLOADER_CNTRL.package_id;
       FIO2PIN_bit.P2_1=~FIO2PIN_bit.P2_1;         //LED togglen
     }
     else
     {
       BOOTLOADER_CNTRL.flash_status=CHECKSUM_ERR;
     }
     
   }
   
   else        //package error!!                                         
   {
     BOOTLOADER_CNTRL.flash_status=PACKAGE_ID_ERR;
   }

   if(BOOTLOADER_CNTRL.received_packages == BOOTLOADER_CNTRL.package_count)
   {
     BOOTLOADER_CNTRL.flash_status=READY_TO_FLASH;
     COMMAND_CNTRL.Kommando=flash;
     COMMAND_CNTRL.command_received=1;
   }
 }


void update_firmware(void)
{
  
  if(BOOTLOADER_CNTRL.sector>2)
  {
    if(iap_prepare_sector(BOOTLOADER_CNTRL.sector, (BOOTLOADER_CNTRL.sector+BOOTLOADER_CNTRL.sector_count-1))!=0)
    {
      BOOTLOADER_CNTRL.flash_status=PREP_ERR;
    }
    else
    {
      if(iap_erase_sector(BOOTLOADER_CNTRL.sector, (BOOTLOADER_CNTRL.sector+BOOTLOADER_CNTRL.sector_count-1),100000)!=0)
      {
        BOOTLOADER_CNTRL.flash_status=PREP_ERR;
      }
      else
      {
          for(int i=0;i<BOOTLOADER_CNTRL.sector_count;i++)
          {
            if(iap_prepare_sector(BOOTLOADER_CNTRL.sector+i,BOOTLOADER_CNTRL.sector+i)!=0)
            {
              BOOTLOADER_CNTRL.flash_status=PREP_ERR;
              break;
            }
            else
            {
              if(iap_write_flash(calculate_rom_address(i),&CAM_CNTRL.Picture[calculate_array_pos(i)],4096,100000)!=0)
              {
                BOOTLOADER_CNTRL.flash_status=WRITE_ERR;
                break;
              }
              else
              {
                unsigned char *ram_p, *flash_p;
                ram_p=&CAM_CNTRL.Picture[0];
                flash_p=(unsigned char*) BOOTLOADER_CNTRL.start_address_const;
                for(int i=0;i<BOOTLOADER_CNTRL.byte_count;i++)
                {
                  BOOTLOADER_CNTRL.flash_status=WRITE_SUCCESS;
                  if(ram_p[i]!=flash_p[i])
                  {
                    BOOTLOADER_CNTRL.flash_status=COMPARE_ERR;
                    break;
                  }
                }
              }
            }
          }
      }
    }
  }
  
  else
  {
    BOOTLOADER_CNTRL.flash_status=LOCKED_SECTOR;
  }
}
 
  /*************************************************************************
 *    Funktion: calculate_rom_address()
 *
 *    Beschreibung: Gibt eine Addresse im Flash zurück
 *                 
 **************************************************************************/
 unsigned long calculate_rom_address(unsigned int sector)
 {
   if(sector==0) return BOOTLOADER_CNTRL.start_address;
   else
   {
     BOOTLOADER_CNTRL.start_address +=  BOOTLOADER_CNTRL.bytes_per_sector[sector-1];
     return BOOTLOADER_CNTRL.start_address;
   }
 }
 
   /*************************************************************************
 *    Funktion: calculate_array_pos()
 *
 *    Beschreibung: Gibt eine Position des Arrays zurück
 *                 
 **************************************************************************/
 unsigned int calculate_array_pos(unsigned int sector)
 {
   if(sector==0) return BOOTLOADER_CNTRL.array_pos;
   else
   {
     BOOTLOADER_CNTRL.array_pos += BOOTLOADER_CNTRL.bytes_per_sector[sector-1];
     return BOOTLOADER_CNTRL.array_pos;
   }
 }
 
 
 /*************************************************************************
 *    Funktion: send_status()
 *
 *    Beschreibung: Stellt den Status des Bootloaders über I2C bereit 
 *                 
 **************************************************************************/
  
void send_boot_status(void)
{
   I2C0_CNTRL.SendSize=1;
   I2C0_CNTRL.SendBuffer[0] = (char) BOOTLOADER_CNTRL.flash_status;         
}

 /*************************************************************************
 *    Funktion: send_bootloader_version()
 *
 *    Beschreibung: Stellt die Versionsnummer des Bootloaders über I2C bereit 
 *                 
 **************************************************************************/
void send_bootloader_version(void)
{
   I2C0_CNTRL.SendSize=2;
   I2C0_CNTRL.SendBuffer[0] = (char) bootloader_major;
   I2C0_CNTRL.SendBuffer[1] = (char) bootloader_minor;
   
}
  