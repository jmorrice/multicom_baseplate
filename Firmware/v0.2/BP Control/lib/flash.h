/********************************************************************************
 *    Bibliothek: flash.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Entält Funktions-Header und Kontrolstrukturen für flash.c.
 *                       
 *    
 ********************************************************************************/
/****** Definitionen ******/

#define IAP_LOCATION 0x1FFF1FF1

typedef void (*IAP)(unsigned long [],unsigned long[]);

/****** Strukturen ******/

struct iap
{
  unsigned int byte_counter;
  unsigned long command[5];
  unsigned long result[5];
};

/****** Aufzählungen ******/

extern struct iap IAP_CNTRL;

enum {
  CMD_PREPARE_SECTORS = 50,
  CMD_COPY_RAM_TO_FLASH,
  CMD_ERASE_SECTORS,
  CMD_BLANK_CHECK_SECTORS,
  CMD_READ_PART_ID,
  CMD_READ_BOOT_CODE_VERSION,
  CMD_COMPARE,
  CMD_ENTER_ISP,
  CMD_SERIAL_NUMBER
};

enum {
  STATUS_CMD_SUCCESS = 0,
  STATUS_INVALID_COMMAND,
  STATUS_SRC_ADDR_ERROR,
  STATUS_DST_ADDR_ERROR,
  STATUS_SRC_ADDR_NOT_MAPPED,
  STATUS_DST_ADDR_NOT_MAPPED,
  STATUS_COUNT_ERROR,
  STATUS_INVALID_SECTOR,
  STATUS_SECTOR_NOT_BLANK,
  STATUS_SECTOR_NOT_PREPARED_FOR_WRITE_OPERATION,
  STATUS_COMPARE_ERROR,
  STATUS_BUSY,
};

/****** Header ******/

int iap_prepare_sector(unsigned char start_sector, unsigned char end_sector);

int iap_write_flash(unsigned long flash_address,unsigned char *ram_address, unsigned long size, unsigned long clk_speed);

int iap_erase_sector(unsigned char start_sector, unsigned char end_sector, unsigned long clk_speed);

int iap_check_blank(unsigned char start_sector, unsigned char end_sector);

void iap_enter_isp(void);

void Flash(void);