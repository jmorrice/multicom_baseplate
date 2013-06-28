/********************************************************************************
 *    Bibliothek: i2c.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schr�der, Mult.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enth�lt die Definitionen und Strukturen f�r i2c_master.c und i2c_slave.c .
 *                   
 *    
 ********************************************************************************/


/****** Definitionen ******/

#define busy 0
#define free 1

#define write 0
#define read 1

#define I2C_Timeout 5000


/****** Aufz�hlungen ******/

enum error { send_fail=2, 
             receive_fail, 
             no_data, 
             no_ack, 
             i2c_busy, 
             unused_data, 
             ok, 
             unknown_state,
             timeout};



/****** Strukturen ******/

/****************************************************************************************
 *    Struktur: i2c
 *
 *    Beschreibung: enth�lt alle wichtigen Flags und Buffer f�r die I�C Kommunikation. 
 * 
 ****************************************************************************************/

struct i2c 
{
  unsigned char SendBuffer[130]; 
  unsigned char ReceiveBuffer[150];
  unsigned char SendCounter;
  unsigned char ReceiveCounter;
  unsigned int SendSize;
  unsigned int ReceiveSize;
  unsigned char Busy;
  unsigned char RoW;
  unsigned char Addr;
  unsigned char WriteRead;
  unsigned char Data;
  enum error  Error_ID;
  unsigned char Transmission;
  unsigned int Bytes;
  unsigned char address;
};